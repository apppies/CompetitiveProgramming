using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020
{
    class Day13
    {
        public void Solve()
        {
            var input = File.ReadAllText("input13.txt");
            var lines = input.Split(new char[] { '\n' }).Select(s => s.Trim()).ToArray();

            var start = int.Parse(lines[0]);
            var busses = lines[1].Split(new char[] { ',' }).Where(c => c != "x").Select(s => int.Parse(s)).ToArray();

            var minWait = int.MaxValue;
            var nextBus = 0;
            foreach (var bus in busses)
            {
                var next = (start / bus + 1) * bus;
                var wait = next - start;
                if (wait < minWait)
                {
                    minWait = wait;
                    nextBus = bus;
                }
            }

            Console.WriteLine($"Next bus {nextBus} in { minWait}: {nextBus * minWait} ");

            lines = test.Split(new char[] { '\n' }).Select(s => s.Trim()).ToArray();
            var busses2 = lines[1].Split(new char[] { ',' }).Select(s => s == "x" ? 1 : long.Parse(s)).ToArray();

            //make wolfram query
            var q = "";
            var v = 'A';
            for (int i = 0; i < busses2.Length; i++)
            {
                if (busses2[i] != 1)
                {
                    q += $"t = {busses2[i]}{v} - {i}; ";
                    v++;
                    if (v == 'e')
                        v++;
                    if (v == 'i' || v == 'I')
                        v = (char)((int)v + 2);
                }
            }

            Console.WriteLine(q);

            lines = input.Split(new char[] { '\n' }).Select(s => s.Trim()).ToArray();
            busses2 = lines[1].Split(new char[] { ',' }).Select(s => s == "x" ? 1 : long.Parse(s)).ToArray();

            //make wolfram query
            q = "";
            v = 'A';
            for (int i = 0; i < busses2.Length; i++)
            {
                if (busses2[i] != 1)
                {
                    q += $"t = {busses2[i]}{v} - {i}; ";
                    v++;
                    if (v == 'e')
                        v++;
                    if (v == 'i' || v == 'I')
                        v = (char)((int)v + 2);
                }
            }

            Console.WriteLine("Insert into Wolfram Alpha and get the offset for t");
            Console.WriteLine(q);
            Console.ReadKey();
        }


        static string test = @"939
7,13,x,x,59,x,31,19";
    }
}
