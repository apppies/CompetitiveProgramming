using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020
{
    class Day5
    {
        public void Solve()
        {
            var input = File.ReadAllText("input5.txt");
            var tickets = input.Split(new char[] { '\n' }).Select(s => s.Trim());

            var maxId = 0;
            var ids = new List<int>();
            foreach (var ticket in tickets)
            {
                (int row, int col) = TicketToSeat(ticket);
                var seatId = row * 8 + col;
                ids.Add(seatId);
            }

            ids.Sort();

            Console.WriteLine($"Max seat id {ids.Last()}");
                        
            for (int i = 0; i < ids.Count - 1; i++)
            {
                if (ids[i] == ids[i + 1] - 2)
                    Console.WriteLine($"Your id {ids[i] + 1}");
            }

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

        static string test = @"FBFBBFFRLR";

    }
}
