using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2018
{
    class Day5
    {
        string input;
        public Day5()
        {
            input = File.ReadAllText("input5.txt");
        }
        
        public string AnswerA()
        {
            return Solve(input).Length.ToString();
        }

        string Solve(string input)
        {
            var found = true;
            var output = new StringBuilder();
            while (found)
            {
                output.Clear();
                found = false;
                for (int i = 0; i < input.Length; i++)
                {
                    if (i == input.Length - 1)
                    {
                        output.Append(input[i]);
                    }
                    else if (input[i] == input[i + 1] + 32 || input[i] == input[i + 1] - 32)
                    {
                        i++; // Skip next one
                        found = true; // Go for another round
                    }
                    else
                    {
                        output.Append(input[i]);
                    }
                }
                input = output.ToString();
            }
            return input;
        }

        public string AnswerB()
        {
            var minL = int.MaxValue;
            for (int i = 65; i < 91; i++)
            {
                var input2 = input.Replace(((char)i).ToString(), "").Replace(((char)(i + 32)).ToString(), "");
                var l = Solve(input2).Length;
                minL = Math.Min(l, minL);

            }
            return minL.ToString();
        }

        string testInput = @"dabAcCaCBAcCcaDA";
    }
}
