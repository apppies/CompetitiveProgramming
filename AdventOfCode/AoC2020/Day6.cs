using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020
{
    class Day6
    {
        public void Solve()
        {
            var input = File.ReadAllText("input6.txt");
            var lines = input.Split(new char[] { '\n' }).Select(s => s.Trim());
            

            var answers = new HashSet<char>();
            var count = 0;
            foreach (var line in lines)
            {
                foreach (var c in line)
                {
                    answers.Add(c);
                }

                if (string.IsNullOrWhiteSpace(line))
                {
                    count += answers.Count;
                    answers = new HashSet<char>();
                }
            }
            count += answers.Count;
            answers = new HashSet<char>();

            Console.WriteLine($"Sum of counts: {count}");

            lines = input.Split(new char[] { '\n' }).Select(s => s.Trim());
            count = 0;
            var answers2 = new Dictionary<char, int>();
            var nlines = 0;
            foreach (var line in lines)
            {
               

                if (string.IsNullOrWhiteSpace(line))
                {
                    int subcount = 0;
                    foreach (var kv in answers2)
                    {
                        if (kv.Value == nlines)
                            subcount++;
                    }
                    //Console.WriteLine($"{subcount}");
                    count += subcount;
                    answers2 = new Dictionary<char, int>();
                    nlines = 0;
                }
                else
                {
                    nlines++;
                    foreach (var c in line)
                    {
                        if (answers2.ContainsKey(c))
                            answers2[c]++;
                        else
                            answers2.Add(c, 1);

                    }
                }
            }


            Console.WriteLine($"Sum of counts 2: {count}");

            Console.ReadKey();
        }

        static (int row, int column) TicketToSeat(string ticket)
        {
            var r_min = 0;
            var r_max = 127;
            var c_min = 0;
            var c_max = 7;

            for (int i = 0; i < ticket.Length; i++)
            {
                switch (ticket[i])
                {
                    case 'F':
                        r_max = (r_max + r_min + 1) / 2 - 1;
                        break;
                    case 'B':
                        r_min = (r_max + r_min + 1) / 2;
                        break;
                    case 'R':
                        c_min = (c_max + c_min + 1) / 2;
                        break;
                    case 'L':
                        c_max = (c_max + c_min + 1) / 2 - 1;
                        break;

                    default:
                        break;
                }

                //Console.WriteLine($"{r_min} - {r_max} : {c_min} - {c_max}");
            }

            return (r_min, c_min);
        }

        static string test = @"abc

a
b
c

ab
ac

a
a
a
a

b
";

    }
}
