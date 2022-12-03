using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day21
    {
        public void Solve()
        {
            //Player 1 starting position: 7
            //Player 2 starting position: 1

            // Part 1, make it bruteforce
            int dice = 0;
            int p1pos = 7;
            int p1score = 0;
            int p2pos = 1;
            int p2score = 0;
            while (true)
            {
                //player 1
                var throws = GetDiceThrow() + GetDiceThrow() + GetDiceThrow();
                dice += 3;
                p1pos = p1pos + throws;
                if (p1pos > 10)
                {
                    p1pos %= 10;
                    if (p1pos == 0)
                        p1pos = 10;
                }
                p1score += p1pos;
                if (p1score >= 1000)
                    break;

                //player2
                throws = GetDiceThrow() + GetDiceThrow() + GetDiceThrow();
                dice += 3;
                p2pos = p2pos + throws;
                if (p2pos > 10)
                {
                    p2pos %= 10;
                    if (p2pos == 0)
                        p2pos = 10;
                }
                p2score += p2pos;
                if (p2score >= 1000)
                    break;
            }

            Console.WriteLine($"{p1score} {p2score} {dice} {Math.Min(p1score, p2score) * dice}");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        var v = i + j + k + 3;
                        if (DiracDice.ContainsKey(v))
                        {
                            DiracDice[v]++;
                        }
                        else
                        {
                            DiracDice.Add(v, 1);
                        }
                    }
                }
            }

            var sw = new Stopwatch();
            sw.Start();
            RunDiracGame(7, 0, 1, 0, 0, 1);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(p1wins);
            Console.WriteLine(p2wins);
        }

        static long p1wins = 0;
        static long p2wins = 0;
        static Dictionary<int, int> DiracDice = new Dictionary<int, int>();
        private void RunDiracGame(int p1pos, int p1score, int p2pos, int p2score, int player, long numgames)
        {
            foreach (var die in DiracDice)
            {
                if (player == 0)
                {
                    var newp1pos = p1pos + die.Key;
                    if (newp1pos > 10)
                    {
                        newp1pos %= 10;
                        if (newp1pos == 0)
                            newp1pos = 10;
                    }

                    if (p1score + newp1pos >= 21)
                    {
                        p1wins += numgames * die.Value;
                    }
                    else
                    {
                        RunDiracGame(newp1pos, p1score + newp1pos, p2pos, p2score, 1, numgames * die.Value);
                    }
                }
                else
                {
                    var newp2pos = p2pos + die.Key;
                    if (newp2pos > 10)
                    {
                        newp2pos %= 10;
                        if (newp2pos == 0)
                            newp2pos = 10;
                    }

                    if (p2score + newp2pos >= 21)
                    {
                        p2wins += numgames * die.Value;
                    }
                    else
                    {
                        RunDiracGame(p1pos, p1score, newp2pos, p2score + newp2pos, 0, numgames * die.Value);
                    }
                }
            }
        }

        private int lastThrow = 0;
        private int GetDiceThrow()
        {
            lastThrow++;
            if (lastThrow > 100)
                lastThrow = 1;
            return lastThrow;
        }
    }
}
