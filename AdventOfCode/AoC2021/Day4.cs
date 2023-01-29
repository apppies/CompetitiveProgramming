using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day4
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input4.txt").Select(s => s.Trim()).ToArray();
            var sample = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7".Split(new char[] { '\n' }).Select(s => s.Trim()).ToArray();
            //input = sample;

            var draws = input[0].Split(new char[] { ',' }).Select(c => int.Parse(c)).ToArray();
            int[] newBoard = new int[5 * 8];
            int curLine = 0;
            var boards = new List<Board>();
            for (int i = 2; i < input.Length; i++)
            {
                if (input[i].Length == 0)
                {
                    boards.Add(new Board(newBoard));
                    newBoard = new int[5 * 5];
                    curLine = 0;
                }
                else
                {
                    var newLine = input[i].Replace("  ", " ").Split(new char[] { ' ' }).Select(c => int.Parse(c)).ToArray();
                    for (int j = 0; j < 5; j++)
                    {
                        newBoard[curLine * 5 + j] = newLine[j];
                    }
                    curLine++;
                }
            }
            boards.Add(new Board(newBoard));

            var winningBoards = new List<int>();

            for (int i = 0; i < draws.Length; i++)
            {
                for (int j = 0; j < boards.Count; j++)
                {
                    if (!winningBoards.Contains(j))
                    {
                        var win = boards[j].CheckNumber(draws[i]);
                        if (win)
                        {
                            if (winningBoards.Count == 0)
                            {
                                Console.WriteLine($"First winning board {j}: {boards[j].GetWinCode(draws[i])}");
                            }
                            if (winningBoards.Count == boards.Count - 1)
                            {
                                Console.WriteLine($"Last winning board {j}: {boards[j].GetWinCode(draws[i])}");
                                return;
                            }
                            winningBoards.Add(j);
                        }
                    }
                }

            }
        }

        class Board
        {
            int[] board;
            bool[] crossed;
            int width = 5;
            public Board(int[] board, int w = 5)
            {
                this.board = board;
                this.crossed = new bool[board.Length];
                width = w;
            }

            public bool CheckNumber(int n)
            {
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == n)
                    {
                        crossed[i] = true;
                        return IsWinning();
                    }
                }
                return false;
            }

            public bool IsWinning()
            {
                for (int i = 0; i < width; i++)
                {
                    bool win = true;
                    for (int j = 0; j < width; j++)
                    {
                        win &= crossed[i * width + j];
                    }
                    if (win)
                        return true;

                    win = true;
                    for (int j = 0; j < width; j++)
                    {
                        win &= crossed[i + j * width];
                    }
                    if (win)
                        return true;
                }
                return false;
            }
            public int GetWinCode(int draw)
            {
                var sum = 0;
                for (int i = 0; i < crossed.Length; i++)
                {
                    if (!crossed[i])
                    {
                        sum += board[i];
                    }
                }
                return sum * draw;
            }



        }
    }
}
