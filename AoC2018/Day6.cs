using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2018
{
    class Day6
    {
        string rawInput;
        public Day6()
        {
            rawInput = File.ReadAllText("input6.txt");
        }
        
        public string AnswerA()
        {
            var input = rawInput.Split('\n').Select(s => s.Trim().Split(", ")).Select(a => (int.Parse(a[0]),int.Parse(a[1]), false)).ToArray();
            var minX = input.Select(a => a.Item1).Min();
            var maxX = input.Select(a => a.Item1).Max();
            var minY = input.Select(a => a.Item2).Min();
            var maxY = input.Select(a => a.Item2).Max();
            
            // Initialize field
            var field = new ValueTuple<int,int>[maxX + 1, maxY + 1];
            for (int x = 0; x < maxX + 1; x++)
            {
                for (int y = 0; y < maxY + 1; y++)
                {
                    field[x, y] = (int.MaxValue, -1);
                }
            }

            // Calculate field
            for (int i = 0; i < input.Length; i++)
            {
                var item = input[i];
                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        var d = Math.Abs(x - item.Item1) + Math.Abs(y - item.Item2);
                        if (field[x,y].Item1 > d)
                        {
                            field[x, y] = (d, i);
                        }
                        else if (field[x,y].Item1 == d)
                        {
                            field[x, y] = (d, -1);
                        }
                    }
                }
            }

            // Throw out infinites
            var toRemove = new HashSet<int>();
            toRemove.Add(-1);
            for (int x = minX; x < maxX + 1; x++)
            {
                toRemove.Add(field[x, minY].Item2);
                toRemove.Add(field[x, maxY].Item2);
            }
            for (int y = minY; y < maxY + 1; y++)
            {
                toRemove.Add(field[minX, y].Item2);
                toRemove.Add(field[maxX, y].Item2);
            }

            var leftOvers = from (int, int) item in field where (!toRemove.Contains(item.Item2)) select item.Item2;
            var counts = from item in leftOvers group item by item into g let count = g.Count() orderby count descending select (g.Key, count);
            
            return counts.First().count.ToString();
        }

        public string AnswerB()
        {
            var input = rawInput.Split('\n').Select(s => s.Trim().Split(", ")).Select(a => (int.Parse(a[0]), int.Parse(a[1]), false)).ToArray();
            var minX = input.Select(a => a.Item1).Min();
            var maxX = input.Select(a => a.Item1).Max();
            var minY = input.Select(a => a.Item2).Min();
            var maxY = input.Select(a => a.Item2).Max();

            // Initialize field
            var field = new int[maxX + 1, maxY + 1];
            for (int x = 0; x < maxX + 1; x++)
            {
                for (int y = 0; y < maxY + 1; y++)
                {
                    field[x, y] = -1;
                }
            }

            // Calculate field
            for (int i = 0; i < input.Length; i++)
            {
                var item = input[i];
                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        var d = Math.Abs(x - item.Item1) + Math.Abs(y - item.Item2);
                        if (field[x, y] == -1)
                            field[x, y] = 0;
                        field[x, y] += d;
                    }
                }
            }

            
            var limit = 10000;
            var count = from int x in field where x < limit && x >= 0 select x;
            return count.Count().ToString();
        }

        string testInput = @"1, 1
1, 6
8, 3
3, 4
5, 5
8, 9";
        
    }
}
