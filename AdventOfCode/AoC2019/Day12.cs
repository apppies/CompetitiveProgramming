using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day12
    {
        string rawInput;
        public Day12()
        {
            rawInput = File.ReadAllText("input12.txt");
        }
        
        class Moon
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public int Vx { get; set; }
            public int Vy { get; set; }
            public int Vz { get; set; }
            public Moon(int[] xyz)
            {
                X = xyz[0];
                Y = xyz[1];
                Z = xyz[2];
            }

            public int Potential { get { return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z); } }
            public int Kinetic { get { return Math.Abs(Vx) + Math.Abs(Vy) + Math.Abs(Vz); } }
            public int Energy { get { return Potential * Kinetic; } }

            public (int,int) StateX { get { return (X,Vx); } }
            public (int, int) StateY { get { return (Y,Vy); } }
            public (int, int) StateZ { get { return (Z,Vz); } }
        }

        public  void Solve()
        {
            SolveA();
            SolveB();
        }
         void SolveA()
        {
            var lines = rawInput.Split('\n');
            var moons = new List<Moon>();
            var states = new HashSet<string>();
            foreach (var line in lines)
            {
                var coords = from Match v in Regex.Matches(line, @"-?\d+") select int.Parse(v.Value);
                
                var moon = new Moon(coords.ToArray());
                moons.Add(moon);
            }

            for (int i = 0; i <= 1000; i++) //steps
            {
                foreach (var sm in moons)
                {
                    foreach (var dm in moons)
                    {
                        if (dm != sm)
                        {
                            sm.Vx += Math.Sign(dm.X - sm.X);
                            sm.Vy += Math.Sign(dm.Y - sm.Y);
                            sm.Vz += Math.Sign(dm.Z - sm.Z);
                        }
                    }
                }

                var totalEnergy = 0;
                foreach (var moon in moons)
                {
                    moon.X += moon.Vx;
                    moon.Y += moon.Vy;
                    moon.Z += moon.Vz;
                    totalEnergy += moon.Energy;
                }
                if (i == 999)
                    Console.WriteLine($"{i}: Energy {totalEnergy}");
            }
        }

         void SolveB()
        {
            var lines = rawInput.Split('\n');
            var moons = new List<Moon>();

            var statesX = new HashSet<string>();
            var statesY = new HashSet<string>();
            var statesZ = new HashSet<string>();
            foreach (var line in lines)
            {
                var coords = from Match v in Regex.Matches(line, @"-?\d+") select int.Parse(v.Value);
                var moon = new Moon(coords.ToArray());
                moons.Add(moon);
            }
            statesX.Add(moons.Aggregate("", (acc, m) => acc + m.StateX));
            statesY.Add(moons.Aggregate("", (acc, m) => acc + m.StateY));
            statesZ.Add(moons.Aggregate("", (acc, m) => acc + m.StateZ));

            var foundX = 0;
            var foundY = 0;
            var foundZ = 0;
            var step = 0;
            while (foundX == 0 || foundY == 0 || foundZ == 0)
            {
                foreach (var sm in moons)
                {
                    foreach (var dm in moons)
                    {
                        if (dm != sm)
                        {
                            sm.Vx += Math.Sign(dm.X - sm.X);
                            sm.Vy += Math.Sign(dm.Y - sm.Y);
                            sm.Vz += Math.Sign(dm.Z - sm.Z);
                        }
                    }
                }

                foreach (var moon in moons)
                {
                    moon.X += moon.Vx;
                    moon.Y += moon.Vy;
                    moon.Z += moon.Vz;
                }

                step++;

                if (foundX == 0)
                {
                    var s = moons.Aggregate("", (acc, m) => acc + m.StateX);
                    if (statesX.Contains(s))
                        foundX = step;
                    else
                        statesX.Add(s);
                }
                if (foundY == 0)
                {
                    var s = moons.Aggregate("", (acc, m) => acc + m.StateY);
                    if (statesY.Contains(s))
                        foundY = step;
                    else
                        statesY.Add(s);
                }
                if (foundZ == 0)
                {
                    var s = moons.Aggregate("", (acc, m) => acc + m.StateZ);
                    if (statesZ.Contains(s))
                        foundZ = step;
                    else
                        statesZ.Add(s);
                }
            }
            var ans = GetGCF(foundX, GetGCF(foundY, foundZ));

            Console.WriteLine($"Period {ans}");
        }

         string testInput = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

         string testInput2 = @"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>";

         long GetGCF(long a, long b)
        {
            return (a * b) / GetGCD(a, b);
        }

         long GetGCD(long a, long b)
        {
            while (a != b)
                if (a < b) 
                    b = b - a;
                else 
                    a = a - b;
            return (a);
        }

    }
}
