using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2018
{
    class Day1
    {
        string input;
        public Day1()
        {
            input = File.ReadAllText("input1.txt");
        }
        
        public string AnswerA()
        {
            long answer = 0;
            foreach (var line in input.Split('\n'))
            {
                long value = long.Parse(line.Substring(1));
                switch (line[0])
                {
                    case '-':
                        answer -= value;
                        break;
                    case '+':
                        answer += value;
                        break;
                    default:
                        break;
                }
            }
            return answer.ToString();
        }

        public string AnswerB()
        {
            var table = new HashSet<long>();
            long answer = 0;
            while (true)
            {

            foreach (var line in input.Split('\n'))
            {
                long value = long.Parse(line.Substring(1));
                switch (line[0])
                {
                    case '-':
                        answer -= value;
                        break;
                    case '+':
                        answer += value;
                        break;
                    default:
                        break;
                }
                if (!table.Add(answer))
            return answer.ToString();



                }
            }
        }

        
    }
}
