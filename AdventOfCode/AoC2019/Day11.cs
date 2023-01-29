using System;
using System.Linq;
using System.Text;

namespace AoC2019
{
    class Day11
    {
        string rawInput;
        public Day11()
        {
            rawInput = File.ReadAllText("input11.txt");
        }
        
         long[,] RunRobot(int mapSize, int startPanel)
        {
            var input = rawInput.Split(',').Select(s => long.Parse(s)).ToArray();
            var robot = new Intcode(rawInput);

            var field = new long[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    field[i, j] = -1;
                }
            }

            var x = mapSize / 2;
            var y = mapSize / 2;
            var dir = 0;
            field[x, y] = startPanel;

            while (!robot.Halted)
            {
                var f = field[x, y];
                if (f == -1)
                    f = 0;

                robot.RunWithInput(f);
                if (robot.Halted)
                    break;
                
                field[x, y] = robot.Output.Dequeue();
                if (robot.Output.Dequeue() == 0)
                    dir -= 90;
                else
                    dir += 90;

                if (dir < 0)
                    dir += 360;

                if (dir >= 360)
                    dir -= 360;

                if (dir == 0)
                    y--;
                else if (dir == 90)
                    x++;
                else if (dir == 180)
                    y++;
                else if (dir == 270)
                    x--;
            }

            return field;
        }

        public  void Solve()
        {
            Console.WriteLine((from long item in RunRobot(200, 0) where item >= 0 select item).Count());

            var s = 100;
            var field = RunRobot(s, 1);
            var count = 0;
            var sb = new StringBuilder();
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    if (field[i, j] >= 0)
                        count++;
                    if (field[i, j] == 1)
                        sb.Append('#');
                    else
                        sb.Append(' ');
                }
                sb.AppendLine();
            }

            Console.Write(sb.ToString());
        }
    }
}