using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020
{
    class Day19
    {
        class Rule
        {
            public int Index { get; set; }
            public string Char { get; set; } = null;
            public List<List<int>> SubRules { get; set; } = new List<List<int>>();

            public Rule(string line)
            {
                var s1 = line.Split(':');
                Index = int.Parse(s1[0]);
                var s2 = s1[1].Split('|');
                if (s1[1].Trim()[0] == '"')
                {
                    Char = s1[1].Trim().Substring(1, 1);
                }
                else
                {
                    foreach (var ruleset in s2)
                    {
                        SubRules.Add(ruleset.Trim().Split(' ').Select(s => int.Parse(s)).ToList());
                    }
                }
            }

            public List<string> GetRuleString(int depth = 0)
            {
                var output = new List<string>();
                if (SubRules.Count == 0)
                    output.Add(Char);
                else if (depth > maxDepth)
                    output.Add("");
                else
                {
                    foreach (var ruleset in SubRules)
                    {
                        // 1 2
                        var rulesetstring = new List<string>() { "" };

                        foreach (var rule in ruleset)
                        {
                            var ret = rules[rule].GetRuleString(depth + 1);
                            var n = rulesetstring.Count;
                            for (int i = 0; i < n; i++)
                            {
                                var baserule = rulesetstring[i];
                                var firstReplaced = false;
                                for (int j = 0; j < ret.Count; j++)
                                {
                                    var newRule = baserule + ret[j];
                                    if (data.Any(s => s.Contains(newRule)))
                                    {
                                        if (!firstReplaced)
                                        {
                                            rulesetstring[i] = newRule;
                                            firstReplaced = true;
                                        }
                                        else
                                            rulesetstring.Add(newRule);
                                    }
                                }
                            }
                        }
                        output.AddRange(rulesetstring);
                    }
                }
                return output;
            }



        }
        static Dictionary<int, Rule> rules = new Dictionary<int, Rule>();
        static List<string> data = new List<string>();
        static int maxDepth = 0;
        public void Solve()
        {
            var input = File.ReadAllText("input19.txt");
            var lines = input.Split(new char[] { '\n' }).Select(s => s.Trim()).ToList();

            data = new List<string>();
            var adData = false;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    adData = true;
                else if (adData == false)
                {
                    var r = new Rule(line);
                    rules.Add(r.Index, r);
                }
                else
                    data.Add(line);
            }

            maxDepth = data.Max(d => d.Length) + 1;

            // Expand rule 0
            var rule0 = rules[0];

            var rulestrings = rule0.GetRuleString();
            long count = 0;
            foreach (var d in data)
            {
                if (rulestrings.Contains(d))
                    count++;
            }
            Console.WriteLine(count);



            rules[8] = new Rule("8: 42 | 42 8");
            rules[11] = new Rule("11: 42 31 | 42 11 31");

            Console.WriteLine($"Rule 31: {rules[31].GetRuleString()}");
            Console.WriteLine($"Rule 42: {rules[42].GetRuleString()}");


            // Expand rule 0
            rule0 = rules[0];

            rulestrings = rule0.GetRuleString();
            count = 0;
            foreach (var d in data)
            {
                if (rulestrings.IndexOf(d) >= 0)
                    count++;
            }
            Console.WriteLine(count);



            Console.ReadKey();
        }



        static string test = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""

ababbb
bababa
abbbab
aaabbb
aaaabbb";

        static string test2 = @"42: 9 14 | 10 1
9: 14 27 | 1 26
10: 23 14 | 28 1
1: ""a""
11: 42 31
5: 1 14 | 15 1
19: 14 1 | 14 14
12: 24 14 | 19 1
16: 15 1 | 14 14
31: 14 17 | 1 13
6: 14 14 | 1 14
2: 1 24 | 14 4
0: 8 11
13: 14 3 | 1 12
15: 1 | 14
17: 14 2 | 1 7
23: 25 1 | 22 14
28: 16 1
4: 1 1
20: 14 14 | 1 15
3: 5 14 | 16 1
27: 1 6 | 14 18
14: ""b""
21: 14 1 | 1 14
25: 1 1 | 1 14
22: 14 14
8: 42
26: 14 22 | 1 20
18: 15 15
7: 14 5 | 1 21
24: 14 1

abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa
bbabbbbaabaabba
babbbbaabbbbbabbbbbbaabaaabaaa
aaabbbbbbaaaabaababaabababbabaaabbababababaaa
bbbbbbbaaaabbbbaaabbabaaa
bbbababbbbaaaaaaaabbababaaababaabab
ababaaaaaabaaab
ababaaaaabbbaba
baabbaaaabbaaaababbaababb
abbbbabbbbaaaababbbbbbaaaababb
aaaaabbaabaaaaababaa
aaaabbaaaabbaaa
aaaabbaabbaaaaaaabbbabbbaaabbaabaaa
babaaabbbaaabaababbaabababaaab
aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba";
    }
}
