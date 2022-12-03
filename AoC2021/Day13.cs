using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day13
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input13.txt");
            var map = new List<(int x, int y)>();

            Console.Clear();
            for (int i = 0; i < input.Length; i++)
            {
                var line = input[i];
                if (line.Contains(','))
                {
                    var xy = line.Split(new char[] { ',' });
                    map.Add((int.Parse(xy[0]), int.Parse(xy[1])));
                }
                else if (line.Contains("fold"))
                {
                    var axis = line.Substring("fold along ".Length, 1);
                    var index = int.Parse(line.Substring("fold along x=".Length));

                    if (axis == "x")
                    {
                        for (int j = 0; j < map.Count; j++)
                        {
                            if (map[j].x >= index)
                            {
                                map[j] = (index - (map[j].x - index), map[j].y);
                            }
                        }
                    }
                    else if (axis == "y")
                    {
                        for (int j = 0; j < map.Count; j++)
                        {
                            if (map[j].y >= index)
                            {
                                map[j] = (map[j].x, index - (map[j].y - index));
                            }
                        }
                    }

                    
                    Console.WriteLine($"Dot count: {map.Distinct().Count()}");
                }
            }

            //Show map
            var offset = Console.CursorTop + 1;
            for (int i = 0; i < map.Count; i++)
            {
                Console.SetCursorPosition(map[i].x, map[i].y + offset);
                Console.Write("#");
            }
            Console.SetCursorPosition(0, offset + map.Max(v => v.y) + 1);
        }
    }
}
