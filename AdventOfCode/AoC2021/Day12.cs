using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day12
    {
        public void Solve()
        {
            var input = System.IO.File.ReadAllLines("input12.txt").Select(s => s.Split(new char[] { '-' })).ToList();
            var nodes = new Dictionary<string, List<string>>();
            //nodes.Add("start", new List<string>());
            //nodes.Add("end", new List<string>());

            for (int i = 0; i < input.Count; i++)
            {
                if (!nodes.ContainsKey(input[i][0]))
                    nodes.Add(input[i][0], new List<string>());
                if (!nodes.ContainsKey(input[i][1]))
                    nodes.Add(input[i][1], new List<string>());
                nodes[input[i][0]].Add(input[i][1]);
                nodes[input[i][1]].Add(input[i][0]);
            }

            GetRoute("start-", "start", nodes);
            Console.WriteLine($"Number of routes {Day12Routes.Count}");
            GetRoute2("start-", "start", nodes, false);
            Console.WriteLine($"Number of routes {Day12Routes2.Count}");
        }

        List<string> Day12Routes = new List<string>();
        List<string> Day12Routes2 = new List<string>();
        void GetRoute(string curRoute, string curNode, Dictionary<string, List<string>> nodes)
        {
            var ret = new List<string>();
            if (curNode == "end")
            {
                Day12Routes.Add(curRoute);
                return;
            }
            var next = nodes[curNode];
            foreach (var n in next)
            {
                if (n.All(char.IsUpper)) //no limit
                {
                    GetRoute(curRoute + n + '-', n, nodes);
                }
                else if (!curRoute.Contains(n + '-'))
                {
                    GetRoute(curRoute + n + '-', n, nodes);
                }
            }

        }
        void GetRoute2(string curRoute, string curNode, Dictionary<string, List<string>> nodes, bool twiced)
        {
            var ret = new List<string>();
            if (curNode == "end")
            {
                Day12Routes2.Add(curRoute);
                return;
            }
            var next = nodes[curNode];
            foreach (var n in next)
            {
                if (n.All(char.IsUpper)) //no limit
                {
                    GetRoute2(curRoute + n + '-', n, nodes, twiced);
                }
                else if (n == "start")
                {
                    continue;
                }
                else if (!curRoute.Contains(n + '-')) // not yet found
                {
                    GetRoute2(curRoute + n + '-', n, nodes, twiced);
                }
                else if (!twiced) // already visited, but no twice hit yet
                {
                    GetRoute2(curRoute + n + '-', n, nodes, true);
                }
            }

        }
    }
}
