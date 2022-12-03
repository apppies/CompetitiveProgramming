using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day6
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input6.txt")[0].Split(new char[] { ',' }).Select(c => int.Parse(c)).ToList();
            input = new List<int>() { 3, 4, 3, 1, 2 };
            for (int d = 0; d < 80; d++)
            {
                var toAdd = 0;
                for (int i = 0; i < input.Count; i++)
                {
                    if (input[i] == 0)
                    {
                        toAdd++;
                        input[i] = 7;
                    }
                    input[i]--;

                }

                for (int i = 0; i < toAdd; i++)
                {
                    input.Add(8);
                }
                Console.WriteLine($"Day {d + 1}: {input.Count}");
            }
            Console.WriteLine($"Day 80: {input.Count}");

            input = System.IO.File.ReadAllLines("input6.txt")[0].Split(new char[] { ',' }).Select(c => int.Parse(c)).ToList();
            input = new List<int>() { 3, 4, 3, 1, 2 };
            var bins = new long[9];
            for (int i = 0; i < input.Count; i++)
            {
                bins[input[i]]++;
            }

            for (int d = 0; d < 256; d++)
            {
                var over = bins[0];
                for (int i = 1; i < bins.Length; i++)
                {
                    bins[i - 1] = bins[i];
                }
                bins[6] += over;
                bins[8] = over;
            }
            Console.WriteLine($"Day 256: {bins.Sum()}");
        }
    }
}
