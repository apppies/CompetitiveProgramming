using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2018
{
    class Day8
    {
        string rawInput;
        public Day8()
        {
            rawInput = File.ReadAllText("input8.txt");
        }
        
        class Node
        {
            public List<Node> ChildNodes { get; set; }
            public List<int> Metadata { get; set; }

            public int MetaDataSum { get { return Metadata.Sum() + ChildNodes.Sum(n => n.MetaDataSum); } }
            public int MetaDataSum2
            {
                get
                {
                    if (ChildNodes.Count == 0)
                    {
                        return Metadata.Sum();
                    }
                    else
                    {
                        var sum = 0;
                        for (int i = 0; i < Metadata.Count; i++)
                        {
                            var index = Metadata[i] - 1;
                            if (index < ChildNodes.Count)
                            {
                                sum += ChildNodes[index].MetaDataSum2;
                            }
                        }
                        return sum;
                    }
                }
            }

            public Node(int children, int entries)
            {
                ChildNodes = new List<Node>(children);
                Metadata = new List<int>(entries);
            }
        }

        public string AnswerA()
        {
            var input = rawInput.Split(' ').Select(c => int.Parse(c)).ToArray();

            var tree = new List<Node>();
            var currentTree = tree;
            var sum = 0;
            var sum2 = 0;
            for (int i = 0; i < input.Length; i++)
            {
                Node node;
                i = ParseNode(i, input, out node);
                currentTree.Add(node);
                sum += node.MetaDataSum;
                sum2 += node.MetaDataSum2;
            }

            return $"{sum} - {sum2}";
        }

        static int ParseNode(int index, int[] input, out Node node)
        {
            var nc = input[index];
            var nm = input[index + 1];
            index += 2;

            node = new Node(nc, nm);

            for (int i = 0; i < nc; i++)
            {
                Node cnode;
                index = ParseNode(index, input, out cnode);
                node.ChildNodes.Add(cnode);
            }
            for (int i = 0; i < nm; i++)
            {
                node.Metadata.Add(input[index++]);
            }

            return index;
        }

        static string testInput = @"2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
    }
}
