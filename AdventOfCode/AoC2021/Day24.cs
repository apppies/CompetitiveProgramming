using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day24
    {
        enum OPCODE
        {
            inp,
            add,
            mod,
            mul,
            div,
            eql
        }

        Dictionary<char, long> registers = new();
        public void Solve()
        {
            registers.Add('w', 0);
            registers.Add('x', 0);
            registers.Add('y', 0);
            registers.Add('z', 0);

            var instructions = File.ReadAllLines("input24.txt");
            var program = new List<(OPCODE Opcode, char R1, char R2, int V2)>();
            for (int i = 0; i < instructions.Length; i++)
            {
                var x = instructions[i].Split(' ');
                var opcode = (OPCODE)Enum.Parse(typeof(OPCODE), x[0]);
                var r1 = x[1][0];
                var r2 = (char)0;
                var v2 = 0;
                if (x.Length > 2)
                {
                    if (x[2].Length == 1 && registers.ContainsKey(x[2][0]))
                        r2 = x[2][0];
                    else
                        v2 = int.Parse(x[2]);
                }
                program.Add((opcode, r1, r2, v2));
            }

            Console.WriteLine($"Sample 13579246899999 : {MONAD(program, "13579246899999")}");
            FastMonad(program);
        }

        void FastMonad(List<(OPCODE Opcode, char R1, char R2, int V2)> program)
        {
            var s = new Stack<(int, long)>();
            var part1 = new long[14];
            var part2 = new long[14];
            for (int i = 0; i < 14; i++)
            {
                if (program[ 18 * i + 4].V2 == 1)
                {
                    s.Push((i, program[18 * i + 15].V2));
                }
                else
                {
                    var (j, n) = s.Pop();
                    n += program[18 * i + 5].V2;
                    if (n > 0)
                    {
                        part1[i] = 9;
                        part1[j] = 9 - n;
                        part2[i] = 1 + n;
                        part2[j] = 1;
                    }
                    else
                    {
                        part1[i] = 9 + n;
                        part1[j] = 9;
                        part2[i] = 1;
                        part2[j] = 1 - n;
                    }
                }
            }
            Console.WriteLine($"Part 1: {string.Join("", part1)}");
            Console.WriteLine($"Part 2: {string.Join("", part2)}");
        }
        
        long MONAD(List<(OPCODE Opcode, char R1, char R2, int V2)> program, string input)
        {
            var inputPointer = 0;
            registers['w'] = 0;
            registers['x'] = 0;
            registers['y'] = 0;
            registers['z'] = 0;

            for (int i = 0; i < program.Count; i++)
            {
                switch (program[i].Opcode)
                {
                    case OPCODE.inp:
                        registers[program[i].R1] = int.Parse(input[inputPointer].ToString());
                        inputPointer++;
                        break;
                    case OPCODE.add:
                        if (program[i].R2 != 0)
                            registers[program[i].R1] = registers[program[i].R1] + registers[program[i].R2];
                        else
                            registers[program[i].R1] = registers[program[i].R1] + program[i].V2;
                        break;
                    case OPCODE.mod:
                        if (program[i].R2 != 0)
                            registers[program[i].R1] = registers[program[i].R1] % registers[program[i].R2];
                        else
                            registers[program[i].R1] = registers[program[i].R1] % program[i].V2;
                        break;
                    case OPCODE.mul:
                        if (program[i].R2 != 0)
                            registers[program[i].R1] = registers[program[i].R1] * registers[program[i].R2];
                        else
                            registers[program[i].R1] = registers[program[i].R1] * program[i].V2;
                        break;
                    case OPCODE.div:
                        if (program[i].R2 != 0)
                            registers[program[i].R1] = (int)Math.Floor(registers[program[i].R1] / (1.0* registers[program[i].R2]));
                        else
                            registers[program[i].R1] = (int)Math.Floor(registers[program[i].R1] / (1.0 * program[i].V2));
                        break;
                    case OPCODE.eql:
                        if (program[i].R2 != 0)
                            registers[program[i].R1] = registers[program[i].R1] == registers[program[i].R2] ? 1 : 0;
                        else
                            registers[program[i].R1] = registers[program[i].R1] == program[i].V2 ? 1 : 0;
                        break;
                    default:
                        Console.WriteLine($"Unknown opcode at {i}: {program[i].Opcode}");
                        break;
                }
            }
            return registers['z'];
        }
    }
}
