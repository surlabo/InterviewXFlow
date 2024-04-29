using System;
using System.Collections.Generic;

namespace Complexity
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new List<int>();

            var random = new Random();
            for (var i = 0; i < 100; i++)
            {
                list[i] = random.Next(100);
            }

            for (var i = 0; i < list.Count; i++)
            {
                for (var j = 0; j < list.Count; j++)
                {
                    if (j != i && list[j] == list[i])
                    {
                        list.RemoveAt(j);
                        j--;
                    }
                }
            }
        }
    }
}
