using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day14
    {
       public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input14.txt");

            var t = input[0];
            var r = new Dictionary<string, string>();
            for (int i = 2; i < input.Length; i++)
            {
                r.Add(input[i].Substring(0, 2), input[i].Substring(6, 1));
            }

            var polymer = t;
            for (int i = 0; i < 10; i++)
            {
                var newpolymer = new StringBuilder();

                for (int j = 0; j < polymer.Length - 1; j++)
                {
                    var key = polymer.Substring(j, 2);
                    if (r.ContainsKey(key))
                    {
                        newpolymer.Append(polymer[j]);
                        newpolymer.Append(r[key]);
                    }
                    else
                    {
                        newpolymer.Append(polymer[j]);
                    }
                }

                polymer = newpolymer.ToString() + polymer.Last();
            }

            var counts = new Dictionary<char, long>();
            long minCount = long.MaxValue;
            long maxCount = 0;
            var minKey = ' ';
            var maxKey = ' ';

            for (int i = 0; i < polymer.Length; i++)
            {
                var key = polymer[i];
                if (!counts.ContainsKey(key))
                    counts.Add(key, 1);
                else
                    counts[key]++;
            }

            foreach (var item in counts)
            {
                if (item.Value > maxCount)
                {
                    maxCount = item.Value;
                    maxKey = item.Key;
                }
                if (item.Value < minCount)
                {
                    minCount = item.Value;
                    minKey = item.Key;
                }
            }

            Console.WriteLine(maxCount - minCount);

            // Better for 2: no string building
            //keep track of pairs present
            var p = new Dictionary<string, long>();
            for (int i = 0; i < t.Length - 1; i++)
            {
                var key1 = t.Substring(i, 2);
                if (!p.ContainsKey(key1))
                    p.Add(key1, 1);
                else
                    p[key1]++;
            }
            for (int i = 0; i < 40; i++)
            {
                var keys = p.Keys.ToList();
                var newP = new Dictionary<string, long>();
                foreach (var key in keys)
                {
                    var change = p[key];
                    if (r.ContainsKey(key))
                    {
                        var toAdd = r[key];
                        var key1 = key[0] + toAdd;
                        var key2 = toAdd + key[1];
                        if (!newP.ContainsKey(key1))
                            newP.Add(key1, change);
                        else
                            newP[key1] += change;
                        if (!newP.ContainsKey(key2))
                            newP.Add(key2, change);
                        else
                            newP[key2] += change;

                    }
                    else
                    {
                        if (!newP.ContainsKey(key))
                            newP.Add(key, change);
                        else
                            newP[key] += change;
                    }
                }
                p = newP;
            }

            var occur = new Dictionary<char, long>();
            foreach (var item in p)
            {
                foreach (var c in item.Key)
                {
                    if (!occur.ContainsKey(c))
                        occur.Add(c, item.Value);
                    else
                        occur[c] += item.Value;
                }
            }
            occur[t[0]]++;
            occur[t.Last()]++;


            minCount = long.MaxValue;
            maxCount = 0;
            foreach (var item in occur)
            {
                if (item.Value > maxCount)
                {
                    maxCount = item.Value;
                    maxKey = item.Key;
                }
                if (item.Value < minCount)
                {
                    minCount = item.Value;
                    minKey = item.Key;
                }
            }

            Console.WriteLine(maxCount / 2 - minCount / 2);
        }
    }
}
