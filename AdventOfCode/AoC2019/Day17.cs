using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day17
    {
        string rawInput;
        public Day17()
        {
            rawInput = File.ReadAllText("input17.txt");
        }
        
        public  void Solve()
        {
            var input = rawInput.Split(',').Select(c => int.Parse(c));
            var proc = new Intcode(rawInput);
            proc.Run();
            var w = proc.AllOutput.IndexOf(10);
            var h = proc.AllOutput.Count / w;
            var map = new int[w, h];
            var x = 0;
            var y = 0;
            var startX = 0;
            var startY = 0;

            foreach (var item in proc.AllOutput)
            {
                if (item == 10)
                {
                    y++;
                    x = 0;
                }
                else
                {
                    if (item == (int)'^')
                    {
                        startX = x;
                        startY = y;
                    }

                    map[x, y] = (int)item;
                    x++;
                }
            }

            // Determine crossings
            var crossings = 0;
            for (int i = 1; i < w - 1; i++)
            {
                for (int j = 1; j < h - 1; j++)
                {
                    if (map[i,j] == '#')
                    {
                        if (map[i-1,j] == map[i,j] && map[i + 1, j] == map[i, j] &&
                            map[i, j - 1] == map[i, j] && map[i, j + 1] == map[i, j])
                        {
                            crossings+= i*j;
                        }
                    }
                }
            }
            Console.WriteLine($"Crossings: {crossings}");

            DrawMap(map, w, h);

            // Determine full path
            var done = false;
            var dir = 'u';
            var path = "";
            x = startX;
            y = startY;
            while (!done)
            {
                var steps = 1;
                var turn = 'l';
                if (dir == 'u' || dir == 'd')
                {
                    if (x > 0 && map[x - 1, y] == '#')
                    {
                        if (dir == 'u')
                            turn = 'l';
                        else
                            turn = 'r';

                        dir = 'l';
                        while (x - steps - 1 >= 0 && map[x - steps - 1, y] == '#')
                        {
                            steps++;
                        }
                        x -= steps;
                    }
                    else if (x < w - 1 && map[x + 1, y] == '#')
                    {
                        if (dir == 'u')
                            turn = 'r';
                        else
                            turn = 'l';
                        dir = 'r';
                        while (x + steps + 1 < w && map[x + steps + 1, y] == '#')
                        {
                            steps++;
                        }
                        x += steps;
                    }
                    else
                        done = true;
                }
                else if (dir == 'l' || dir == 'r')
                {
                    if (y > 0 && map[x, y - 1] == '#')
                    {
                        if (dir == 'r')
                            turn = 'l';
                        else
                            turn = 'r';
                        dir = 'u';

                        while (y - steps - 1 >= 0 && map[x, y - steps - 1] == '#')
                        {
                            steps++;
                        }
                        y -= steps;
                    }
                    else if (y < h - 1 && map[x,y+1] == '#')
                    {
                        if (dir == 'r')
                            turn = 'r';
                        else
                            turn = 'l';
                        dir = 'd';
                        while (y + steps + 1 < h && map[x, y + steps + 1] == '#')
                        {
                            steps++;
                        }
                        y += steps;
                    }
                    else
                        done = true;
                }
                path += turn + steps.ToString();
            }
            Console.WriteLine(path);

            var rawInput2 = "2" + rawInput.Substring(1);
            var proc2 = new Intcode(rawInput2);
            proc2.AddInput(("A,B,A,C,A,B,C,C,A,B" + (char)10).Select(c => (long)c).ToArray());
            proc2.AddInput(("R,8,L,10,R,8" + (char)10).Select(c => (long)c).ToArray());
            proc2.AddInput(("R,12,R,8,L,8,L,12" + (char)10).Select(c => (long)c).ToArray());
            proc2.AddInput(("L,12,L,10,L,8" + (char)10).Select(c => (long)c).ToArray());
            proc2.AddInput((long)'y');
            proc2.AddInput(10);
            proc2.Run();


            x = 1;
            var size = (w + 1) * h;
            var offset = 0;

            Console.Clear();
            var mapdata = proc2.AllOutput.GetRange(offset, size).ToArray();
            DrawMap(GetMapFromOutput(mapdata, w , h ), w , h );
            offset += size;

            var moredata = new string(proc2.AllOutput.GetRange(offset, 67).Select(l => (char)l).ToArray());
            Console.WriteLine(moredata);
            offset += 68;
            Console.Clear();
            oldMap = null;
            while (offset < proc2.AllOutput.Count - 40)
            {
                mapdata = proc2.AllOutput.GetRange(offset, size).ToArray();
                DrawMap(GetMapFromOutput(mapdata, w, h), w, h);
                offset += size + 1;
                System.Threading.Thread.Sleep(10);
            }

            Console.SetCursorPosition(1, h + 2);
            Console.WriteLine($"Total dust: {proc2.AllOutput.Last()}");
        }

         int[,] GetMapFromOutput(long[] output, int w, int h)
        {
            var map = new int[w, h];
            int x = 0;
            int y = 0;
            foreach (var item in output)
            {
                if (item == 10)
                {
                    y++;
                    x = 0;
                }
                else
                {
                    map[x, y] = (int)item;
                    x++;
                }
            }
            return map;
        }

         int[,] oldMap = null;
         void DrawMap(int[,] map, int w, int h)
        {
            if (oldMap == null)
            {
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Console.Write((char)map[x, y]);

                    }
                    Console.WriteLine();
                }
            }
            else
            {
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        if (oldMap[x,y] != map[x, y])
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write((char)map[x, y]);
                        }

                    }
                }
            }
            oldMap = map;
        }
    }
}
