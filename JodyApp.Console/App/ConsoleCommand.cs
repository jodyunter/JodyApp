﻿using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.App
{
    public class ConsoleCommand
    {
        public List<object> Arguments { get; set; }
        public string Name { get; set; }
        public string LibraryClassName { get; set; }        

        public List<object> ApplicationArguments { get; set; }
        
        public ConsoleCommand(string input, ApplicationContext context)
        {
            Arguments = new List<object>();
            ApplicationArguments = new List<object>();

            // Ugly regex to split string on spaces, but preserve quoted text intact:
            var stringArray =
                Regex.Split(input,
                    "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            for (int i = 0; i < stringArray.Length; i++)
            {
                // The first element is always the command:
                if (i == 0)
                {
                    Name = stringArray[i];

                    // Set the default:
                    if (context.LastCommandLibrary != null)
                    {
                        LibraryClassName = context.LastCommandLibrary;
                    }
                    else
                    {
                        LibraryClassName = "DefaultCommands";                        
                    }
                    string[] s = stringArray[0].Split('.');
                    if (s.Length == 2)
                    {
                        LibraryClassName = s[0];
                        Name = s[1];

                        if (!LibraryClassName.Contains("Commands")) LibraryClassName += "Commands";
                    }

                    context.LastCommandLibrary = LibraryClassName;
                }
                else
                {
                    var inputArgument = stringArray[i];
                    string argument = inputArgument;

                    // Is the argument a quoted text string?
                    var regex = new Regex("\"(.*?)\"", RegexOptions.Singleline);
                    var match = regex.Match(inputArgument);

                    if (match.Captures.Count > 0)
                    {
                        // Get the unquoted text:
                        var captureQuotedText = new Regex("[^\"]*[^\"]");
                        var quoted = captureQuotedText.Match(match.Captures[0].Value);
                        argument = quoted.Captures[0].Value;
                    }
                    Arguments.Add(argument);
                }
            }

            ApplicationArguments.Add(context);
        }
        
    }
}

