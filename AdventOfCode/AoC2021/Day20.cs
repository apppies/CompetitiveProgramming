using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day20
    {
       public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input20.txt");
            var enhance = input[0];
            var map = new Dictionary<(int x, int y), char>();
            var oy = input.Length - 2;
            var ox = input[2].Length;
            for (int i = 2; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    map.Add((j, i - 2), input[i][j]);
                }
            }

            for (int y = 0; y < oy; y++)
            {
                for (int x = 0; x < ox; x++)
                {
                    Console.Write(map[(x, y)]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();



            for (int i = 0; i < 2; i++)
            {
                map = Enhance(map, enhance, i, ox, oy);
            }

            var c = map.Count(v => v.Value == '#');
            Console.WriteLine(c);


            for (int i = 2; i < 50; i++)
            {
                map = Enhance(map, enhance, i, ox, oy);
            }

            c = map.Count(v => v.Value == '#');
            Console.WriteLine(c);
        }

        Dictionary<(int x, int y), char> Enhance(Dictionary<(int x, int y), char> map, string enhance, int iteration, int ox, int oy)
        {
            var newmap = new Dictionary<(int x, int y), char>();

            for (int cx = -iteration - 1; cx < ox + iteration + 1; cx++)
            {
                for (int cy = -iteration - 1; cy < oy + iteration + 1; cy++)
                {
                    var binString = "";
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                            if (map.ContainsKey((cx + x, cy + y)))
                            {
                                var c = map[(cx + x, cy + y)];
                                if (c == '.')
                                    binString += '0';
                                else
                                    binString += '1';
                            }
                            else
                            {
                                if (enhance[0] == '.')
                                {
                                    binString += '0';
                                }
                                else
                                {
                                    if (iteration % 2 == 0)
                                        binString += '0';
                                    else
                                        binString += '1';// after 1 iteration all dark is lit
                                }
                            }
                        }
                    }
                    var newValue = enhance[Convert.ToInt32(binString, 2)];
                    newmap.Add((cx, cy), newValue);
                }
            }


            return newmap;
        }
    }
}
