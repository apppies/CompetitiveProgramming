using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day8
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input8.txt").Select(s => s.Split(new char[] { '|' })).Select(a => a[1].Split(new char[] { ' ' })).ToArray();
            var count = new int[10];
            for (int i = 0; i < input.GetUpperBound(0); i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j].Length == 2)
                        count[1]++;
                    else if (input[i][j].Length == 4)
                        count[4]++;
                    else if (input[i][j].Length == 3)
                        count[7]++;
                    else if (input[i][j].Length == 7)
                        count[8]++;
                }
            }
            Console.WriteLine(count.Sum());
            long total = 0;
            input = System.IO.File.ReadAllLines("input8.txt").Select(s => s.Replace("| ", "").Split(new char[] { ' ' }).Select(s2 => new string(s2.OrderBy(c => c).ToArray())).ToArray()).ToArray();
            for (int i = 0; i <= input.GetUpperBound(0); i++)
            {
                //
                //    0
                //  1   2
                //    3
                //  4   5
                //    6
                //
                var segments = new string[] { "abcdefg", "abcdefg", "abcdefg", "abcdefg", "abcdefg", "abcdefg", "abcdefg" };
                for (int j = 0; j < input[i].Length; j++)
                {
                    var toRemove = new int[0];
                    var val = input[i][j];
                    if (input[i][j].Length == 2) // == 1
                    {
                        toRemove = new int[] { 0, 1, 3, 4, 6 };
                        segments[2] = RemoveOthers(segments[2], input[i][j]);
                        segments[5] = RemoveOthers(segments[5], input[i][j]);
                    }
                    else if (input[i][j].Length == 4) // 4
                    {
                        toRemove = new int[] { 0, 4, 6 };
                        segments[1] = RemoveOthers(segments[1], input[i][j]);
                        segments[2] = RemoveOthers(segments[2], input[i][j]);
                        segments[3] = RemoveOthers(segments[3], input[i][j]);
                        segments[5] = RemoveOthers(segments[5], input[i][j]);
                    }
                    else if (input[i][j].Length == 3) // 7
                    {
                        toRemove = new int[] { 1, 3, 4, 6 };
                        segments[0] = RemoveOthers(segments[0], input[i][j]);
                        segments[2] = RemoveOthers(segments[2], input[i][j]);
                        segments[5] = RemoveOthers(segments[5], input[i][j]);
                    }

                    if (toRemove.Length > 0)
                    {
                        for (int k = 0; k < segments.Length; k++)
                        {
                            if (toRemove.Contains(k))
                                segments[k] = RemoveThese(segments[k], input[i][j]);
                            else
                                segments[k] = RemoveOthers(segments[k], input[i][j]);
                        }
                    }
                }


                //Doubles: remove all occurences of a two character segment in the other segments apart from the other one segment with the same characters
                for (int j = 0; j < segments.Length; j++)
                {
                    if (segments[j].Length == 2)
                    {
                        for (int k = 0; k < segments.Length; k++)
                        {
                            if (j != k && segments[j] == segments[k])
                            {
                                for (int l = 0; l < segments.Length; l++)
                                {
                                    if (l != j && l != k)
                                    {
                                        for (int q = 0; q < segments[j].Length; q++)
                                        {
                                            segments[l] = segments[l].Replace(segments[j][q].ToString(), "");

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                // Singles
                for (int j = 0; j < segments.Length; j++)
                {
                    if (segments[j].Length == 1)
                    {
                        for (int k = 0; k < segments.Length; k++)
                        {
                            if (j != k)
                            {
                                segments[k] = segments[k].Replace(segments[j], "");
                            }
                        }
                    }
                }


                // Search for 0: determine segments 1 and 3
                var zero = new string((segments[0] + segments[2] + segments[4] + segments[1][0]).ToCharArray().OrderBy(c => c).ToArray());
                if (input[i].Contains(zero))
                {
                    segments[1] = segments[1][0].ToString();
                    segments[3] = segments[3][1].ToString();
                }
                else
                {
                    segments[1] = segments[1][1].ToString();
                    segments[3] = segments[3][0].ToString();
                }


                // Search for three: determine segments 4 and 6
                var three = new string((segments[0] + segments[2] + segments[3] + segments[6][0]).ToCharArray().OrderBy(c => c).ToArray());
                if (input[i].Contains(three))
                {
                    segments[6] = segments[6][0].ToString();
                    segments[4] = segments[4][1].ToString();
                }
                else
                {
                    segments[6] = segments[6][1].ToString();
                    segments[4] = segments[4][0].ToString();
                }

                // Search for two: determine segments 2 and 5
                var two = new string((segments[0] + segments[1] + segments[3] + segments[6] + segments[5][0]).ToCharArray().OrderBy(c => c).ToArray());
                if (input[i].Contains(two))
                {
                    segments[5] = segments[5][0].ToString();
                    segments[2] = segments[2][1].ToString();
                }
                else
                {
                    segments[5] = segments[5][1].ToString();
                    segments[2] = segments[2][0].ToString();
                }

                // Decode inputs
                var numbers = new List<string> {
                    segments[0]+segments[1]+segments[2]+segments[4]+segments[5]+segments[6], // 0
                    segments[2]+segments[5], // 1
                    segments[0]+segments[2]+segments[3]+segments[4]+segments[6], // 2
                    segments[0]+segments[2]+segments[3]+segments[5]+segments[6], // 3
                    segments[1]+segments[2]+segments[3]+segments[5], // 4
                    segments[0]+segments[1]+segments[3]+segments[5]+segments[6], // 5
                    segments[0]+segments[1]+segments[3]+segments[4]+segments[5]+segments[6], // 6
                    segments[0]+segments[2]+segments[5], // 7
                    segments[0]+segments[1]+segments[2]+segments[3]+segments[4]+segments[5]+segments[6], // 8
                    segments[0]+segments[1]+segments[2]+segments[3]+segments[5]+segments[6] // 9
                };
                for (int j = 0; j < numbers.Count; j++)
                {
                    numbers[j] = new string(numbers[j].OrderBy(c => c).ToArray());
                }

                var n = input[i].Length;
                var sum = numbers.IndexOf(input[i][n - 4]) * 1000 + numbers.IndexOf(input[i][n - 3]) * 100 + numbers.IndexOf(input[i][n - 2]) * 10 + numbers.IndexOf(input[i][n - 1]) * 1;
                total += sum;
            }
            Console.WriteLine(total);
        }

        string RemoveOthers(string source, string toKeep)
        {
            var output = "";
            foreach (var c in source)
            {
                if (toKeep.Contains(c))
                {
                    output += c;
                }
            }
            return output;
        }

        string RemoveThese(string source, string toRemove)
        {
            var output = "";
            foreach (var c in source)
            {
                if (!toRemove.Contains(c))
                {
                    output += c;
                }
            }
            return output;
        }
    }
}
