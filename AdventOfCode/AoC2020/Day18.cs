using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020
{
    class Day18
    {
        public void Solve()
        {
            var input = File.ReadAllText("input18.txt");
            var lines = input.Split(new char[] { '\n' }).Select(s => s.Trim()).ToList();

            long total = 0;
            foreach (var line in lines)
            {
                total += Calc(line);
                //Console.WriteLine(Calc(line));
            }
            Console.WriteLine($"Q1: {total}");


            total = 0;
            foreach (var line in lines)
            {
               // Console.WriteLine(Prep(line) + ":" + Calc(Prep(line)));
                total += Calc(Prep(line));
            }
            Console.WriteLine($"Q2: {total}");

            Console.ReadKey();
        }

        private static string Prep(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '+')
                {
                    // Find spots to insert brackets
                    // First (
                    var open = 0;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (j == 0)
                        {
                            line = '(' + line;
                            break;
                        }
                        else if (line[j] == ')')
                            open++;
                        else if (line[j] == '(')
                        {
                            open--;
                            if (open == 0)
                            {
                                line = line.Insert(j, "(");
                                break;
                            }
                        }
                        else if (line[j] >= 48 && line[j] <= 57 && open == 0)
                        {
                            line = line.Insert(j, "(");
                            break;
                        }
                    }

                    // Next )
                    open = 0;
                    for (int j = i + 2; j < line.Length; j++) // + 2 to correct for changed line by adding (
                    {
                        if (j == line.Length - 1)
                        {
                            line = line + ')';
                            break;
                        }
                        else if (line[j] == ')')
                        {
                            open--;
                            if (open == 0)
                            {
                                line = line.Insert(j + 1, ")");
                                break;
                            }
                        }
                        else if (line[j] == '(')
                            open++;
                        else if (line[j] >= 48 && line[j] <= 57 && open == 0)
                        {
                            line = line.Insert(j + 1, ")");
                            break;
                        }
                    }
                    i += 3;
                }
            }
            return line;
        }

        private static long Calc(string line)
        {
            bool first = true;
            long total = 0;
            long value = 0;
            char operand = '+';
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '+')
                {
                    operand = '+';
                    continue;
                }
                else if (line[i] == '*')
                {
                    operand = '*';
                    continue;
                }
                else if (line[i] == '(')
                {
                    // Find matching )
                    int open = 1;
                    int closeIndex = i + 1;
                    for (int j = i + 1; j < line.Length; j++)
                    {
                        if (line[j] == '(')
                            open++;
                        if (line[j] == ')')
                            open--;
                        if (open == 0)
                        {
                            closeIndex = j;
                            break;
                        }
                    }
                    value = Calc(line.Substring(i + 1, (closeIndex) - (i + 1)));
                    i = closeIndex;
                }
                else if (line[i] != ' ')
                    value = (int)(line[i] - 48);
                else
                    continue;

                if (first)
                {
                    total = value;
                    first = false;
                }
                else
                {
                    switch (operand)
                    {
                        case '+':
                            total += value;
                            break;
                        case '*':
                            total *= value;
                            break;

                        default:
                            break;
                    }
                }

            }
            return total;
        }

        static string test = @"2 * 3 + (4 * 5)
5 + (8 * 3 + 9 + 3 * 4 * 3)
5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))
((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";


    }
}
