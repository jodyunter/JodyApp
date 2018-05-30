using JodyApp.ConsoleApp.IO;
using JodyApp.ConsoleApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.App
{
    public class Application
    {
        public static List<string> AnyTimeCommands = new List<string>() { "QUIT", "BACK" };

        public ApplicationContext Context { get; set; }
        public Application(ApplicationContext context)
        {
            Context = context;
        }


        public static string ReadFromConsole(ApplicationContext context, string promptMessage = "", string extraInfo = "")
        {
            var consoleInput = IOMethods.ReadFromConsole(context, promptMessage, extraInfo);

            PreProcessCommand(context, consoleInput);

            return consoleInput;
        }

        public static Dictionary<string, string> GatherData(ApplicationContext context, string dataContext, List<string> prompts)
        {
            var result = new Dictionary<string, string>();

            for (int i = 0; i < prompts.Count; i++)
            {
                result[prompts[i]] = ReadFromConsole(context, dataContext + ">" + prompts[i] + ">");
            }

            return result;

        }
        public static string PreProcessCommand(ApplicationContext context, string input)
        {
            switch (input.ToUpper())
            {
                case "QUIT":
                    return RunExitRoutine();                    
                case "BACK":
                    IOMethods.WriteToConsole(context.GetLastView());
                    return null;                    
                default:
                    return input;
            }
        }
        public void Run()
        {
            while (true)
            {
                var consoleInput = ReadFromConsole(Context);
                if (string.IsNullOrWhiteSpace(consoleInput)) continue;
                try
                {                    
                    // Create a ConsoleCommand instance:
                    var cmd = new ConsoleCommand(consoleInput, Context);

                    // Execute the command:
                    var result = Execute(Context, cmd);

                    // Write out the result:
                    if (result != null)
                    {
                        IOMethods.WriteToConsole(result.GetView());
                        Context.AddView(result);
                    }

                }
                catch (Exception ex)
                {
                    // OOPS! Something went wrong - Write out the problem:
                    IOMethods.WriteToConsole(ex.Message);
                }
            }

        }

        BaseView Execute(ApplicationContext context, ConsoleCommand command)
        {
            var badCommandMessage_NoClassName = string.Format("No commands found for {0}.", command.LibraryClassName);
            var badCommandMessage_NoMethodName = string.Format("No command {0} exists in {1}", command.Name, command.LibraryClassName);

            if (!context.CommandLibraries.ContainsKey(command.LibraryClassName))
            {
                return new ErrorView(badCommandMessage_NoClassName);

            }
            var methodDictionary = context.CommandLibraries[command.LibraryClassName];
            if (!methodDictionary.ContainsKey(command.Name))
            {
                return new ErrorView(badCommandMessage_NoMethodName);
            }

            // Make sure the corret number of required arguments are provided:
            // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            var methodParameterValueList = new List<object>();
            IEnumerable<ParameterInfo> paramInfoList = methodDictionary[command.Name].ToList();

            // Validate proper # of required arguments provided. Some may be optional:
            var requiredParams = paramInfoList.Where(p => p.IsOptional == false);
            var optionalParams = paramInfoList.Where(p => p.IsOptional == true);
            int applicationArguments = command.ApplicationArguments.Count();

            int requiredCount = requiredParams.Count() - applicationArguments;
            int optionalCount = optionalParams.Count();
            int providedCount = command.Arguments.Count();

            if (requiredCount > providedCount)
            {
                return new ErrorView(string.Format(
                    "Missing required argument. {0} required, {1} optional, {2} provided",
                    requiredCount, optionalCount, providedCount));
            }

            // Make sure all arguments are coerced to the proper type, and that there is a 
            // value for every emthod parameter. The InvokeMember method fails if the number 
            // of arguments provided does not match the number of parameters in the 
            // method signature, even if some are optional:
            // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (paramInfoList.Count() > 0)
            {
                // Populate the list with default values:
                foreach (var param in paramInfoList)
                {
                    // This will either add a null object reference if the param is required 
                    // by the method, or will set a default value for optional parameters. in 
                    // any case, there will be a value or null for each method argument 
                    // in the method signature:
                    methodParameterValueList.Add(param.DefaultValue);
                }

                //these are the always included arguments
                for (int i = 0; i < command.ApplicationArguments.Count(); i++)
                {
                    methodParameterValueList.RemoveAt(i);
                    methodParameterValueList.Insert(i, command.ApplicationArguments[i]);

                }

                // Now walk through all the arguments passed from the console and assign 
                // accordingly. Any optional arguments not provided have already been set to 
                // the default specified by the method signature:
                for (int i = applicationArguments; i < command.Arguments.Count() + applicationArguments; i++)
                {
                    int commandArgumentPosition = i - applicationArguments;
                    var methodParam = paramInfoList.ElementAt(i);
                    var typeRequired = methodParam.ParameterType;
                    object value = null;
                    try
                    {
                        // Coming from the Console, all of our arguments are passed in as 
                        // strings. Coerce to the type to match the method paramter:
                        value = CoerceArgument(typeRequired, command.Arguments.ElementAt(commandArgumentPosition));
                        methodParameterValueList.RemoveAt(i);
                        methodParameterValueList.Insert(i, value);
                    }
                    catch (ArgumentException ex)
                    {
                        string argumentName = methodParam.Name;
                        string argumentTypeName = typeRequired.Name;
                        string message =
                            string.Format(""
                            + "The value passed for argument '{0}' cannot be parsed to type '{1}'",
                            argumentName, argumentTypeName);
                        throw new ArgumentException(message);
                    }
                }
            }

            // Set up to invoke the method using reflection:
            // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            Assembly current = typeof(Program).Assembly;

            // Need the full Namespace for this:
            Type commandLibaryClass =
                current.GetType(context.CommandNameSpace + "." + command.LibraryClassName);

            var inputArgs = new List<object>();

            if (methodParameterValueList.Count > 0)
            {
                inputArgs.AddRange(methodParameterValueList);
            }

            var typeInfo = commandLibaryClass;

            // This will throw if the number of arguments provided does not match the number 
            // required by the method signature, even if some are optional:
            try
            {
                var result = typeInfo.InvokeMember(
                    command.Name,
                    BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public,
                    null, context.CommandObjects[commandLibaryClass.Name], inputArgs.ToArray());
                return (BaseView)result;
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }

        }

        static string RunExitRoutine()
        {
            IOMethods.WriteToConsole("Exiting Program, press enter to close the windoer");
            Console.ReadLine();
            Environment.Exit(0);

            return null;
        }

        public static object CoerceArgument(Type requiredType, object input)
        {

            var inputValue = (string)input;
            var requiredTypeCode = Type.GetTypeCode(requiredType);
            string exceptionMessage =
                string.Format("Cannnot coerce the input argument {0} to required type {1}",
                inputValue, requiredType.Name);

            object result = null;
            switch (requiredTypeCode)
            {
                case TypeCode.String:
                    result = inputValue;
                    break;
                case TypeCode.Int16:
                    short number16;
                    if (Int16.TryParse(inputValue, out number16))
                    {
                        result = number16;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Int32:
                    int number32;
                    if (Int32.TryParse(inputValue, out number32))
                    {
                        result = number32;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Int64:
                    long number64;
                    if (Int64.TryParse(inputValue, out number64))
                    {
                        result = number64;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Boolean:
                    bool trueFalse;
                    if (bool.TryParse(inputValue, out trueFalse))
                    {
                        result = trueFalse;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Byte:
                    byte byteValue;
                    if (byte.TryParse(inputValue, out byteValue))
                    {
                        result = byteValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Char:
                    char charValue;
                    if (char.TryParse(inputValue, out charValue))
                    {
                        result = charValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.DateTime:
                    DateTime dateValue;
                    if (DateTime.TryParse(inputValue, out dateValue))
                    {
                        result = dateValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Decimal:
                    Decimal decimalValue;
                    if (Decimal.TryParse(inputValue, out decimalValue))
                    {
                        result = decimalValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Double:
                    Double doubleValue;
                    if (Double.TryParse(inputValue, out doubleValue))
                    {
                        result = doubleValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Single:
                    Single singleValue;
                    if (Single.TryParse(inputValue, out singleValue))
                    {
                        result = singleValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.UInt16:
                    UInt16 uInt16Value;
                    if (UInt16.TryParse(inputValue, out uInt16Value))
                    {
                        result = uInt16Value;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.UInt32:
                    UInt32 uInt32Value;
                    if (UInt32.TryParse(inputValue, out uInt32Value))
                    {
                        result = uInt32Value;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.UInt64:
                    UInt64 uInt64Value;
                    if (UInt64.TryParse(inputValue, out uInt64Value))
                    {
                        result = uInt64Value;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                default:
                    throw new ArgumentException(exceptionMessage);
            }
            return result;
        }
    }
}
