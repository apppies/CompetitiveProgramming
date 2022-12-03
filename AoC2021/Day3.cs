using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day3
    {
        public void Solve()
        {

            var sample = @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010".Split(new char[] { '\n' }).Select(s => s.Trim()).ToArray(); ;
            var input = System.IO.File.ReadAllLines("input3.txt").Select(s => s.Trim()).ToArray();

            //input = sample;

            var n = input.Length;
            var w = input[0].Length;
            var ones = new int[w];
            for (int i = 0; i < n; i++)
            {
                var l = input[i];
                for (int j = 0; j < w; j++)
                {
                    if (l[j] == '1')
                        ones[j]++;
                }
            }

            var gamma = "";
            var epsilon = "";
            for (int i = 0; i < w; i++)
            {
                if (ones[i] >= n / 2)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }
            }

            Console.WriteLine($"Gamma: {gamma}");
            Console.WriteLine($"Epsilon: {epsilon}");
            Console.WriteLine($"Power: {Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2) }");

            var oxygen = GetOxygen(input, 0);
            var co2 = GetCO2(input, 0);

            Console.WriteLine($"Oxygen: {oxygen}");
            Console.WriteLine($"CO2: {co2}");
            Console.WriteLine($"Life support: {Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2) }");




        }

        string GetOxygen(IEnumerable<string> source, int index)
        {
            if (source.Count() == 1)
            {
                return source.First();
            }
            else
            {
                var ones = source.Count(s => s[index] == '1');
                if (ones * 2 >= source.Count())
                    return GetOxygen(source.Where(s => s[index] == '1'), index + 1);
                else
                {
                    return GetOxygen(source.Where(s => s[index] == '0'), index + 1);
                }
            }
        }
        string GetCO2(IEnumerable<string> source, int index)
        {
            if (source.Count() == 1)
            {
                return source.First();
            }
            else
            {
                var zeros = source.Count(s => s[index] == '0');
                if (zeros * 2 <= source.Count())
                    return GetCO2(source.Where(s => s[index] == '0'), index + 1);
                else
                {
                    return GetCO2(source.Where(s => s[index] == '1'), index + 1);
                }
            }
        }
    }
}
