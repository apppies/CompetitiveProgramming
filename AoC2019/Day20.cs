using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day20
    {
        string rawInput;
        public Day20()
        {
            rawInput = File.ReadAllText("input20.txt");
        }
        
        class Node
        {
            public string Portal { get; set; } = "";
            public bool OuterPortal { get; set; }
            public List<Node> Neighbours { get; set; } = new List<Node>();
            public int X { get; set; }
            public int Y { get; set; }
            public int Distance { get; set; } = -1;
            public Node(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }

        public  void Solve()
        {
            var lines = rawInput.Split('\n');

            var w = lines[0].Length - 4;
            var h = lines.Length - 4;

            var templateLevel = new List<Node>();
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    var val = lines[j + 2][i + 2];
                    if (val == '.')
                    {
                        var newNode = new Node(i, j);
                        if (lines[j + 2][i + 1] >= 64)
                        {
                            newNode.Portal = new string(new char[] { lines[j + 2][i + 0], lines[j + 2][i + 1] });
                        }
                        if (lines[j + 2][i + 3] >= 64)
                        {
                            newNode.Portal = new string(new char[] { lines[j + 2][i + 3], lines[j + 2][i + 4] });
                        }
                        if (lines[j + 1][i + 2] >= 64)
                        {
                            newNode.Portal = new string(new char[] { lines[j + 0][i + 2], lines[j + 1][i + 2] });
                        }
                        if (lines[j + 3][i + 2] >= 64)
                        {
                            newNode.Portal = new string(new char[] { lines[j + 3][i + 2], lines[j + 4][i + 2] });
                        }
                        if (newNode.Portal.Length > 0)
                        {
                            newNode.OuterPortal = (i == 0 || i == w - 1 || j == 0 || j == h - 1);
                        }
                        templateLevel.Add(newNode);
                    }
                }
            }

            var levelA = BuildLevel(templateLevel);

            var startNode = levelA.First(n => n.Portal == "AA");
            startNode.Distance = 0;
            var endNode = levelA.First(n => n.Portal == "ZZ");
            var currentNodes = new List<Node>();
            currentNodes.Add(startNode);
            while (currentNodes.Count > 0 && !currentNodes.Contains(endNode))
            {
                var nextNodes = new List<Node>();
                foreach (var node in currentNodes)
                {
                    foreach (var neighbour in node.Neighbours)
                    {
                        if (neighbour.Distance == -1)
                        {
                            nextNodes.Add(neighbour);
                            neighbour.Distance = node.Distance + 1;
                        }
                    }
                }
                currentNodes = nextNodes;
            }

            Console.WriteLine(endNode.Distance);

            var levelB = new Dictionary<int, List<Node>>();
            levelB.Add(0 ,BuildLevel(templateLevel, true, null));
            for (int i = 1; i < 50; i++)
            {
                levelB.Add(i, BuildLevel(templateLevel, true, levelB[i- 1]));
            }

            startNode = levelB[0].First(n => n.Portal == "AA");
            startNode.Distance = 0;
            endNode = levelB[0].First(n => n.Portal == "ZZ");
            currentNodes = new List<Node>();
            currentNodes.Add(startNode);
            while (currentNodes.Count > 0 && !currentNodes.Contains(endNode))
            {
                var nextNodes = new List<Node>();
                foreach (var node in currentNodes)
                {
                    foreach (var neighbour in node.Neighbours)
                    {
                        if (neighbour.Distance == -1)
                        {
                            nextNodes.Add(neighbour);
                            neighbour.Distance = node.Distance + 1;
                        }
                    }
                }
                currentNodes = nextNodes;
            }

            Console.WriteLine(endNode.Distance);
        }

         List<Node> BuildLevel(List<Node> templateLevel, bool partB = false, List<Node> upperLevel = null)
        {
            var newLevel = new List<Node>();
            foreach (var node in templateLevel)
            {
                newLevel.Add(new Node(node.X, node.Y) { Portal = node.Portal, OuterPortal = node.OuterPortal });
            }

            foreach (var node in newLevel)
            {
                var nodeNeighbours = newLevel.Where(n => (n.X == node.X - 1 && n.Y == node.Y) || (n.X == node.X && n.Y == node.Y - 1));
                foreach (var nodeNeighbour in nodeNeighbours)
                {
                    nodeNeighbour.Neighbours.Add(node);
                    node.Neighbours.Add(nodeNeighbour);
                }
            }

            // Fix portals
            if (!partB)
            {
                var portals = newLevel.Select(n => n.Portal).Where(p => p.Length > 0);
                foreach (var portal in portals)
                {
                    var nodes = newLevel.Where(n => n.Portal == portal);
                    foreach (var node in nodes)
                    {
                        node.Neighbours.AddRange(nodes.Where(n => n != node));
                    }
                }
            }
            else
            {
                if (upperLevel != null)
                {
                    var portalNodes = newLevel.Where(n => n.OuterPortal);
                    foreach (var node in portalNodes)
                    {
                        // Get upperlevel portalnode
                        var upperNodes = upperLevel.Where(n => !n.OuterPortal && n.Portal == node.Portal);

                        // Attach
                        node.Neighbours.AddRange(upperNodes);
                        foreach (var upperNode in upperNodes)
                        {
                            upperNode.Neighbours.Add(node);
                        }
                    }
                }
            }

            return newLevel;
        }
    }
}
