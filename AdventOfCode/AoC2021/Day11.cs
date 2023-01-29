using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day11
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input11.txt");
            var w = input[0].Length;
            var h = input.Length;
            var map = new int[w, h];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    map[j, i] = (int)input[i][j] - (int)'0';
                }
            }

            long totalFlashCount = 0;
            int s = 0;
            while (true)
            {

                // Increase all by one
                // Check for flashes
                var flashes = new List<(int x, int y)>();
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        map[j, i]++;
                        if (map[j, i] == 10)
                            flashes.Add((j, i));
                    }
                }

                // While new flashes occur
                while (flashes.Count > 0)
                {
                    var newFlashes = new List<(int x, int y)>();
                    for (int f = 0; f < flashes.Count; f++)
                    {
                        // Up all neighbors
                        // Check for new flashes
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                if (i != 0 || j != 0)
                                {
                                    var x = flashes[f].x + i;
                                    var y = flashes[f].y + j;
                                    if (x >= 0 && y >= 0 && x < w && y < h)
                                    {
                                        map[x, y]++;
                                        if (map[x, y] == 10)
                                            newFlashes.Add((x, y));
                                    }
                                }
                            }
                        }
                        totalFlashCount++;
                    }
                    flashes = newFlashes;
                }

                // Erase flashed
                var roundFlashCount = 0;
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {

                        if (map[j, i] > 9)
                        {
                            map[j, i] = 0;
                            roundFlashCount++;
                        }
                    }
                }
                if (roundFlashCount == w * h)
                {
                    Console.WriteLine($"Everybody flashed in round: {s + 1}");
                    break;
                }

                if (s == 99)
                {
                    Console.WriteLine($"Round 100 total flash count: {totalFlashCount}");
                }
                s++;
            }
        }
    }
}
