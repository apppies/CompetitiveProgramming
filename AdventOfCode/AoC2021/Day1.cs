using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day1
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input1.txt").Select(s => int.Parse(s)).ToArray();
            var sample = @"199
200
208
210
200
207
240
269
260
263".Split(new char[] { '\n' }).Select(s => int.Parse(s.Trim())).ToArray();
            var larger = 0;
            var smaller = 0;
            var equal = 0;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] > input[i - 1])
                    larger++;
                else if (input[i] < input[i - 1])
                    smaller++;
                else
                    equal++;
            }

            Console.WriteLine($"Part 1: {larger}");

            var sum1 = input[0] + input[1] + input[2];
            var sum2 = input[1] + input[2] + input[3];
            var sumlarger = 0;
            if (sum2 > sum1)
                sumlarger++;
            for (int i = 4; i < input.Length; i++)
            {
                sum1 += input[i - 1] - input[i - 4];
                sum2 += input[i] - input[i - 3];

                if (sum2 > sum1)
                    sumlarger++;
            }

            Console.WriteLine($"Part 2: {sumlarger}");
        }
    }
}
