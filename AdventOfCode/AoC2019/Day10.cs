using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019
{
    class Day10
    {
        string rawInput;
        public Day10()
        {
            rawInput = File.ReadAllText("input10.txt");
        }
        
        class Asteroid
        {
            public int X { get; set; }
            public int Y { get; set; }
            public double a { get; private set; }
            public int R2 { get; private set; }
            public bool Destroyed { get; set; }
            public Asteroid SetOrigin(Asteroid origin)
            {
                SetOrigin(origin.X, origin.Y);
                return this;
            }
            public Asteroid SetOrigin(int ox, int oy)
            {
                int dx = X - ox;
                int dy = Y - oy;
                this.R2 = dx * dx + dy * dy;

                var a = Math.Atan2(dy, dx) / Math.PI * 180.0;
                if (a < -90)
                    a += 360;
                a += 90;
                this.a = a;
                return this;
            }

            public Asteroid(int x, int y)
            {
                this.X = x;
                this.Y = y;
                SetOrigin(0,0);
            }
        }



        public  void Solve()
        {
            var asteroids = new List<Asteroid>();
            var lines = rawInput.Split('\n').Select(s => s.Trim()).ToArray();
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                        asteroids.Add(new Asteroid(x, y));
                }
            }

            Console.WriteLine("Day 10 - 1");
            var maxCount = 0;
            Asteroid origin = asteroids.First();
            foreach (var testOrigin in asteroids)
            {
                var polar = asteroids.Where(a => a != testOrigin).Select(b => b.SetOrigin(testOrigin));
                var count = asteroids.Where(a => a != testOrigin).Select(b => b.SetOrigin(testOrigin).a).Distinct().Count();
                if (count > maxCount)
                {
                    maxCount = count;
                    origin = testOrigin;
                }
            }
            Console.WriteLine($"{origin.X},{origin.Y}");
            Console.WriteLine($"Count {maxCount}");

            Console.WriteLine("Day 10 - 2");
            var toDestroy = asteroids.Where(a => a != origin).Select(b => b.SetOrigin(origin)).OrderBy(a => a.a).ThenBy(r => r.R2).ToArray();
            var target = 200;
            var counter = 0;
            var lastAngle = -1.0;
            while (counter < target)
            {
                for (int i = 0; i < toDestroy.Length; i++)
                {
                    if (toDestroy[i].a == lastAngle)
                        continue;
                    if (toDestroy[i].Destroyed)
                        continue;

                    toDestroy[i].Destroyed = true;
                    lastAngle = toDestroy[i].a;
                    counter++;
                    if (counter == target)
                        Console.WriteLine($"{toDestroy[i].X * 100 + toDestroy[i].Y}");
                    
                }
            }
        }
    }
}
