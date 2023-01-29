namespace AoC2016
{
    class Day10
    {

        class bot
        {
            public int Id { get; set; }
            public int? Value1 { get; set; }
            public int? Value2 { get; set; }
            public int LowTo { get; set; }
            public bool LowToBot { get; set; }
            public int HighTo { get; set; }
            public bool HighToBot { get; set; }
            public int Low
            {
                get
                {
                    if (Value1 < Value2)
                        return Value1.Value;
                    else
                        return Value2.Value;
                }
            }
            public int High
            {
                get
                {
                    if (Value1 > Value2)
                        return Value1.Value;
                    else
                        return Value2.Value;
                }
            }
            public void Clear()
            {
                Value1 = null;
                Value2 = null;
            }
            public void Add(int num)
            {
                if (!Value1.HasValue)
                {
                    Value1 = num;
                }
                else if (!Value2.HasValue)
                {
                    Value2 = num;
                }
                else
                    Console.WriteLine("3 items!");
            }
            public bool HasTwo()
            {
                return (Value1.HasValue && Value2.HasValue);
            }
        }
        public void Solve()
        {
            var bots = new Dictionary<int, bot>();
            var outputs = new Dictionary<int, List<int>>();
            var lines = File.ReadAllText("input10.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Split());
            foreach (var line in lines)
            {
                if (line[0].Equals("value"))
                {
                    int value = int.Parse(line[1]);
                    int botid = int.Parse(line[5]);
                    if (bots.ContainsKey(botid))
                    {
                        bots[botid].Add(value);
                    }
                    else
                        bots.Add(botid, new bot { Id = botid, Value1 = value });
                }
                else if (line[0].Equals("bot"))
                {
                    int botid = int.Parse(line[1]);
                    int lowto = int.Parse(line[6]);
                    int highto = int.Parse(line[11]);
                    if (bots.ContainsKey(botid))
                    {
                        bots[botid].LowTo = lowto;
                        bots[botid].LowToBot = line[5].Equals("bot");
                        bots[botid].HighTo = highto;
                        bots[botid].HighToBot = line[10].Equals("bot");
                    }
                    else
                        bots.Add(botid, new bot
                        {
                            Id = botid,
                            LowTo = lowto,
                            LowToBot = line[5].Equals("bot"),
                            HighTo = highto,
                            HighToBot = line[10].Equals("bot")
                        });
                }
            }

            //process 
            bool hastwo = true;
            while (hastwo)
            {
                hastwo = false;
                foreach (var botentry in bots)
                {
                    var bot = botentry.Value;
                    if (bot.HasTwo())
                    {
                        if (bot.Low == 17 && bot.High == 61)
                            Console.WriteLine($"Bot {bot.Id}");

                        if (bot.LowToBot)
                            bots[bot.LowTo].Add(bot.Low);
                        else
                        {
                            if (outputs.ContainsKey(bot.LowTo))
                                outputs[bot.LowTo].Add(bot.Low);
                            else
                                outputs.Add(bot.LowTo, new List<int>() { bot.Low });
                        }
                        if (bot.HighToBot)
                            bots[bot.HighTo].Add(bot.High);
                        else
                        {
                            if (outputs.ContainsKey(bot.HighTo))
                                outputs[bot.HighTo].Add(bot.High);
                            else
                                outputs.Add(bot.HighTo, new List<int>() { bot.High });
                        }
                        bot.Clear();
                        hastwo |= bots[bot.LowTo].HasTwo() || bots[bot.HighTo].HasTwo();
                    }
                }
            }
            Console.WriteLine(outputs[0].First() * outputs[1].First() * outputs[2].First());

        }

    }
}
