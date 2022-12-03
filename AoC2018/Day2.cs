using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AoC2018
{
    class Day2
    {
        string input;
        public Day2()
        {
            input = File.ReadAllText("input2.txt");
        }
        
        public string AnswerA()
        {
            int countDouble = 0;
            int countTriple = 0;
            foreach (var line in input.Split('\n'))
            {
                var counts = line.GroupBy(c => c);
                if (counts.Any(x => x.Count() == 2))
                    countDouble++;
                if (counts.Any(x => x.Count() == 3))
                    countTriple++;
            }
            return (countDouble * countTriple).ToString();

        }

        public string AnswerB()
        {
            var lines = input.Split('\n').Select(s => s.Trim()).ToArray();
            var answerline = "";
            for (int i = 0; i < lines.Length - 1; i++)
            {
                for (int j = i+1; j < lines.Length- 1; j++)
                {
                    var l1 = lines[i];
                    var l2 = lines[j];
                    int difference = 0;
                    for (int k = 0; k < lines[i].Length; k++)
                    {
                        if (lines[i][k] != lines[j][k])
                            difference++;

                        if (difference > 1)
                            break;
                    }
                    if (difference == 1)
                    {
                        answerline = new string(lines[i].Intersect(lines[j]).OrderBy(c => c).ToArray());
                     //   return answerline;
                    }
                }


            }
            return answerline;
        }

        string testinput = @"abcdef
bababc
abbcde
abcccd
aabcdd
abcdee
ababab";

        string testinputB = @"abcde
fghij
klmno
pqrst
fguij
axcye
wvxyz";

    }
}
