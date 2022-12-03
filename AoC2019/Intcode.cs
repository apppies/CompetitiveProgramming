using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Intcode
    {
        public bool BlockInput { get; set; } = true;
        // Input
        private Queue<long> inputQueue = new Queue<long>();
        public int NeedInputCounter { get; set; } = 0;

        public void AddAsciiLine(string text)
        {
            AddInput(text.Select(c => (long)c));
            AddInput(10);
        }
        public void AddInput(long value)
        {
            inputQueue.Enqueue(value);
            NeedsInput = false;
        }
        public void AddInput(IEnumerable<long> values)
        {
            foreach (var value in values)
            {
                inputQueue.Enqueue(value);
            }
            NeedsInput = false;
        }

        internal bool Step()
        {
            // Determine opcode and instruction
            var instruction = Memory[p];
            var opcode = instruction % 100;
            var modes = GetModes(instruction);

            switch (opcode)
            {
                case 1: // Sum
                    SetMemory(p + 3, modes[2], GetMemory(p + 1, modes[0]) + GetMemory(p + 2, modes[1]));
                    p += 4;
                    break;

                case 2: // Multiply
                    SetMemory(p + 3, modes[2], GetMemory(p + 1, modes[0]) * GetMemory(p + 2, modes[1]));
                    p += 4;
                    break;

                case 3: // Input
                    if (BlockInput && inputQueue.Count == 0)
                    {
                        NeedsInput = true;
                        return false;
                    }
                    if (inputQueue.Count == 0)
                    {
                        SetMemory(p + 1, modes[0], -1);
                        NeedsInput = true;
                        NeedInputCounter++;
                    }
                    else
                    {
                        NeedInputCounter = 0;
                        SetMemory(p + 1, modes[0], inputQueue.Dequeue());
                        NeedsInput = false;
                    }
                    p += 2;
                    break;

                case 4: // Output
                    var value = GetMemory(p + 1, modes[0]);
                    AllOutput.Add(value);
                    Output.Enqueue(value);
                    //Console.WriteLine(value);
                    p += 2;
                    break;

                case 5: // JT
                    if (GetMemory(p + 1, modes[0]) != 0)
                    {
                        p = GetMemory(p + 2, modes[1]);
                    }
                    else
                    {
                        p += 3;
                    }
                    break;

                case 6: // JF
                    if (GetMemory(p + 1, modes[0]) == 0)
                    {
                        p = GetMemory(p + 2, modes[1]);
                    }
                    else
                    {
                        p += 3;
                    }
                    break;

                case 7: // <
                    if (GetMemory(p + 1, modes[0]) < GetMemory(p + 2, modes[1]))
                    {
                        SetMemory(p + 3, modes[2], 1);
                    }
                    else
                    {
                        SetMemory(p + 3, modes[2], 0);
                    }
                    p += 4;
                    break;

                case 8: // ==
                    if (GetMemory(p + 1, modes[0]) == GetMemory(p + 2, modes[1]))
                    {
                        SetMemory(p + 3, modes[2], 1);
                    }
                    else
                    {
                        SetMemory(p + 3, modes[2], 0);
                    }
                    p += 4;
                    break;

                case 9: // Change relative base
                    relativeBase += GetMemory(p + 1, modes[0]);
                    p += 2;
                    break;

                case 99: // Halt
                    Halted = true;
                    break;

                default:
                    throw new InvalidOperationException($"Invalid opcode {opcode}");
            }

            return true;
        }

        public bool NeedsInput { get; private set; }

        // Output
        public List<long> AllOutput { get; } = new List<long>();
        public Queue<long> Output { get; } = new Queue<long>();


        // State
        public bool Halted { get; set; }

        // Program memory
        private long[] originalMemory;
        public Dictionary<long, long> Memory { get; private set; }
        // Memory pointer
        private long p;
        private long relativeBase = 0;

        public Intcode(long[] program)
        {
            originalMemory = (long[])program.Clone();
            Reset();
        }

        public Intcode(string program)
        {
            originalMemory = program.Split(',').Select(s => long.Parse(s)).ToArray();
            Reset();
        }

        public void Reset()
        {
            Memory = new Dictionary<long, long>();
            for (int i = 0; i < originalMemory.Length; i++)
            {
                Memory.Add(i, originalMemory[i]);
            }
            p = 0;
            relativeBase = 0;
        }

        // Run 
        public bool RunWithInput(long value)
        {
            AddInput(value);
            return Run();
        }

        public bool RunWithInput(long[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                AddInput(values[i]);
            }

            return Run();
        }

        public bool Run()
        {
            while (Step() && !Halted)
            {
            }

            return Halted;
        }

        private long GetParam(long[] memory, long pointer, long mode)
        {
            if (mode == 0)
            {
                return memory[memory[pointer]];
            }
            else
            {
                return memory[pointer];
            }
        }

        long GetMemory(long p, long mode)
        {
            long key = -1;

            if (mode == 0)
            {
                key = GetMemory(p, 1);
            }
            else if (mode == 1)
            {
                key = p;
            }
            else if (mode == 2)
            {
                key = GetMemory(p, 1) + relativeBase;
            }

            if (key == -1)
            {
                throw new InvalidOperationException($"No valid mode specified: {mode}");
            }

            if (Memory.ContainsKey(key))
            {
                return Memory[key];
            }
            else
            {
                Memory.Add(key, 0);
                return 0;
            }
        }
        void SetMemory(long p, long mode, long value)
        {
            p = GetMemory(p, 1);
            if (mode == 2)
            {
                p += relativeBase;
            }

            if (!Memory.ContainsKey(p))
            {
                Memory.Add(p, value);
            }
            else
            {
                Memory[p] = value;
            }
        }




        private long[] GetModes(long instruction)
        {
            instruction /= 100;
            var size = 3;
            var modes = new long[size];
            for (int i = 0; i < size; i++)
            {
                modes[i] = instruction % 10;
                instruction /= 10;
            }
            return modes;
        }
    }
}
