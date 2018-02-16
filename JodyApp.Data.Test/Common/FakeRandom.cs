using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Data.Test.Common
{
    public class FakeRandom: Random
    {
        int count = 0;
        List<int> NumberList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public FakeRandom(List<int> list)
        {
            NumberList = list;
        }
        public override int Next(int maxValue)
        {
            if (count >= NumberList.Count)
            {
                count = 0;
            }
            
            int result = NumberList[count];
            count++;
            return result;

        }
    }
}
