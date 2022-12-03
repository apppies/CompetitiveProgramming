using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020
{
    class Day14
    {
        public void Solve()
        {
            var input = File.ReadAllText("input14.txt");
            var lines = input.Split(new char[] { '\n' }).Select(s => s.Trim()).ToList();
            var mem = new Dictionary<int, ulong>();
            ulong mask0 = ulong.MaxValue;
            ulong mask1 = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("mask"))
                {
                    (mask0, mask1) = ConvertMask(lines[i].Substring(7));
                }
                else if (lines[i].StartsWith("mem"))
                {
                    var parts = lines[i].Split(new char[] { ']' }).ToArray();
                    int memindex = int.Parse(parts[0].Substring(4));
                    if (!mem.ContainsKey(memindex))
                        mem.Add(memindex, 0);

                    ulong newvalue = ulong.Parse(parts[1].Substring(3));
                    mem[memindex] = ((newvalue & mask0) | mask1);
                }
            }

            ulong sum = 0;
            foreach (var item in mem)
            {
                //Console.WriteLine($"{item.Key}: {item.Value}");
                sum += item.Value;
            }
            Console.WriteLine($"Q1: {sum}");


            var mem2 = new Dictionary<ulong, ulong>();
            var currentmask = "";
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("mask"))
                {
                    currentmask = lines[i].Substring(7);
                }
                else if (lines[i].StartsWith("mem"))
                {
                    var parts = lines[i].Split(new char[] { ']' }).ToArray();
                    ulong memindex = ulong.Parse(parts[0].Substring(4));
                    ulong newvalue = ulong.Parse(parts[1].Substring(3));

                    var address = GetAddress(memindex, currentmask);
                    foreach (var item in address)
                    {
                        if (!mem2.ContainsKey(item))
                            mem2.Add(item, newvalue);
                        else
                            mem2[item] = newvalue;

                    }
                }
            }


            //var mem2 = new Dictionary<ulong, ulong>();

            //var toParse = new Dictionary<uint, uint>();
            //var 

            //for (int i = 0; i < lines.Count; i++)
            //{
            //    var cl = lines[lines.Count - 1 - i];

            //    if (lines[i].StartsWith("mask"))
            //    {
            //        (mask0, mask1) = ConvertMask(lines[i].Substring(7));
            //    }
            //    else if (lines[i].StartsWith("mem"))
            //    {
            //        var parts = lines[i].Split(new char[] { ']' }).ToArray();
            //        uint memindex = uint.Parse(parts[0].Substring(4));
            //        uint newvalue = uint.Parse(parts[1].Substring(3));
            //        toParse.Add(memindex, newvalue);
            //    }


            //}


            Console.WriteLine(totalX);
            sum = 0;
            ulong lastsum = 0;
            foreach (var item in mem2)
            {
                //Console.WriteLine($"{item.Key}: {item.Value}");
                sum += item.Value;
                if (sum < lastsum)
                    Console.WriteLine("OVERFLOW");
                lastsum = sum;
            }
            Console.WriteLine($"Q2: {sum}"); 
            Console.ReadKey();
        }

        static (ulong mask0, ulong mask1) ConvertMask(string mask)
        {
            ulong mask0 = ulong.MaxValue;
            ulong mask1 = 0;
            for (int i = 0; i < mask.Length; i++)
            {
                switch (mask[mask.Length - 1 - i])
                {
                    case '0':
                        mask0 &= ~((ulong)1 << i);
                        break;

                    case '1':
                        mask1 |= ((ulong)1 << i);
                        break;

                    case 'X':
                        break;

                    default:
                        break;
                }
            }
            return (mask0, mask1);
        }
        static int totalX;

        static List<ulong> GetAddress(ulong index, string mask)
        {
            var addr = new List<ulong>();
            ulong start = (index | Convert.ToUInt64(mask.Replace('X', '0'), 2))  & Convert.ToUInt64(mask.Replace('0', '1').Replace('X', '0'), 2);

            addr.Add(start);

            for (int i = 0; i < mask.Length; i++)
            {
                switch (mask[mask.Length - 1 - i])
                {
                    case 'X':
                        var c = addr.Count;
                        for (int j = 0; j < c; j++)
                        {
                            addr.Add(addr[j] | ((ulong)1 << i));
                        }
                        break;
                    default:
                        break;
                }
            }

            var xcount = mask.Count(c => c == 'X');
            totalX += addr.Count;
            Console.Write($"{xcount}:");

            return addr;
        }



        static string test = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";

        static string test2 = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";



        static string test3 = @"mask = 00000000000000000000000000000000000X
mem[0] = 1
mask = 000000000000000000000000000000000000
mem[0] = 10";

    }
}
