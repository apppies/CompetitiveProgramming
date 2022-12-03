using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AoC2021
{
    internal class Day22
    {
        internal class Cube
        {
            public int x;
            public int y;
            public int z;
            public bool value;
            public (int x, int y ,int z) c => (x,y,z);
        }
        internal class Step
        {
            public int x1;
            public int x2;
            public int y1;
            public int y2;
            public int z1;
            public int z2;
            public bool value;

            public Step()
            {

            }
            public Step(string line)
            {
                var p = line.Split('=');
                var x = p[1].Split("..");
                var y = p[2].Split("..");
                var z = p[3].Split("..");
                x1 = int.Parse(x[0]);
                x2 = int.Parse(x[1][0..^2]);
                y1 = int.Parse(y[0]);
                y2 = int.Parse(y[1][0..^2]);
                z1 = int.Parse(z[0]);
                z2 = int.Parse(z[1]);
                value = line[..2] == "on";
            }
            public long NumberOfCubes => (x2 - x1 + 1L) * (y2 - y1 + 1) * (z2 - z1 + 1);

            public List<Cube> GetInitCubes()
            {
                var l = new List<Cube>();
                for (int i = Math.Max(x1, -50); i <= Math.Min(x2, 50); i++)
                {
                    for (int j = Math.Max(y1, -50); j <= Math.Min(y2, 50); j++)
                    {
                        for (int k = Math.Max(z1, -50); k <= Math.Min(z2, 50); k++)
                        {
                            l.Add(new Cube() { x = i, y = j, z = k, value = value });
                        }
                    }
                }
                return l;
            }

            public bool Overlaps(Step other)
            {
                var noOverlap = x1 > other.x2 || x2 < other.x1 || y1 > other.y2 || y2 < other.y1 || z1 > other.z2 || z2 < other.z1;
                return !noOverlap;
            }

            public Step GetOverlap(Step other)
            {
                return new Step()
                {
                    value = !other.value,
                    x1 = Math.Max(x1, other.x1),
                    x2 = Math.Min(x2, other.x2),
                    y1 = Math.Max(y1, other.y1),
                    y2 = Math.Min(y2, other.y2),
                    z1 = Math.Max(z1, other.z1),
                    z2 = Math.Min(z2, other.z2),
                };
            }
        }
        public void Solve()
        {
            var input = File.ReadAllLines("input22.txt");
            var initSteps = new List<Step>();
            foreach (var line in input)
            {
                initSteps.Add(new Step(line));
            }

            var initCubes = new Dictionary<(int x, int y, int z), bool>();
            foreach (var step in initSteps)
            {
                var stepInitCubes = step.GetInitCubes();
                foreach (var cube in stepInitCubes)
                {
                    if (initCubes.ContainsKey(cube.c))
                        initCubes[cube.c] = cube.value;
                    else
                        initCubes.Add(cube.c, cube.value);
                }
            }

            var initCubesOn = initCubes.Count(c => c.Value);
            Console.WriteLine(initCubesOn);


            // Part 2 needs to be smarter
            // Determine all On 
            var OnCubes = 0L;
            var steps = new List<Step>();

            foreach (var step in initSteps)
            {
                var stepsToAdd = new List<Step>();
                foreach (var s in steps)
                {
                    if (s.Overlaps(step))
                    {
                        stepsToAdd.Add(step.GetOverlap(s));
                    }
                }
                steps.AddRange(stepsToAdd);
                if (step.value)
                    steps.Add(step);
            }

            Console.WriteLine(steps.Sum(s => s.NumberOfCubes * (s.value ? 1 : -1)));


        }
    }


}
