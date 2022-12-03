using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AoC2018
{
    class Day7
    {
        string input;
        public Day7()
        {
            input = File.ReadAllText("input7.txt");
        }
        
        private class Step
        {
            public List<Step> Parents { get; set; } = new List<Step>();
            public List<Step> Children { get; set; } = new List<Step>();
            public bool IsDone { get; set; } = false;
            public bool CanBeDone { get { return !IsDone && (Parents.Count == 0 || Parents.All(p => p.IsDone)); } }
            public char Key = ' ';

            public int StartedAt { get; set; } = -1;
            public int DoneAt { get { return StartedAt + Key - 'A' + 1 + 60 ; } }

            public override string ToString()
            {
                return $"{Key}: {StartedAt}, {DoneAt}, {Parents.Count}";
            }
        }

        public string AnswerA()
        {
            var steps = new Dictionary<char, Step>();

            var lines = input.Split('\n').Select(s => s.Trim()).Select(s => new Tuple<char, char>(s[5], s[36]));
            foreach (var item in lines)
            {
                if (!steps.ContainsKey(item.Item1))
                {
                    steps.Add(item.Item1, new Step() { Key = item.Item1 });
                }
                if (!steps.ContainsKey(item.Item2))
                {
                    steps.Add(item.Item2, new Step() { Key = item.Item2 });
                }
                steps[item.Item1].Children.Add(steps[item.Item2]);
                steps[item.Item2].Parents.Add(steps[item.Item1]);
            }

            //First item = item without parent
            string answer = "";
            var count = 0;
            var available = new List<Step>();
            available = steps.Select(s => s.Value).OrderBy(s => s.Key).Where(s => s.CanBeDone).ToList();
            while (count < steps.Count)
            {
                var next = available.First(s => s.CanBeDone);
                next.IsDone = true;
                count++;
                answer += next.Key;

                available.AddRange(next.Children);
                available = available.OrderBy(s => s.Key).Where(s => !s.IsDone).ToList();
            }

            return answer;
        }

        public string AnswerB()
        {
            var steps = new Dictionary<char, Step>();

            var lines = input.Split('\n').Select(s => s.Trim()).Select(s => new Tuple<char, char>(s[5], s[36]));
            foreach (var item in lines)
            {
                if (!steps.ContainsKey(item.Item1))
                {
                    steps.Add(item.Item1, new Step() { Key = item.Item1 });
                }
                if (!steps.ContainsKey(item.Item2))
                {
                    steps.Add(item.Item2, new Step() { Key = item.Item2 });
                }
                steps[item.Item1].Children.Add(steps[item.Item2]);
                steps[item.Item2].Parents.Add(steps[item.Item1]);
            }

            string answer = "";
            var count = 0;
            int time = 0;
            var available = new List<Step>();
            var running = new List<Step>();
            available = steps.Select(s => s.Value).OrderBy(s => s.Key).Where(s => s.CanBeDone).ToList();
            var workers = 5;
            var processed = new List<Step>();
            while (processed.Count < steps.Count)
            {
                var next = available.Take(workers - running.Count).ToList();
                foreach (var item in next)
                {
                    available.Remove(item);
                    item.StartedAt = time;
                }
                running.AddRange(next);

                //finish first runnings
                var done = running.Where(i => i.DoneAt == running.Min(v=>v.DoneAt)).ToList();
                foreach (var item in done)
                {
                    item.IsDone = true;
                    time = item.DoneAt; 
                    running.Remove(item);
                    available.AddRange(item.Children);
                    processed.Add(item); 
                }
                available = steps.Select(s => s.Value).OrderBy(s => s.Key).Where(s => !s.IsDone && s.StartedAt == -1 && s.CanBeDone).ToList();
                
            }

            return time.ToString();
        }
            string testinput = @"Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.";

        }
    }
