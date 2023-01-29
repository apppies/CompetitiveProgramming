using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day8
    {
        string rawInput;
        public Day8()
        {
            rawInput = File.ReadAllText("input8.txt");
        }
        
        public  void Solve()
        {
            int w = 25;
            int h = 6;

            int min0 = int.MaxValue;
            int minI = 0;
            for (int i = 0; i < rawInput.Length / (w * h); i++)
            {
                int c0 = 0;
                for (int j = 0; j < w * h; j++)
                {
                    if (rawInput[i * w * h + j] == '0')
                        c0++;
                }
                if (c0 < min0)
                {
                    min0 = c0;
                    minI = i;
                }
            }

            int c1 = 0;
            int c2 = 0;
            for (int i = 0; i < w*h; i++)
            {
                if (rawInput[minI * w * h + i] == '1')
                    c1++;

                else if (rawInput[minI * w * h + i] == '2')
                    c2++;
            }
            Console.WriteLine(c1 * c2);

            for (int i = 0; i < w*h; i++)
            {
                if (i % w == 0)
                    Console.WriteLine();

                for (int j = 0; j < rawInput.Length / (w*h); j++)
                {
                    var c = rawInput[j * w * h + i];
                    if (c == '2')
                        continue;
                    else if (c == '1')
                        Console.Write("█");
                    else if (c == '0')
                        Console.Write(" ");
                    break;
                }

            }

            // Second method

            // Parse input
            var layers = new List<string>();
            for (int i = 0; i < rawInput.Length; i += w * h)
            {
                layers.Add(rawInput.Substring(i, w * h));
            }

            // Verify
            var minLayer = layers.Aggregate((minItem, nextItem) => (minItem.Count(c => c == '0') < nextItem.Count(c => c == '0')) ? minItem : nextItem);
            var count1 = minLayer.Count(x => x == '1');
            var count2 = minLayer.Count(x => x == '2');
            Console.WriteLine(count1 * count2);

            // Display
            for (int i = 0; i < layers[0].Length; i++)
            {
                if (i % w == 0)
                    Console.WriteLine();

                for (int j = 0; j < layers.Count; j++)
                {
                    if (layers[j][i] == '2')
                        continue;
                    else if (layers[j][i] == '1')
                        Console.Write("█");
                    else if (layers[j][i] == '0')
                        Console.Write("▒");
                    break;
                }
            }
            Console.WriteLine();

        }
    }
}
