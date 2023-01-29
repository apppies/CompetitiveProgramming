using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AoC2021
{
    class Day202223
    {
        class Elf
        {
            public Elf(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int x;
            public int y;
        }

        void drawmap(List<Elf> elves)
        {
            int minx = elves[0].x;
            int miny = elves[0].y;
            int maxx = elves[0].x;
            int maxy = elves[0].y;
            var map = new HashSet<(int, int)>();
            foreach (var elf in elves)
            {
                minx = Math.Min(minx, elf.x);
                maxx = Math.Max(maxx, elf.x);
                miny = Math.Min(miny, elf.y);
                maxy = Math.Max(maxy, elf.y);
                map.Add((elf.x, elf.y));
            }

            for (int y = miny; y <= maxy; y++)
            {
                for (int x = minx; x <= maxx; x++)
                {
                    if (map.Contains((x, y)))
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        internal void Solve()
        {
            var input = System.IO.File.ReadAllLines("input23.txt");
            var currentmap = new HashSet<(int, int)>();

            var elves = new List<Elf>();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        var newelf = new Elf(x, y);
                        elves.Add(newelf);
                        currentmap.Add((x, y));//, new List<Elf>() { newelf });
                    }
                }
            }

            int loopcount = 10;
            int roundcount = 0;
            while(true)
            {
                int nomove = 0;
                var newmap = new Dictionary<(int, int), List<Elf>>();
                foreach (var elf in elves)
                {
                    var w = (elf.x - 1, elf.y);
                    var nw = (elf.x - 1, elf.y - 1);
                    var n = (elf.x, elf.y - 1);
                    var ne = (elf.x + 1, elf.y - 1);
                    var e = (elf.x + 1, elf.y);
                    var se = (elf.x + 1, elf.y + 1);
                    var s = (elf.x, elf.y + 1);
                    var sw = (elf.x - 1, elf.y + 1);
                    var elfmoved = false;

                    if (!(currentmap.Contains(nw) || currentmap.Contains(n) || currentmap.Contains(ne) || currentmap.Contains(e) ||
                          currentmap.Contains(w) || currentmap.Contains(sw) || currentmap.Contains(s) || currentmap.Contains(se)))
                    {
                        elfmoved = false;
                    }
                    else
                    {
                        for (int c = 0; c < 4; c++)
                        {
                            int tocheck = (roundcount + c) % 4;
                            if (tocheck == 0)
                            {
                                if (!(currentmap.Contains(nw) || currentmap.Contains(n) || currentmap.Contains(ne)))
                                {
                                    if (!newmap.ContainsKey(n))
                                    {
                                        newmap.Add(n, new List<Elf>());
                                    }
                                    newmap[n].Add(elf);
                                    elfmoved = true;
                                    break;
                                }
                            }
                            else if (tocheck == 1)
                            {
                                if (!(currentmap.Contains(sw) || currentmap.Contains(s) || currentmap.Contains(se)))
                                {
                                    if (!newmap.ContainsKey(s))
                                    {
                                        newmap.Add(s, new List<Elf>());
                                    }
                                    newmap[s].Add(elf);
                                    elfmoved = true;
                                    break;
                                }
                            }
                            else if (tocheck == 2)
                            {
                                if (!(currentmap.Contains(sw) || currentmap.Contains(w) || currentmap.Contains(nw)))
                                {
                                    if (!newmap.ContainsKey(w))
                                    {
                                        newmap.Add(w, new List<Elf>());
                                    }
                                    newmap[w].Add(elf);
                                    elfmoved = true;
                                    break;
                                }
                            }
                            else if (tocheck == 3)
                            {
                                if (!(currentmap.Contains(se) || currentmap.Contains(e) || currentmap.Contains(ne)))
                                {
                                    if (!newmap.ContainsKey(e))
                                    {
                                        newmap.Add(e, new List<Elf>());
                                    }
                                    newmap[e].Add(elf);
                                    elfmoved = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (elfmoved == false)
                    {
                        if (!newmap.ContainsKey((elf.x, elf.y)))
                        {
                            newmap.Add((elf.x, elf.y), new List<Elf>());
                        }
                        newmap[(elf.x, elf.y)].Add(elf);
                        nomove++;
                    }
                }

                foreach (var p in newmap)
                {
                    if (p.Value.Count == 1)
                    {
                        foreach (var e in p.Value)
                        {
                            e.x = p.Key.Item1;
                            e.y = p.Key.Item2;
                        }
                    }
                }

                currentmap.Clear();
                foreach (var e in elves)
                {
                    currentmap.Add((e.x, e.y));//, e);
                }



                // drawmap(elves);
                roundcount++;
                if (nomove == elves.Count)
                {
                    break;
                }
            }


            int minx = elves[0].x;
            int miny = elves[0].y;
            int maxx = elves[0].x;
            int maxy = elves[0].y;
            foreach (var elf in elves)
            {
                minx = Math.Min(minx, elf.x);
                maxx = Math.Max(maxx, elf.x);
                miny = Math.Min(miny, elf.y);
                maxy = Math.Max(maxy, elf.y);
            }

            Console.WriteLine($"Part 1: { (maxx - minx + 1) * (maxy - miny + 1) - elves.Count}");
            Console.WriteLine($"Part 2: {roundcount}");
        }


    }
}
