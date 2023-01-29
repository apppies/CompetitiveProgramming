using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace AoC2018
{
    class Day13
    {
        string input;
        public Day13()
        {
            input = File.ReadAllText("input13.txt");
        }
        
        class Cart : IComparable
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Dir Dir { get; set; } //0 left,1 up, 2 right, 3 down
            public int Turn { get; set; } //0 left, 1 straight, 2 right
            public bool Crashed { get; set; }

            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                var two = obj as Cart;
                if (two.Y > Y)
                    return -1;
                else if (two.Y < Y)
                    return 1;
                else
                    return X.CompareTo(two.X);
            }
        }

        enum Dir : int
        {
            Left = 0,
            Up,
            Right,
            Down

        }


        public string AnswerA()
        {//.Replace('>','-').Replace('<','-').Replace('^','|').Replace('v','|')
            var map = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(s => s.ToCharArray()).ToArray();
            var carts = new List<Cart>();
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[0].Length; j++)
                {
                    switch (map[i][j])
                    {
                        case '>':
                            carts.Add(new Cart() { Y = i, X = j, Dir = Dir.Right, Turn = 0 });
                            break;
                        case '<':
                            carts.Add(new Cart() { Y = i, X = j, Dir = Dir.Left, Turn = 0 });
                            break;
                        case '^':
                            carts.Add(new Cart() { Y = i, X = j, Dir = Dir.Up, Turn = 0 });
                            break;
                        case 'v':
                            carts.Add(new Cart() { Y = i, X = j, Dir = Dir.Down, Turn = 0 });
                            break;
                        default:
                            break;
                    }
                }
            }
            int tick = 0;
            while (carts.All(c => !c.Crashed))
            {
                carts.Sort();
                var newLocations = new List<(int x, int y)>();

                //move
                foreach (var car in carts)
                {
                    switch (car.Dir)
                    {
                        case Dir.Left:
                            car.X--;
                            break;
                        case Dir.Up:
                            car.Y--;
                            break;
                        case Dir.Right:
                            car.X++;
                            break;
                        case Dir.Down:
                            car.Y++;
                            break;
                        default:
                            break;
                    }
                    switch (map[car.Y][car.X])
                    {
                        case '-':
                            break;
                        case '|':
                            break;
                        case '\\':
                            if (car.Dir == Dir.Right)
                                car.Dir = Dir.Down;
                            else if (car.Dir == Dir.Left)
                                car.Dir = Dir.Up;
                            else if (car.Dir == Dir.Up)
                                car.Dir = Dir.Left;
                            else if (car.Dir == Dir.Down)
                                car.Dir = Dir.Right;
                            break;
                        case '/':
                            if (car.Dir == Dir.Right)
                                car.Dir = Dir.Up;
                            else if (car.Dir == Dir.Left)
                                car.Dir = Dir.Down;
                            else if (car.Dir == Dir.Up)
                                car.Dir = Dir.Right;
                            else if (car.Dir == Dir.Down)
                                car.Dir = Dir.Left;
                            break;
                        case '+':
                            switch (car.Turn)
                            {
                                case 0: //turn left
                                    switch (car.Dir)
                                    {
                                        case Dir.Left:
                                            car.Dir = Dir.Down;
                                            break;
                                        case Dir.Up:
                                            car.Dir = Dir.Left;
                                            break;
                                        case Dir.Right:
                                            car.Dir = Dir.Up;
                                            break;
                                        case Dir.Down:
                                            car.Dir = Dir.Right;
                                            break;
                                        default:
                                            break;
                                    }
                                    break;

                                case 1:
                                    break;

                                case 2: //turn right
                                    switch (car.Dir)
                                    {
                                        case Dir.Left:
                                            car.Dir = Dir.Up;
                                            break;
                                        case Dir.Up:
                                            car.Dir = Dir.Right;
                                            break;
                                        case Dir.Right:
                                            car.Dir = Dir.Down;
                                            break;
                                        case Dir.Down:
                                            car.Dir = Dir.Left;
                                            break;
                                        default:
                                            break;
                                    }
                                    break;

                                default:
                                    break;
                            }

                            car.Turn += 1;
                            car.Turn %= 3;

                            break;
                        default:
                            break;
                    }
                    //Check collisions
                    for (int i = 0; i < carts.Count; i++)
                    {
                        if (carts[i] != car && carts[i].X == car.X && carts[i].Y == car.Y)
                        {
                            return $"{carts[i].X},{carts[i].Y}";
                        }
                    }

                }



                // Console.Write(tick);
                tick++;
            }
            return "";

        }

        public string AnswerB()
        {//.Replace('>','-').Replace('<','-').Replace('^','|').Replace('v','|')
            var map = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(s => s.ToCharArray()).ToArray();
            var carts = new List<Cart>();
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[0].Length; j++)
                {
                    switch (map[i][j])
                    {
                        case '>':
                            carts.Add(new Cart() { Y = i, X = j, Dir = Dir.Right, Turn = 0 });
                            break;
                        case '<':
                            carts.Add(new Cart() { Y = i, X = j, Dir = Dir.Left, Turn = 0 });
                            break;
                        case '^':
                            carts.Add(new Cart() { Y = i, X = j, Dir = Dir.Up, Turn = 0 });
                            break;
                        case 'v':
                            carts.Add(new Cart() { Y = i, X = j, Dir = Dir.Down, Turn = 0 });
                            break;
                        default:
                            break;
                    }
                }
            }
            int tick = 0;
            while (true)
            {
                carts = carts.Where(c => !c.Crashed).ToList();
                carts.Sort();

                if (carts.Count == 1)
                {
                    return $"{carts[0].X},{carts[0].Y}";
                }

                //move
                foreach (var car in carts)
                {
                    switch (car.Dir)
                    {
                        case Dir.Left:
                            car.X--;
                            break;
                        case Dir.Up:
                            car.Y--;
                            break;
                        case Dir.Right:
                            car.X++;
                            break;
                        case Dir.Down:
                            car.Y++;
                            break;
                        default:
                            break;
                    }
                    switch (map[car.Y][car.X])
                    {
                        case '-':
                            break;
                        case '|':
                            break;
                        case '\\':
                            if (car.Dir == Dir.Right)
                                car.Dir = Dir.Down;
                            else if (car.Dir == Dir.Left)
                                car.Dir = Dir.Up;
                            else if (car.Dir == Dir.Up)
                                car.Dir = Dir.Left;
                            else if (car.Dir == Dir.Down)
                                car.Dir = Dir.Right;
                            break;
                        case '/':
                            if (car.Dir == Dir.Right)
                                car.Dir = Dir.Up;
                            else if (car.Dir == Dir.Left)
                                car.Dir = Dir.Down;
                            else if (car.Dir == Dir.Up)
                                car.Dir = Dir.Right;
                            else if (car.Dir == Dir.Down)
                                car.Dir = Dir.Left;
                            break;
                        case '+':
                            switch (car.Turn)
                            {
                                case 0: //turn left
                                    switch (car.Dir)
                                    {
                                        case Dir.Left:
                                            car.Dir = Dir.Down;
                                            break;
                                        case Dir.Up:
                                            car.Dir = Dir.Left;
                                            break;
                                        case Dir.Right:
                                            car.Dir = Dir.Up;
                                            break;
                                        case Dir.Down:
                                            car.Dir = Dir.Right;
                                            break;
                                        default:
                                            break;
                                    }
                                    break;

                                case 1:
                                    break;

                                case 2: //turn right
                                    switch (car.Dir)
                                    {
                                        case Dir.Left:
                                            car.Dir = Dir.Up;
                                            break;
                                        case Dir.Up:
                                            car.Dir = Dir.Right;
                                            break;
                                        case Dir.Right:
                                            car.Dir = Dir.Down;
                                            break;
                                        case Dir.Down:
                                            car.Dir = Dir.Left;
                                            break;
                                        default:
                                            break;
                                    }
                                    break;

                                default:
                                    break;
                            }

                            car.Turn += 1;
                            car.Turn %= 3;

                            break;
                        default:
                            break;
                    }
                    //Check collisions
                    for (int i = 0; i < carts.Count; i++)
                    {
                        if (carts[i] != car && !carts[i].Crashed && carts[i].X == car.X && carts[i].Y == car.Y)
                        {
                            carts[i].Crashed = true;
                            car.Crashed = true;
                        }
                    }

                }


                // Console.Write(tick);
                tick++;
            }
            return "";

        }

        string testinput = @"/->-\        
|   |  /----\
| /-+--+-\  |
| | |  | v  |
\-+-/  \-+--/
  \------/   ";

        string testinputB = @"/>-<\  
|   |  
| /<+-\
| | | v
\>+</ |
  |   ^
  \<->/";
    }
}