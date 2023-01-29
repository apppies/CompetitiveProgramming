using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day15
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input15.txt");
            var w = input[0].Length;
            var h = input.Length;
            var map = new int[w * 5, h * 5];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    map[j, i] = (int)input[i][j] - (int)'0';
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    var o = i + j;
                    for (int i2 = 0; i2 < w; i2++)
                    {
                        for (int j2 = 0; j2 < h; j2++)
                        {
                            var n = map[i2, j2] + o;
                            if (n > 9)
                                n = n % 10 + 1;
                            map[i2 + i * w, j2 + h * j] = n;
                        }
                    }
                }
            }

            Solve(w, h, map);
            Solve(5 * w, 5 * h, map);
        }
        void Solve(int w, int h, int[,] map)
        {
            var Day15h = h;
            var Day15w = w;

            var minPath = new List<(int x, int y)>();
            var curPath = new Queue<(int x, int y)>();
            var costs = new int[Day15w, Day15h];
            var parents = new (int x, int y)[Day15w, Day15h];

            curPath.Enqueue((0, 0));
            while (curPath.Count > 0)
            {
                var current = curPath.Dequeue();
                var children = new List<(int x, int y)>();
                if (current.x < Day15w - 1)
                    children.Add((current.x + 1, current.y));
                if (current.y < Day15h - 1)
                    children.Add((current.x, current.y + 1));
                if (current.x > 0)
                    children.Add((current.x - 1, current.y));
                if (current.y > 0)
                    children.Add((current.x, current.y - 1));

                foreach (var child in children)
                {
                    if (costs[child.x, child.y] == 0 || costs[child.x, child.y] > costs[current.x, current.y] + map[child.x, child.y]) // did we get here cheaper
                    {
                        curPath.Enqueue(child);
                        costs[child.x, child.y] = costs[current.x, current.y] + map[child.x, child.y];
                        parents[child.x, child.y] = current;
                    }
                }
            }

            var node = (Day15w - 1, Day15h - 1);
            while (node.Item1 != 0 || node.Item2 != 0)
            {
                minPath.Add(node);
                node = parents[node.Item1, node.Item2];
            }

            long sum = 0;
            foreach (var p in minPath)
            {
                sum += map[p.Item1, p.Item2];
            }


            Console.WriteLine($"Answer: {sum}");
        }
    }
}
