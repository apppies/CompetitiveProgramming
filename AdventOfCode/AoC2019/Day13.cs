using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day13
    {
        string rawInput;
        public Day13()
        {
            rawInput = File.ReadAllText("input13.txt");
        }
        
        public  void Solve()
        {
            var proc = new Intcode(rawInput);
            proc.Run();

            var blocks = 0;
            for (int i = 0; i < proc.AllOutput.Count; i += 3)
            {
                if (proc.AllOutput[i + 2] == 2)
                    blocks++;
            }
            Console.WriteLine(blocks);

            proc = new Intcode("2" + rawInput.Substring(1));
            long score = 0;
            Console.Clear();
            Console.CursorVisible = false;

            var bot = true;

            while (!proc.Halted)
            {
                proc.Run();

                var screen = new Dictionary<(long, long), char>();
                while (proc.Output.Count > 0)
                {
                    var x = proc.Output.Dequeue();
                    var y = proc.Output.Dequeue();
                    var v = proc.Output.Dequeue();

                    if (x == -1)
                    {
                        score = v;
                    }
                    else
                    {
                        var s = ' ';
                        switch (v)
                        {
                            case 0:
                                s = ' ';
                                break;
                            case 1:
                                s = '█';
                                break;
                            case 2:
                                s = '#';
                                break;
                            case 3:
                                s = '═';
                                break;
                            case 4:
                                s = '■';
                                break;
                            default:
                                break;
                        }
                        if (!screen.ContainsKey((x, y)))
                            screen.Add((x, y), s);
                        else
                            screen[(x, y)] = s;
                    }
                }

                long ball = -1;
                long padd = -1;
                foreach (var item in screen)
                {
                    Console.SetCursorPosition((int)item.Key.Item1, (int)item.Key.Item2 + 1);
                    Console.Write(item.Value);

                    if (item.Value == '═')
                        padd = item.Key.Item1;
                    if (item.Value == '■')
                        ball = item.Key.Item1;
                }

                if (bot)
                {
                    // Bot mode
                    if (ball < padd)
                        proc.AddInput(-1);
                    else if (ball > padd)
                        proc.AddInput(1);
                    else
                        proc.AddInput(0);
                }
                else
                {
                    // Manual mode
                    var k = Console.ReadKey();
                    if (k.Key == ConsoleKey.LeftArrow)
                        proc.AddInput(-1);
                    else if (k.Key == ConsoleKey.UpArrow)
                        proc.AddInput(0);
                    else if (k.Key == ConsoleKey.RightArrow)
                        proc.AddInput(1);
                }

                Console.SetCursorPosition(0, 0);
                Console.Write("AoC Breakout    Auto    Score {0,6}", score);
            }

            Console.SetCursorPosition(14, 10);
            Console.Write("Game over");
        }
    }
}
