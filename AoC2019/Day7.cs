using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day7
    {
        string rawInput;
        public Day7()
        {
            rawInput = File.ReadAllText("input7.txt");
        }
        
         string testInput = @"3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
         string testInput2 = @"3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";
         string testInput4 = @"3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
         string testInput5 = @"3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";
        public  void Solve()
        {
            var input = rawInput.Split(',').Select(s => long.Parse(s.Trim())).ToArray();

            var procs = new Intcode[5];

            var phases = Permute(new int[] { 0, 1, 2, 3, 4 }).Select(a => a.ToArray());
            var maxOutput = long.MinValue;
            foreach (var phase in phases)
            {
                for (int i = 0; i < 5; i++)
                {
                    procs[i] = new Intcode(input);
                    procs[i].RunWithInput(phase[i]);
                }

                long start = 0;
                for (int i = 0; i < 5; i++)
                {
                    procs[i].RunWithInput(start);
                    start = procs[i].Output.Last();
                }
                maxOutput = Math.Max(maxOutput, start);
            }

            Console.WriteLine($"Max: {maxOutput}");


            phases = Permute(new int[] { 5, 6, 7, 8, 9 }).Select(a => a.ToArray());
            maxOutput = long.MinValue;
            foreach (var phase in phases)
            {
                long start = 0;

                // Initialize all procs
                for (int i = 0; i < 5; i++)
                {
                    procs[i] = new Intcode(input);
                    procs[i].RunWithInput(phase[i]);
                }

                bool halt = false;
                while (!halt)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        procs[i].RunWithInput(start);
                        if (procs[i].Output.Count > 0)
                        {
                            start = procs[i].Output.Last();
                        }
                        if (procs[i].Halted)
                        {
                            if (i == 4)
                                halt = true;
                        }
                    }
                }
                maxOutput = Math.Max(maxOutput, start);
            }
            Console.WriteLine($"Max: {maxOutput}");
        }

        public  IEnumerable<IEnumerable<T>> Permute<T>(IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                yield break;
            }

            var list = sequence.ToList();

            if (!list.Any())
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var startingElementIndex = 0;

                foreach (var startingElement in list)
                {
                    var index = startingElementIndex;
                    var remainingItems = list.Where((e, i) => i != index);

                    foreach (var permutationOfRemainder in Permute(remainingItems))
                    {
                        yield return Concat(startingElement, permutationOfRemainder);
                    }

                    startingElementIndex++;
                }
            }
        }

        private  IEnumerable<T> Concat<T>(T firstElement, IEnumerable<T> secondSequence)
        {
            yield return firstElement;
            if (secondSequence == null)
            {
                yield break;
            }

            foreach (var item in secondSequence)
            {
                yield return item;
            }
        }
    }
}
