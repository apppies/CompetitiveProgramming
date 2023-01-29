using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day23
    {
        string rawInput;
        public Day23()
        {
            rawInput = File.ReadAllText("input23.txt");
        }
        
        public  void Solve()
        {
            var procs = new Intcode[50];
            for (int i = 0; i < 50; i++)
            {
                procs[i] = new Intcode(rawInput) { BlockInput = false };
                procs[i].AddInput(i);
            }

            var running = true;
            bool partOne = true;
            var NAT = (-1l, -1l);
            var NATSent = (-2l, -2l);
            while (running)
            {
                var needInput = true;
                for (int i = 0; i < 50; i++)
                {
                    procs[i].Step();
                    needInput &= (procs[i].NeedsInput && procs[i].Output.Count == 0);
                }

                for (int i = 0; i < 50; i++)
                {
                    if (procs[i].Output.Count >= 3)
                    {
                        var address = procs[i].Output.Dequeue();
                        var x = procs[i].Output.Dequeue();
                        var y = procs[i].Output.Dequeue();
                        if (address == 255)
                        {
                            //Part one
                            if (partOne)
                                Console.WriteLine(y);
                            partOne = false;
                            //running = false;
                            //break;

                            // Part two
                            NAT = (x, y);
                        }
                        else
                        {
                            procs[address].AddInput(x);
                            procs[address].AddInput(y);
                        }
                    }

                }


                if (needInput)
                {
                    procs[0].AddInput(NAT.Item1);
                    procs[0].AddInput(NAT.Item2);
                    //Console.WriteLine(NAT);

                    if (NAT.Item2 == NATSent.Item2)
                    {
                        Console.WriteLine(NATSent.Item2);
                        running = false;
                    }

                    NATSent = NAT;
                }
            }
        }
    }
}
