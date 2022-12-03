using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day9
    {
        static int[,] day9map;
        static int day9w;
        static int day9h;
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input9.txt");
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

            var lowPoints = new Dictionary<(int, int), int>();
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    var lowest = true;
                    if (x > 0 && map[x, y] >= map[x - 1, y])
                        lowest = false;
                    if (x < w - 1 && map[x, y] >= map[x + 1, y])
                        lowest = false;
                    if (y > 0 && map[x, y] >= map[x, y - 1])
                        lowest = false;
                    if (y < h - 1 && map[x, y] >= map[x, y + 1])
                        lowest = false;
                    if (lowest)
                    {
                        lowPoints.Add((x, y), map[x, y]);
                        Console.WriteLine($"{x},{y}: {map[x, y]}");
                    }
                }
            }
            var risksum = lowPoints.Select(s => s.Value + 1).Sum();
            Console.WriteLine($"Risk level sum: {risksum}");

            day9map = map;
            day9h = h;
            day9w = w;
            var sizes = new List<int>();
            var identifier = 100;

            foreach (var lowPoint in lowPoints)
            {
                if (day9map[lowPoint.Key.Item1, lowPoint.Key.Item2] < 9) // Not already filled by another basin
                {
                    sizes.Add(GetBasin(lowPoint.Key, 1, identifier));
                }
                identifier++;
            }
            sizes = sizes.OrderByDescending(i => i).ToList();
            Console.WriteLine(sizes[0] * sizes[1] * sizes[2]);


        }

        int GetBasin((int, int) start, int cursize, int identifier)
        {
            var (x, y) = start;
            day9map[x, y] = identifier;
            var newsize = cursize;
            if (x > 0 && day9map[x - 1, y] < 9)
            {
                newsize = GetBasin((x - 1, y), newsize + 1, identifier);
            }
            if (x < day9w - 1 && day9map[x + 1, y] < 9)
            {
                newsize = GetBasin((x + 1, y), newsize + 1, identifier);
            }
            if (y > 0 && day9map[x, y - 1] < 9)
            {
                newsize = GetBasin((x, y - 1), newsize + 1, identifier);
            }
            if (y < day9h - 1 && day9map[x, y + 1] < 9)
            {
                newsize = GetBasin((x, y + 1), newsize + 1, identifier);
            }
            return newsize;
        }
    }
}
