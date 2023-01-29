using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day1
    {
        string rawInput;
        public Day1()
        {
            rawInput = File.ReadAllText("input1.txt");
        }
        
        public void Solve()
        {
            var input = rawInput.Split(new char[] { '\n' }).Select(s => int.Parse(s)).ToArray();
            int fuel = 0;
            for (int i = 0; i < input.Length; i++)
            {
                fuel += getFuel(input[i]);
            }
            Console.WriteLine($"Day 1 - 1 (For): {fuel}");
            //fuel = input.Aggregate(0, (sum, n) => sum + getFuel(n));
            //Console.WriteLine($"Day 1 - 1 (Linq): {fuel}");

            fuel = 0;
            for (int i = 0; i < input.Length; i++)
            {
                fuel += getFuelWithFuel(input[i]);
            }
            Console.WriteLine($"Day 1 - 2: {fuel}");
        }

        int getFuel(int mass)
        {
            return (int)Math.Floor(mass / 3.0) - 2;
        }

        int getFuelWithFuel(int mass)
        {
            int fuel = 0;
            while ((mass = getFuel(mass)) > 0)
            {
                fuel += mass;
            }
            return fuel;
        }

    }
}
