using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020
{
    class Day12
    {
        public void Solve()
        {
            var input = File.ReadAllText("input12.txt");
            var lines = input.Split(new char[] { '\n' }).Select(s => s.Trim());

            var x = 0;
            var y = 0;
            var direction = 90;

            foreach (var item in lines)
            {
                var action = item[0];
                var n = int.Parse(item.Substring(1));

                switch (action)
                {
                    case 'N':
                        y -= n;
                        break;

                    case 'S':
                        y += n;
                        break;
                    case 'W':
                        x -= n;
                        break;
                    case 'E':
                        x += n;
                        break;
                    case 'L':
                        direction = direction - n + 360;
                        direction %= 360;
                        break;
                    case 'R':
                        direction = direction + n + 360;
                        direction %= 360; 
                        break;
                    case 'F':
                        switch (direction)
                        {
                            case 0:
                                y -= n;
                                break;
                            case 90:
                                x += n;
                                break;
                            case 180:
                                y += n;
                                break;
                            case 270:
                                x -= n;
                                break;
                            default:
                                Console.WriteLine($"{direction} AA");
                                break;
                        }
                        break;

                    default:
                        break;
                }

            }

            Console.WriteLine($"|x| + |y|: |{x}| + |{y}| = {Math.Abs(x) + Math.Abs(y)}");




            x = 0;
            y = 0;
            var wx = 10;
            var wy = -1;


            foreach (var item in lines)
            {
                var action = item[0];
                var n = int.Parse(item.Substring(1));

                var tx = wx;
                var ty = wy;
                switch (action)
                {
                    case 'N':
                        wy -= n;
                        break;
                    case 'S':
                        wy += n;
                        break;
                    case 'W':
                        wx -= n;
                        break;
                    case 'E':
                        wx += n;
                        break;
                    case 'L':

                        switch (n)
                        {
                            case 0:
                                break;
                            case 90:
                                wx = ty;
                                wy = -tx;
                                break;
                            case 180:
                                wy = -ty;
                                wx = -tx;
                                break;
                            case 270:
                                wx = -ty;
                                wy = tx;
                                break;
                            default:
                                Console.WriteLine($"{direction} BB");
                                break;
                        }
                        break;

                    case 'R':
                        switch (n)
                        {
                            case 0:
                                break;
                            case 90:
                                wx = -ty;
                                wy = tx;
                                break;
                            case 180:
                                wy = -ty;
                                wx = -tx;
                                break;
                            case 270:
                                wx = ty;
                                wy = -tx;
                                break;
                            default:
                                Console.WriteLine($"{direction} BB");
                                break;
                        }
                        break;

                    case 'F':
                        x += wx * n;
                        y += wy * n;
                        break;

                    default:
                        break;
                }

            }



            Console.WriteLine($"|x| + |y|: |{x}| + |{y}| = {Math.Abs(x) + Math.Abs(y)}");
            Console.ReadKey();
        }


        static string test = @"F10
N3
F7
R90
F11";

    }
}
