using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2018
{
    class Day11

    {
        private const int Input = 7989;

        private class FuelCell
        {
            public int X { get; set; }
            public int Y { get; set; }
            private int _powerLevel = int.MinValue;
            public int PowerLevel
            {
                get
                {
                    if (_powerLevel == int.MinValue)
                    {
                        var rackID = (X + 10);
                        var pL = rackID * Y;
                        pL += Input;
                        pL *= rackID;
                        _powerLevel = (pL / 100 % 10) - 5;
                    }
                    return _powerLevel;
                }
            }

        }


        public string AnswerA()
        {
            //Create grid
            var grid = new FuelCell[300, 300];
            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 300; j++)
                {
                    grid[i, j] = new FuelCell() { X = i + 1, Y = j + 1 };
                }
            }

            // Get powerlevelgrids
            FuelCell top = null;
            int currentMax = int.MinValue;

            for (int i = 1; i < 299; i++)
            {
                for (int j = 1; j < 299; j++)
                {
                    var sum = grid[i - 1, j - 1].PowerLevel + grid[i, j - 1].PowerLevel + grid[i + 1, j - 1].PowerLevel +
                              grid[i - 1, j].PowerLevel + grid[i, j].PowerLevel + grid[i + 1, j].PowerLevel +
                              grid[i - 1, j + 1].PowerLevel + grid[i, j + 1].PowerLevel + grid[i + 1, j + 1].PowerLevel;
                    if (sum > currentMax)
                    {
                        currentMax = sum;
                        top = grid[i - 1, j - 1];
                    }

                }
            }

            return $"{top.X},{top.Y}";
        }

        public string AnswerB()
        {
            //Create grid
            var grid = new FuelCell[300, 300];
            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 300; j++)
                {
                    grid[i, j] = new FuelCell() { X = i + 1, Y = j + 1 };
                }
            }

            // Get powerlevelgrids
            FuelCell top = null;
            int currentMax = int.MinValue;
            int currentSize = 1;
            for (int s = 1; s < 301; s++)
            {

                for (int i = 0; i < 300 - (s - 1); i++)
                {
                    for (int j = 0; j < 300 - (s - 1); j++)
                    {
                        var sum = 0;
                        for (int x = 0; x < s; x++)
                        {
                            for (int y = 0; y < s; y++)
                            {
                                sum += grid[i + x, j + y].PowerLevel;
                            }
                        }
                        if (sum > currentMax)
                        {
                            currentMax = sum;
                            currentSize = s;
                            top = grid[i , j ];
                        }

                    }
                }

            }
            return $"{top.X},{top.Y},{currentSize}";
        }
    }
}
