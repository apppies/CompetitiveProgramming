using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day7
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input7.txt")[0].Split(new char[] { ',' }).Select(c => int.Parse(c)).ToList();
            //input = new List<int>() { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
            input.Sort();
            int median = -1;
            if (input.Count % 2 == 0)
            {
                median = input[input.Count / 2];
            }
            else
            {
                median = input[input.Count / 2] + input[input.Count / 2 + 1];
            }
            Console.WriteLine($"Median {median}");

            long fuel = 0;
            for (int i = 0; i < input.Count; i++)
            {
                fuel += Math.Abs(input[i] - median);
            }
            Console.WriteLine($"Fuel at {median}: {fuel}");

            // New fuel costs
            // cost = dist * (dist +  1) / 2
            var minFuel = long.MaxValue; var minFuelIndex = -1;
            var m = input.Max();
            for (int x = 0; x < m; x++)
            {

                fuel = 0;
                for (int i = 0; i < input.Count; i++)
                {
                    var dist = Math.Abs(input[i] - x);
                    fuel += dist * (dist + 1) / 2;
                }
                if (fuel < minFuel)
                {
                    minFuel = fuel;
                    minFuelIndex = x;
                }
            }
            Console.WriteLine($"Fuel at {minFuelIndex}: {minFuel}");

        }
    }
}
