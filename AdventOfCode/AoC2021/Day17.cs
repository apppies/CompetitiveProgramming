using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day17
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input17.txt");
            var target = "x = 81..129, y = -150..-108";

            // highest v0 = (ytarget_min - 1)
            // max heigh  =v0 * (v0+1) / 2
            var h = 149 * 150 / 2;
            Console.WriteLine(h);

            var minx = 81;
            var maxx = 129;
            var maxy = -108;
            var miny = -150;

            var maxvy0 = 149;
            var minvy0 = -150;
            var maxvx0 = 129;
            var minvx0 = 13; //max x  = v0 * (v0+1) / 2 == 13

            //var minx = 20;
            //var maxx = 30;
            //var maxy = -5;
            //var miny = -10;
            //var maxvy0 = 9;
            //var minvy0 = -10;
            //var maxvx0 = 30;
            //var minvx0 = 6;
            var results = 0;
            for (int vx0 = minvx0; vx0 <= maxvx0; vx0++)
            {
                for (int vy0 = minvy0; vy0 <= maxvy0; vy0++)
                {
                    var x = 0;
                    var y = 0;
                    var vx = vx0;
                    var vy = vy0;
                    while (x <= maxx && y >= miny)
                    {
                        x += vx;
                        y += vy;

                        if (x >= minx && x <= maxx && y <= maxy && y >= miny)
                        {
                            results++;
                            break;
                        }

                        vx--;
                        if (vx < 0)
                            vx = 0;
                        vy--;

                    }
                }
            }

            Console.WriteLine(results);
        }
    }
}
