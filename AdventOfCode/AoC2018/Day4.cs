using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace AoC2018
{
    class Day4
    {
        string input;
        public Day4()
        {
            input = File.ReadAllText("input4.txt");
        }
        
        private class InfoLine
        {
            public DateTime date;
            public string eventtype;
            public string tag;
            public InfoLine(string line)
            {
                date = DateTime.Parse(line.Substring(1, 16));
                eventtype = line.Substring(19, 5);
                if (eventtype == "Guard")
                {
                    tag = line.Substring(26).Trim().Split(' ')[0];
                }
            }
        }

        private class Guard
        {
            public int[] Asleep;
            public string ID;
            public Guard()
            {
                Asleep = new int[60];
            }
            public int TotalSleep
            {
                get { return Asleep.Sum(); }
            } 
        }


        public string AnswerA()
        {
            var Info = new List<InfoLine>();
            foreach (var line in input.Split('\n'))
            {
                Info.Add(new InfoLine(line.Trim()));
            }
            var sorted = Info.OrderBy(i => i.date);

            var Guards = new Dictionary<string, Guard>();
            Guard selectedGuard = null;

            DateTime sleep = DateTime.Now;
            DateTime wake = DateTime.Now;
            foreach (var item in sorted)
            {
                if (item.eventtype == "Guard")
                {
                    if (!Guards.ContainsKey(item.tag))
                        Guards.Add(item.tag, new Guard() { ID = item.tag });
                    selectedGuard = Guards[item.tag];
                }
                else if (item.eventtype == "falls")
                {
                    sleep = item.date;
                }
                else if (item.eventtype == "wakes")
                {
                    wake = item.date;

                    int startminute = 0;
                    if (sleep.Hour == 0)
                        startminute = sleep.Minute;

                    for (int i = startminute; i < wake.Minute; i++)
                    {
                        selectedGuard.Asleep[i]++;
                    }
                }
            }

            var sleepyGuard = Guards.OrderByDescending(g=>g.Value.TotalSleep).First();
            Console.WriteLine($"Guard {sleepyGuard.Value.ID}");
            Console.WriteLine($"Minute {Array.IndexOf(sleepyGuard.Value.Asleep, sleepyGuard.Value.Asleep.Max())}");
            return (int.Parse(sleepyGuard.Value.ID) * Array.IndexOf(sleepyGuard.Value.Asleep, sleepyGuard.Value.Asleep.Max())).ToString(); 
            
        }

        public string AnswerB()
        {
            var Info = new List<InfoLine>();
            foreach (var line in input.Split('\n'))
            {
                Info.Add(new InfoLine(line.Trim()));
            }
            var sorted = Info.OrderBy(i => i.date);

            var Guards = new Dictionary<string, Guard>();
            Guard selectedGuard = null;

            DateTime sleep = DateTime.Now;
            DateTime wake = DateTime.Now;
            foreach (var item in sorted)
            {
                if (item.eventtype == "Guard")
                {
                    if (!Guards.ContainsKey(item.tag))
                        Guards.Add(item.tag, new Guard() { ID = item.tag });
                    selectedGuard = Guards[item.tag];
                }
                else if (item.eventtype == "falls")
                {
                    sleep = item.date;
                }
                else if (item.eventtype == "wakes")
                {
                    wake = item.date;

                    int startminute = 0;
                    if (sleep.Hour == 0)
                        startminute = sleep.Minute;

                    for (int i = startminute; i < wake.Minute; i++)
                    {
                        selectedGuard.Asleep[i]++;
                    }
                }
            }

            int maxSleep = -1;
            int maxSleepIndex = -1;
            int maxSleepID = -1;
            foreach (var g in Guards)
            {
                for (int i = 0; i < 60; i++)
                {
                    if (g.Value.Asleep[i] > maxSleep)
                    {
                        maxSleep = g.Value.Asleep[i];
                        maxSleepIndex = i;
                        maxSleepID = int.Parse(g.Value.ID);
                    }
                }
            }
            return (maxSleepID * maxSleepIndex).ToString();
        }
        string testinput = @"[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-03 00:24] falls asleep
[1518-11-03 00:29] wakes up
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-04 00:36] falls asleep
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up";

    }
}
