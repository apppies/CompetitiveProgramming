using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day3
    {
        string rawInput;
        public Day3()
        {
            rawInput = File.ReadAllText("input3.txt");
        }
        
        public  void Solve()
        {
            var input = rawInput.Split('\n').Select(s => s.Trim().Split(',').ToArray()).ToArray();
            var map = new Dictionary<(int, int), int>();

            // Draw line 0
            var x = 0;
            var y = 0;
            var steps = 0;
            for (int i = 0; i < input[0].Length; i++)
            {
                var d = input[0][i][0];
                var s = int.Parse(input[0][i].Substring(1));
                for (int j = 0; j < s; j++)
                {
                    switch (d)
                    {
                        case 'R':
                            x++;
                            break;
                        case 'U':
                            y--;
                            break;
                        case 'L':
                            x--;
                            break;
                        case 'D':
                            y++;
                            break;
                    }
                    steps++;
                    if (!map.ContainsKey((x, y)))
                    {
                        map.Add((x, y), steps);
                    }
                }
            }

            x = 0; y = 0; steps = 0;
            var minDistance = int.MaxValue;
            var minSteps = int.MaxValue;
            for (int i = 0; i < input[1].Length; i++)
            {
                var d = input[1][i][0];
                var s = int.Parse(input[1][i].Substring(1));
                for (int j = 0; j < s; j++)
                {
                    switch (d)
                    {
                        case 'R':
                            x++;
                            break;
                        case 'U':
                            y--;
                            break;
                        case 'L':
                            x--;
                            break;
                        case 'D':
                            y++;
                            break;
                    }
                    steps++;
                    if (map.ContainsKey((x, y)))
                    {
                        var md = Math.Abs(x) + Math.Abs(y);
                        if (md < minDistance)
                            minDistance = md;            
                        
                        var ts =steps + map[(x,y)];
                        if (ts < minSteps)
                            minSteps = ts;
                    }
                }
            }
            Console.WriteLine($"Day 3 - 1: {minDistance}");
            Console.WriteLine($"Day 3 - 2: {minSteps}");
        }

         readonly string testInput = @"R75,D30,R83,U83,L12,D49,R71,U7,L72
U62,R66,U55,R34,D71,R55,D58,R83";
    }
}
