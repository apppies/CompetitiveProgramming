using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day2
    {
        public void Solve()
        {
            var sample = @"forward 5
down 5
forward 8
up 3
down 8
forward 2".Split(new char[] { '\n' }).Select(s => s.Split(new char[] { ' ' })).ToArray(); ;
            var input = System.IO.File.ReadAllLines("input2.txt").Select(s => s.Split(new char[] { ' ' })).ToArray();

            var h = 0; var d = 0;
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i][0])
                {
                    case "forward":
                        h += int.Parse(input[i][1]);
                        break;

                    case "down":
                        d += int.Parse(input[i][1]);
                        break;

                    case "up":
                        d -= int.Parse(input[i][1]);
                        break;

                    default:
                        break;
                }
            }

            Console.WriteLine($"Part 1: {d * h}");

            var aim = 0; h = 0; d = 0;
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i][0])
                {
                    case "forward":
                        var x = int.Parse(input[i][1]);
                        h += x;
                        d += aim * x;
                        break;

                    case "down":
                        aim += int.Parse(input[i][1]);
                        break;

                    case "up":
                        aim -= int.Parse(input[i][1]);
                        break;

                    default:
                        break;
                }
            }
            Console.WriteLine($"Part 2: {d * h}");
        }
    }
}
