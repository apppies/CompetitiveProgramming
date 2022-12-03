using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day21
    {
        string rawInput;
        public Day21()
        {
            rawInput = File.ReadAllText("input21.txt");
        }
        
        public  void Solve()
        {
            var proc = new Intcode(rawInput);
            proc.AddAsciiLine("NOT B T");  // Jump if B == false or C == false is a hole and D == true is solid
            proc.AddAsciiLine("OR T J");

            proc.AddAsciiLine("NOT C T");
            proc.AddAsciiLine("OR T J");

            proc.AddAsciiLine("AND D J");

            proc.AddAsciiLine("NOT A T"); // Jump if A == false is a hole
            proc.AddAsciiLine("OR T J");
            
            proc.AddAsciiLine("WALK");
            proc.Run();
            for (int i = 0; i < proc.AllOutput.Count; i++)
            {
                if (proc.AllOutput[i] == 10)
                    Console.WriteLine();
                else
                    Console.Write((char)proc.AllOutput[i]);
            }
            Console.WriteLine(); 
            Console.WriteLine(proc.AllOutput.Last());

            proc = new Intcode(rawInput);
            proc.AddAsciiLine("NOT B T");  // Jump if B == false or C == false is a hole and D == true and H == true is solid
            proc.AddAsciiLine("OR T J");

            proc.AddAsciiLine("NOT C T");
            proc.AddAsciiLine("OR T J");

            proc.AddAsciiLine("AND D J");
            proc.AddAsciiLine("AND H J");

            proc.AddAsciiLine("NOT A T"); // Jump if A == false is a hole
            proc.AddAsciiLine("OR T J");

            proc.AddAsciiLine("RUN");
            proc.Run();
            for (int i = 0; i < proc.AllOutput.Count; i++)
            {
                if (proc.AllOutput[i] == 10)
                    Console.WriteLine();
                else
                    Console.Write((char)proc.AllOutput[i]);
            }
            Console.WriteLine();
            Console.WriteLine(proc.AllOutput.Last());
        }
    }
}
