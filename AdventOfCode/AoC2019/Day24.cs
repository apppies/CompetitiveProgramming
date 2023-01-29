using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day24
    {
        string rawInput;
        public Day24()
        {
            rawInput = File.ReadAllText("input24.txt");
        }
        
        class Level
        {
            public int[] map = null;
            public int[] buffer = null;
            public Level Up { get; set; }
            public Level Down { get; set; }

            int w;
            int h;
            public Level(int w, int h)
            {
                map = new int[w * h];
                buffer = new int[w * h];
                this.w = w;
                this.h = h;
            }

            public int BugCount = 0;

            public void Set(int x, int y, int value)
            {
                if (value == 1)
                    BugCount++;
                else
                    BugCount--;


            }

            public void Get(int x, int y)
            {

            }

            public void Calculate()
            {
                /*
                            07                
                      00 01 02 03 04   
                      05 06 07 08 09   
                11    10 11 ?? 13 14   13
                      15 16 17 18 19   
                      20 21 22 23 24
                            17
                         
                */
                //          Up          Left      Right     Down
                buffer[0] = Up.map[7] + Up.map[11] + map[1] + map[5];
                buffer[1] = Up.map[7] + map[0] + map[2] + map[6];
                buffer[2] = Up.map[7] + map[1] + map[3] + map[7];
                buffer[3] = Up.map[7] + map[2] + map[4] + map[8];
                buffer[4] = Up.map[7] + map[3] + Up.map[13] + map[9];

                buffer[5] = map[0] + Up.map[11] + map[6] + map[10];
                buffer[6] = map[1] + map[5] + map[7] + map[11];
                buffer[7] = map[2] + map[6] + map[8] + (Down.map[0] + Down.map[1] + Down.map[2] + Down.map[3] + Down.map[4]);
                buffer[8] = map[3] + map[7] + map[9] + map[13];
                buffer[9] = map[4] + map[8] + Up.map[13] + map[14];

                buffer[10] = map[5] + Up.map[11] + map[11] + map[15];
                buffer[11] = map[6] + map[10] + (Down.map[0] + Down.map[5] + Down.map[10] + Down.map[15] + Down.map[20]) + map[16];
                //buffer[12]
                buffer[13] = map[8] + (Down.map[4] + Down.map[9] + Down.map[14] + Down.map[19] + Down.map[24]) + map[14] + map[18];
                buffer[14] = map[9] + map[13] + Up.map[13] + map[19];

                buffer[15] = map[10] + Up.map[11] + map[16] + map[20];
                buffer[16] = map[11] + map[15] + map[17] + map[21];
                buffer[17] = (Down.map[20] + Down.map[21] + Down.map[22] + Down.map[23] + Down.map[24]) + map[16] + map[18] + map[22];
                buffer[18] = map[13] + map[17] + map[19] + map[23];
                buffer[19] = map[14] + map[18] + Up.map[13] + map[24];

                buffer[20] = map[15] + Up.map[11] + map[21] + Up.map[17];
                buffer[21] = map[16] + map[20] + map[22] + Up.map[17];
                buffer[22] = map[17] + map[21] + map[23] + Up.map[17];
                buffer[23] = map[18] + map[22] + map[24] + Up.map[17];
                buffer[24] = map[19] + map[23] + Up.map[13] + Up.map[17];
            }
            public void Update()
            {
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        if (map[i + j * h] == 1 && buffer[i + h * j] != 1)
                            map[i + j * h] = 0;
                        else if (map[i + j * h] == 0 && (buffer[i + h * j] == 1 || buffer[i + h * j] == 2))
                            map[i + j * h] = 1;
                    }
                }
            }

        }

        public  void Solve()
        {
            SolveA();
            SolveB();
        }

        public  void SolveB()
        {

            var w = 5;
            var h = 5;
            var time = 200;
            var levels = new List<Level>();
            var upperLevel = new Level(w, h);
            var lowerLevel = new Level(w, h);
            levels.Add(new Level(w, h) { Up = upperLevel });
            for (int i = 0; i < (time + 2); i++)
            {
                levels.Add(new Level(w, h));
                levels[i + 1].Up = levels[i];
                levels[i].Down = levels[i + 1];
            }
            levels[(time + 2)].Down = lowerLevel;

            var input = rawInput.Split('\n').ToArray();
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    levels[(time + 2) / 2].map[i + j * h] = input[j][i] == '.' ? 0 : 1;
                }
            }

            for (int t = 0; t < time; t++)
            {
                foreach (var level in levels)
                {
                    level.Calculate();
                }

                foreach (var level in levels)
                {
                    level.Update();
                }
            }

            Console.WriteLine(levels.Aggregate(0, (sum, level) => sum += level.map.Sum()));

        }
        public  void SolveA()
        {
            var w = 5;
            var h = 5;
            var input = rawInput.Split('\n').ToArray();
            var map = new int[w * h];
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    map[i + j * h] = input[j][i] == '.' ? 0 : 1;
                }
            }
            var buffer = new int[map.Length];

            var maps = new HashSet<long>();

            var hash = GetHash(map);
            while (!maps.Contains(hash))
            {
                maps.Add(hash);
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        var neighbours = 0;
                        if (i > 0)
                            neighbours += map[(i - 1) + h * j];
                        if (i < w - 1)
                            neighbours += map[(i + 1) + h * j];
                        if (j > 0)
                            neighbours += map[i + h * (j - 1)];
                        if (j < h - 1)
                            neighbours += map[i + h * (j + 1)];
                        buffer[i + h * j] = neighbours;
                    }
                }
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        if (map[i + j * h] == 1 && buffer[i + h * j] != 1)
                            map[i + j * h] = 0;
                        else if (map[i + j * h] == 0 && (buffer[i + h * j] == 1 || buffer[i + h * j] == 2))
                            map[i + j * h] = 1;
                    }
                }
                hash = GetHash(map);
            }
            Console.WriteLine(hash);
        }

         long GetHash(int[] map)
        {
            long hash = 0;
            for (int i = 0; i < map.Length; i++)
            {
                hash += (map[i] << i);
            }
            return hash;
        }

         string testInput = @"....#
#..#.
#..##
..#..
#....";
    }
}
