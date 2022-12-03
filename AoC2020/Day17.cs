using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020
{
    class Day17
    {
        public void Solve()
        {
            var input = File.ReadAllText("input17.txt");
            var lines = input.Split(new char[] { '\n' }).Select(s => s.Trim()).ToList();

            //X,Y,Z
            int[,,] grid3D = new int[30, 30, 30];
            int x = 10;
            foreach (var line in lines)
            {
                x++;
                for (int i = 0; i < line.Length; i++)
                {
                    grid3D[x, i + 10, 10] = line[i] == '.' ? 0 : 1;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                grid3D = Iterate3D(grid3D);
                Console.WriteLine($"Count: {Count3D(grid3D)}");
            }

            int[,,,] grid4D = new int[30, 30, 30, 30];
            x = 10;
            foreach (var line in lines)
            {
                x++;
                for (int i = 0; i < line.Length; i++)
                {
                    grid4D[x, i + 10, 10, 10] = line[i] == '.' ? 0 : 1;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                grid4D = Iterate4D(grid4D);
                Console.WriteLine($"Count: {Count4D(grid4D)}");
            }




            Console.ReadKey();
        }

        private static int[,,,] Iterate4D(int[,,,] grid)
        {
            int[,,,] output = new int[30, 30, 30, 30];
            for (int x = 1; x < 29; x++)
            {
                for (int y = 1; y < 29; y++)
                {
                    for (int z = 1; z < 29; z++)
                    {
                        for (int w = 1; w < 29; w++)
                        {
                            var sum = 0;
                            for (int x1 = -1; x1 <= 1; x1++)
                            {
                                for (int y1 = -1; y1 <= 1; y1++)
                                {
                                    for (int z1 = -1; z1 <= 1; z1++)
                                    {
                                        for (int w1 = -1; w1 <= 1; w1++)
                                        {
                                            if (!(x1 == 0 && y1 == 0 && z1 == 0 && w1 == 0))
                                            {
                                                sum += grid[x + x1, y + y1, z + z1, w + w1];
                                            }
                                        }
                                    }
                                }
                            }

                            //grid[x,y,z]
                            if (grid[x, y, z, w] == 0 && sum == 3)
                            {
                                output[x, y, z, w] = 1;
                            }
                            else if (grid[x, y, z, w] == 1 && sum != 2 && sum != 3)
                            {
                                output[x, y, z, w] = 0;
                            }
                            else
                                output[x, y, z, w] = grid[x, y, z, w];
                        }
                    }
                }
            }
            return output;
        }

        private static int Count3D(int[,,] grid)
        {
            int output = 0;
            foreach (var item in grid)
            {
                if (item == 1)
                    output++;
            }
            return output;
        }
        private static int Count4D(int[,,,] grid)
        {
            int output = 0;
            foreach (var item in grid)
            {
                if (item == 1)
                    output++;
            }
            return output;
        }

        private static int[,,] Iterate3D(int[,,] grid)
        {
            int[,,] output = new int[30, 30, 30];
            for (int x = 1; x < 29; x++)
            {
                for (int y = 1; y < 29; y++)
                {
                    for (int z = 1; z < 29; z++)
                    {
                        int sum = grid[x - 1, y - 1, z - 1] + grid[x, y - 1, z - 1] + grid[x + 1, y - 1, z - 1] + grid[x - 1, y, z - 1] + grid[x, y, z - 1] + grid[x + 1, y, z - 1] + grid[x - 1, y + 1, z - 1] + grid[x, y + 1, z - 1] + grid[x + 1, y + 1, z - 1];
                        sum += grid[x - 1, y - 1, z + 1] + grid[x, y - 1, z + 1] + grid[x + 1, y - 1, z + 1] + grid[x - 1, y, z + 1] + grid[x, y, z + 1] + grid[x + 1, y, z + 1] + grid[x - 1, y + 1, z + 1] + grid[x, y + 1, z + 1] + grid[x + 1, y + 1, z + 1];
                        sum += grid[x - 1, y - 1, z] + grid[x, y - 1, z] + grid[x + 1, y - 1, z] + grid[x - 1, y, z] + grid[x + 1, y, z] + grid[x - 1, y + 1, z] + grid[x, y + 1, z] + grid[x + 1, y + 1, z];

                        //grid[x,y,z]
                        if (grid[x, y, z] == 0 && sum == 3)
                        {
                            output[x, y, z] = 1;
                        }
                        else if (grid[x, y, z] == 1 && sum != 2 && sum != 3)
                        {
                            output[x, y, z] = 0;
                        }
                        else
                            output[x, y, z] = grid[x, y, z];
                    }
                }
            }
            return output;
        }

        static string test = @".#.
..#
###";

    }
}
