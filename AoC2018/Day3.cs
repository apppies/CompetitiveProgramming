using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2018
{
    class Day3
    {
        string input;
        public Day3()
        {
            input = File.ReadAllText("input3.txt");
        }

        public string AnswerA()
        {
            var lines = input.Split('\n').Select(s => s.Trim()).ToArray();

            var matrix = new int[1000 * 1000];
            foreach (var line in lines)
            {
                var info = line.Split(' ').ToArray();
                var x = int.Parse(info[2].Split(',')[0]);
                var y = int.Parse(info[2].Split(',')[1].Replace(':', ' '));
                var w = int.Parse(info[3].Split('x')[0]);
                var h = int.Parse(info[3].Split('x')[1]);
                FillSquare(x, y, w, h, matrix, 1000);
            }

            //Count
            return matrix.Count(f => f > 1).ToString();
        }

        private void FillSquare(int x, int y, int w, int h, int[] matrix, int matrixw)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    matrix[(x + i) * matrixw + y + j]++;
                }
            }
        }

        public string AnswerB()
        {
            var lines = input.Split('\n').Select(s => s.Trim()).ToArray();
            var overridden = new int[lines.Length];
            var matrix = new int[1000* 1000];

            foreach (var line in lines)
            {
                var info = line.Split(' ').ToArray();
                var n = int.Parse(info[0].Substring(1));
                var x = int.Parse(info[2].Split(',')[0]);
                var y = int.Parse(info[2].Split(',')[1].Replace(':', ' '));
                var w = int.Parse(info[3].Split('x')[0]);
                var h = int.Parse(info[3].Split('x')[1]);
                FillSquareB(x, y, w, h, matrix, 1000, n, overridden);
            }

            for (int i = 0; i < overridden.Length; i++)
            {
                if (overridden[i] == 0)
                    return i.ToString();
            }
            return "-1";
        }

        private void FillSquareB(int x, int y, int w, int h, int[] matrix, int matrixw, int nr, int[] overridden)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    int c = (x + i) * matrixw + y + j;
                    if (matrix[c] > 0)
                    {
                        overridden[matrix[c] - 1] = 1;
                        overridden[nr - 1] = 1;
                    }
                    else
                    {
                        matrix[c] = nr;
                    }
                }
            }
        }


        string testinput = @"#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2
#4 @ 0,0: 4x3";

    }
}
