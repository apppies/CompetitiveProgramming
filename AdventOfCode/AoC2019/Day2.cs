using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day2
    {
        string rawInput;
        public Day2()
        {
            rawInput = File.ReadAllText("input2.txt");
        }
        
        public  void Solve()
        {
            var input = rawInput.Split(',').Select(s => long.Parse(s.Trim())).ToArray();
            var program = (long[])input.Clone();
            program[1] = 12;
            program[2] = 2;
            var proc = new Intcode(program);
            proc.Run();
            Console.WriteLine($"Day 2 - 1: {proc.Memory[0]}");

            var target = 19690720;
            var found = false;
            int noun = 0;
            int verb = 0;
            for (noun = 0; noun < 100; noun++)
            {
                for (verb = 0; verb < 100; verb++)
                {
                    try
                    {
                        program[1] = noun;
                        program[2] = verb;
                        proc = new Intcode(program);

                        proc.Run();

                        if (proc.Memory[0] == target)
                        {
                            break;
                        }
                    }
                    catch { }
                }


                if (proc.Memory[0] == target)
                {
                    break;
                }
            }

            Console.WriteLine($"Day 2 - 2: {noun * 100 + verb}");
        }

         int[] IntcodeX(int[] code, int noun, int verb)
        {
            var mem = (int[])code.Clone();
            mem[1] = noun;
            mem[2] = verb;

            var p = 0;

            while (mem[p] != 99) //99 == halt
            {
                switch (mem[p])
                {
                    case 1: // Sum
                        mem[mem[p + 3]] = mem[mem[p + 1]] + mem[mem[p + 2]];
                        p += 4;
                        break;

                    case 2: // Mul
                        mem[mem[p + 3]] = mem[mem[p + 1]] * mem[mem[p + 2]];
                        p += 4;
                        break;

                    //case x:

                    default:
                        throw new InvalidOperationException($"Invalid opcode {mem[p]}");
                }
            }

            return mem;
        }


         string testInput1 = @"1,1,1,4,99,5,6,0,99";
         string testInput2 = @"1,9,10,3,2,3,11,0,99,30,40,50";
         string testInput3 = @"2,4,4,5,99,0";
    }
}
