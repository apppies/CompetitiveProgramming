using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day5
    {
        string rawInput;
        public Day5()
        {
            rawInput = File.ReadAllText("input5.txt");
        }
        
        public  void Solve()
        {
            long[] program;
            Console.WriteLine("Test input");
            program = testInput.Split(',').Select(s => long.Parse(s.Trim())).ToArray();
            var proc = new Intcode(program);
            proc.RunWithInput(1);
            Console.WriteLine($"Day 5 - Test");

            program = rawInput.Split(',').Select(s => long.Parse(s.Trim())).ToArray();
            proc = new Intcode(program);
            proc.RunWithInput(1);
            Console.WriteLine($"Day 5 - 1: {proc.Output.Last()}");

            proc = new Intcode(program);
            proc.RunWithInput(5);
            Console.WriteLine($"Day 5 - 2: {proc.Output.Last()}");
        }

         readonly string testInput = @"1002,4,3,4,33";
    }
}
