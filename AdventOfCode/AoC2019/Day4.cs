using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day4
    {       
        public  void Solve()
        {
            var start = 123257;
            var end = 647015;
            var count1 = 0;
            var count2 = 0;
            for (int i = start; i < end; i++)
            {
                var s = i.ToString();


                var valid = true;
                for (int j = 0; j < s.Length - 1; j++)
                {
                    if (s[j] > s[j + 1])
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    var valid1 = false;
                    var valid2 = false;

                    for (char j = '0'; j < '9' + 1; j++)
                    {
                        if (s.Count(c => c == j) > 1)
                            valid1 = true;

                        if (s.Count(c => c == j) == 2)
                            valid2 = true;
                    }

                    if (valid1)
                        count1++;

                    if (valid2)
                        count2++;
                }

            }

            Console.WriteLine($"Day 4 - 1: {count1}");
            Console.WriteLine($"Day 4 - 2: {count2}");
        }

    }
}
