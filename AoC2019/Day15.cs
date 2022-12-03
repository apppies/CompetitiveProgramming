using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day15
    {
        string rawInput;
        public Day15()
        {
            rawInput = File.ReadAllText("input15.txt");
        }
        
        class Location
        {
            //public int X { get; set; }
            //public int Y { get; set; }
            //public (int, int) XY { get { return (X, Y); } }
            public int Entry { get; set; }
            public int Exit { get; set; }
            public char Value { get; set; }
            public int Steps { get; set; }

        }

        public  void Solve()
        {

            var map = new Dictionary<(int, int), Location>();
            var size = 30;
            var proc = new Intcode(rawInput);

            var movement = new Stack<int>();

            var run = true;
            var x = 0;
            var y = 0;
            var lastX = x;
            var lastY = y;
            var nextMove = 1;
            map.Add((0, 0), new Location() { Value = 'X', Entry = -1, Exit = nextMove });

            int counter = 0;
            while (counter < 100000)
            {
                counter++;
                lastX = x;
                lastY = y;
                if (nextMove == 1)
                    y++;
                else if (nextMove == 2)
                    y--;
                else if (nextMove == 3)
                    x--;
                else if (nextMove == 4)
                    x++;

                long output;
                if (Math.Abs(x) > size || Math.Abs(y) > size)
                {
                    //limit screen
                    output = 0;
                }
                else
                {
                    proc.RunWithInput(nextMove);
                    output = proc.Output.Dequeue();
                }
                if (output == 0) // Hit wall, try different move
                {
                    // Add wall
                    if (!map.ContainsKey((x, y)))
                        map.Add((x, y), new Location() { Value = 'W' });

                    // Reset location
                    x = lastX;
                    y = lastY;
                }
                else if (output == 1)
                {
                    if (!map.ContainsKey((x, y)))
                        map.Add((x, y), new Location() { Entry = nextMove, Exit = 0, Value = '.', Steps = map[(lastX, lastY)].Steps + 1 });

                    map[(x, y)].Steps = Math.Min(map[(lastX, lastY)].Steps + 1, map[(x, y)].Steps); // Update steps if we visit again
                }
                else
                {
                    if (!map.ContainsKey((x, y)))
                        map.Add((x, y), new Location() { Entry = nextMove, Exit = 0, Value = 'O', Steps = map[(lastX, lastY)].Steps + 1 });

                    map[(x, y)].Steps = Math.Min(map[(lastX, lastY)].Steps + 1, map[(x, y)].Steps); // Update steps if we visit again

                    Console.WriteLine("Found location");
                }

                // Determine new move, scout more, or go back
                if (map[(x, y)].Entry == -1) // Start 
                {
                    nextMove = map[(x, y)].Exit++;
                    if (nextMove == 5)
                    {
                        break;
                    }
                }
                else if (map[(x, y)].Entry == 1) // Entry north
                {
                    if (!map.ContainsKey((x, y + 1)))
                        nextMove = 1;
                    else if (!map.ContainsKey((x - 1, y)))
                        nextMove = 3;
                    else if (!map.ContainsKey((x + 1, y)))
                        nextMove = 4;
                    else
                        nextMove = 2;
                }
                else if (map[(x, y)].Entry == 2) // Entry south
                {
                    if (!map.ContainsKey((x, y - 1)))
                        nextMove = 2;
                    else if (!map.ContainsKey((x - 1, y)))
                        nextMove = 3;
                    else if (!map.ContainsKey((x + 1, y)))
                        nextMove = 4;
                    else
                    {
                        nextMove = 1;
                    }
                }
                else if (map[(x, y)].Entry == 3) // Entry west
                {
                    if (!map.ContainsKey((x - 1, y)))
                        nextMove = 3;
                    else if (!map.ContainsKey((x, y + 1)))
                        nextMove = 1;
                    else if (!map.ContainsKey((x, y - 1)))
                        nextMove = 2;
                    else
                        nextMove = 4;
                }
                else if (map[(x, y)].Entry == 4) // Entry east
                {
                    if (!map.ContainsKey((x + 1, y)))
                        nextMove = 4;
                    else if (!map.ContainsKey((x, y + 1)))
                        nextMove = 1;
                    else if (!map.ContainsKey((x, y - 1)))
                        nextMove = 2;
                    else
                        nextMove = 3;
                }
            }

            Console.Write("");

            for (int i = -size; i < size; i++)
            {
                for (int j = -size; j < size; j++)
                {
                    if (map.ContainsKey((j, i)))
                    {
                        if (map[(j, i)].Value == '.')
                            Console.Write(map[(j, i)].Steps % 10);
                        else
                            Console.Write(map[(j, i)].Value);
                    }
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Steps {map.Where(l => l.Value.Value == 'O').Select(m => m.Value.Steps).First()}");

            // Fill map from oxygen unit
            var map2 = new int[size  * 2, size * 2];
            foreach (var item in map)
            {
                if (item.Value.Value == 'W')
                    map2[item.Key.Item1+size, item.Key.Item2 + size] = -1;
                else if (item.Value.Value == 'O')
                    map2[item.Key.Item1 + size, item.Key.Item2 + size] = 0;
                else
                    map2[item.Key.Item1 + size, item.Key.Item2 + size] = -2;
            }

            var xy = map.Where(l => l.Value.Value == 'O').Select(m => m.Key).First();
            var todo = new List<(int, int)>();
            todo.Add((xy.Item1 + size, xy.Item2 + size));
            var c = 0;
            while (todo.Count > 0)
            {
                var next = new List<(int, int)>();
                foreach (var item in todo)
                {
                    next.AddRange(FillMap(map2, item.Item1, item.Item2, c));
                }
                todo = next;
                c++;
            }

            Console.WriteLine($"Time: {map2.Cast<int>().Max()}");

            for (int i = 0; i < size*2; i++)
            {
                for (int j = 0; j < size*2; j++)
                {
                    if (map2[j, i] == -1)
                        Console.Write('#');
                    else if (map2[j, i] == 0)
                        Console.Write(' ');
                    else
                        Console.Write(map2[j, i] % 10);
                }
                Console.WriteLine();
            }
        }

         List<(int,int)> FillMap(int[,] map, int x, int y, int c)
        {
            var ret = new List<(int, int)>();
            if (map[x + 1,y] == -2 || map[x + 1, y] > c + 1)
            {
                map[x + 1, y] = c + 1;
                ret.Add((x + 1, y));
            }
            if (map[x - 1, y] == -2 || map[x - 1, y] > c + 1)
            {
                map[x - 1, y] = c + 1;
                ret.Add((x - 1, y));
            }
            if (map[x, y+1] == -2 || map[x, y + 1] > c + 1)
            {
                map[x, y+1] = c + 1;
                ret.Add((x , y + 1));
            }
            if (map[x, y - 1] == -2 || map[x, y - 1] > c + 1)
            {
                map[x, y - 1] = c + 1;
                ret.Add((x, y - 1));
            }
            return ret;
        }
    }
}




//var lastMove = nextMove;
//                    if (map[(x, y)].Entry == 1) // Previous move was north
//                    {
//                        if (lastMove == 1) // and north is blocked
//                        {
//                            nextMove = 3; // Try east
//                        }
//                        else if (lastMove == 3) // and east is blocked
//                        {
//                            nextMove = 4; // Try west
//                        }
//                        else if (lastMove == 4)  // We tried all moves, dead end, go back
//                        {
//                            map[(x, y)].Value = 'D'; // Dead end
//                            nextMove = 2; // Go back
//                        }
//                    }
//                    else if (map[(x, y)].Entry == 2) //Previous move was south
//                    {
//                        if (lastMove == 2) // and south is blocked
//                        {
//                            nextMove = 3; // Try east
//                        }
//                        else if (lastMove == 3) // and east is blocked
//                        {
//                            nextMove = 4; // Try west
//                        }
//                        else if (lastMove == 4)  // We tried all moves, dead end, go back
//                        {
//                            map[(x, y)].Value = 'D'; // Dead end
//                            nextMove = 1; // Go back
//                        }
//                    }
//                    else if (map[(x, y)].Entry == 3) //Previous move was east
//                    {
//                        if (lastMove == 3) // and east is blocked
//                        {
//                            nextMove = 1; // Try north
//                        }
//                        else if (lastMove == 1) // and north is blocked
//                        {
//                            nextMove = 2; // Try south
//                        }
//                        else if (lastMove == 2)  // We tried all moves, dead end, go back
//                        {
//                            map[(x, y)].Value = 'D'; // Dead end
//                            nextMove = 4; // Go back
//                        }
//                    }
//                    else if (map[(x, y)].Entry == 4) //Previous move was west
//                    {
//                        if (lastMove == 3) // and west is blocked
//                        {
//                            nextMove = 1; // Try north
//                        }
//                        else if (lastMove == 1) // and north is blocked
//                        {
//                            nextMove = 2; // Try south
//                        }
//                        else if (lastMove == 2)  // We tried all moves, dead end, go back
//                        {
//                            map[(x, y)].Value = 'D'; // Dead end
//                            nextMove = 3; // Go back
//                        }
//                    }