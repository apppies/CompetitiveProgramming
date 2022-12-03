using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day10
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input10.txt");
            var corruptScore = 0;
            var incompleteScores = new List<long>();
            for (int i = 0; i < input.Length; i++)
            {
                long incompleteScore = 0;
                var line = input[i];
                var open = new Stack<char>();
                var corrupt = ' ';
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == '(' || line[j] == '{' || line[j] == '[' || line[j] == '<')
                    {
                        open.Push(line[j]);
                    }
                    else
                    {
                        switch (line[j])
                        {
                            case ')':
                                if (open.Peek() != '(')
                                    corrupt = line[j];
                                else
                                    open.Pop();
                                break;
                            case ']':
                                if (open.Peek() != '[')
                                    corrupt = line[j];
                                else
                                    open.Pop();
                                break;
                            case '}':
                                if (open.Peek() != '{')
                                    corrupt = line[j];
                                else
                                    open.Pop();
                                break;
                            case '>':
                                if (open.Peek() != '<')
                                    corrupt = line[j];
                                else
                                    open.Pop();
                                break;
                            default:
                                break;
                        }
                    }

                    if (corrupt != ' ')
                        break;

                }
                if (corrupt != ' ')
                {
                    //Console.WriteLine($"Line {i}: found {corrupt} ");
                    switch (corrupt)
                    {
                        case ')':
                            corruptScore += 3;
                            break;
                        case ']':
                            corruptScore += 57;
                            break;
                        case '}':
                            corruptScore += 1197;
                            break;
                        case '>':
                            corruptScore += 25137;
                            break;
                        default:
                            break;
                    }
                }
                else if (open.Count > 0)
                {
                    var n = open.Count;
                    for (int j = n - 1; j >= 0; j--)
                    {
                        incompleteScore *= 5;
                        switch (open.Pop())
                        {
                            case '(':
                                incompleteScore += 1;
                                break;
                            case '[':
                                incompleteScore += 2;
                                break;
                            case '{':
                                incompleteScore += 3;
                                break;
                            case '<':
                                incompleteScore += 4;
                                break;
                            default:
                                break;
                        }
                    }
                }

                //Console.WriteLine(incompleteScore);
                if (incompleteScore > 0)
                    incompleteScores.Add(incompleteScore);
            }
            Console.WriteLine(corruptScore);
            incompleteScores.Sort();
            Console.WriteLine(incompleteScores[incompleteScores.Count / 2]);
        }
    }
}
