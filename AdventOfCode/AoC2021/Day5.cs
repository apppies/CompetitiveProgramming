using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day5
    {
        public void Solve()
        {
            var sample = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2".Split(new char[] { '\n' });
            //var input = sample.Select(s => s.Trim()).Select(s => s.Split(new char[] { ' ' }).Select(l => l.Split(new char[] { ',' }).Where(c => int.TryParse(c, out _)).Select(c => int.Parse(c)).ToArray()).ToArray()).ToArray();
            var input = System.IO.File.ReadAllLines("input5.txt").Select(s => s.Trim()).Select(s => s.Split(new char[] { ' ' }).Select(l => l.Split(new char[] { ',' }).Where(c => int.TryParse(c, out _)).Select(c => int.Parse(c)).ToArray()).ToArray()).ToArray();
            var field = new Dictionary<(int, int), int>();
            foreach (var item in input)
            {
                var x1 = Math.Min(item[0][0], item[2][0]);
                var y1 = Math.Min(item[0][1], item[2][1]);
                var x2 = Math.Max(item[0][0], item[2][0]);
                var y2 = Math.Max(item[0][1], item[2][1]);

                if (y1 == y2)
                {
                    for (int i = x1; i <= x2; i++)
                    {
                        if (!field.ContainsKey((i, y1)))
                            field.Add((i, y1), 1);
                        else
                            field[(i, y1)]++;
                    }
                }
                else if (x1 == x2)
                {
                    for (int i = y1; i <= y2; i++)
                    {
                        if (!field.ContainsKey((x1, i)))
                            field.Add((x1, i), 1);
                        else
                            field[(x1, i)]++;
                    }
                }
            }

            Console.WriteLine($"Overlaps >= 2: {field.Where(v => v.Value >= 2).Count()}");

            field = new Dictionary<(int, int), int>();
            foreach (var item in input)
            {
                var x1 = item[0][0];
                var y1 = item[0][1];
                var x2 = item[2][0];
                var y2 = item[2][1];

                if (x1 > x2)
                {   //Swap to always fill with increasing X
                    x1 = item[2][0];
                    y1 = item[2][1];
                    x2 = item[0][0];
                    y2 = item[0][1];
                }

                if (y1 == y2)
                {
                    for (int i = x1; i <= x2; i++)
                    {
                        if (!field.ContainsKey((i, y1)))
                            field.Add((i, y1), 1);
                        else
                            field[(i, y1)]++;
                    }
                }
                else if (x1 == x2)
                {
                    // Direction not important
                    y1 = Math.Min(item[0][1], item[2][1]);
                    y2 = Math.Max(item[0][1], item[2][1]);

                    for (int i = y1; i <= y2; i++)
                    {
                        if (!field.ContainsKey((x1, i)))
                            field.Add((x1, i), 1);
                        else
                            field[(x1, i)]++;
                    }
                }
                else
                {
                    for (int i = 0; i <= x2 - x1; i++)
                    {
                        var y = y1 + i;
                        if (y2 < y1)
                        {
                            y = y1 - i;
                        }

                        if (!field.ContainsKey((x1 + i, y)))
                            field.Add((x1 + i, y), 1);
                        else
                            field[(x1 + i, y)]++;
                    }
                }
            }
            Console.WriteLine($"Overlaps >= 2: {field.Where(v => v.Value >= 2).Count()}");
        }
    }
}
