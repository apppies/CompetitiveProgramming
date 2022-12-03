using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day16
    {
        class BITSPacket
        {
            public int Version;
            public int TypeID;
            public Int64 LiteralValue;
            internal char LengthID;
            internal int SubpacketLength;
            internal int SubpacketCount;
            internal List<BITSPacket> Children;
            internal int PacketLength;

            public BITSPacket()
            {
                Children = new List<BITSPacket>();
            }

            public long GetValue()
            {
                long ret = 0;
                switch (TypeID)
                {
                    case 0:
                        ret = Children.Sum(c => c.GetValue());
                        break;
                    case 1:
                        ret = 1;
                        foreach (var c in Children)
                        {
                            ret *= c.GetValue();
                        }
                        break;
                    case 2:
                        ret = Children.Min(c => c.GetValue());
                        break;
                    case 3:
                        ret = Children.Max(c => c.GetValue());
                        break;
                    case 4:
                        ret = LiteralValue;
                        break;
                    case 5:
                        if (Children[0].GetValue() > Children[1].GetValue())
                            ret = 1;
                        break;
                    case 6:
                        if (Children[0].GetValue() < Children[1].GetValue())
                            ret = 1;
                        break;
                    case 7:
                        if (Children[0].GetValue() == Children[1].GetValue())
                            ret = 1;
                        break;
                    default:
                        break;
                }
                return ret;
            }

        }

        public void Solve()
        {
            var table = new Dictionary<char, string>()
            {
                {'0' , "0000"},
                {'1' , "0001"},
                {'2' , "0010"},
                {'3' , "0011"},
                {'4' , "0100"},
                {'5' , "0101"},
                {'6' , "0110"},
                {'7' , "0111"},
                {'8' , "1000"},
                {'9' , "1001"},
                {'A' , "1010"},
                {'B' , "1011"},
                {'C' , "1100"},
                {'D' , "1101"},
                {'E' , "1110"},
                {'F' , "1111"},
            };
            var input = System.IO.File.ReadAllLines("input16.txt")[0];

            var bytes = "";
            for (int i = 0; i < input.Length; i++)
            {
                bytes += table[input[i]];
            }

            var packetTree = ParsePackets(bytes);

            Console.WriteLine(allPackets.Sum(p => p.Version));
            Console.WriteLine(packetTree[0].GetValue());

        }

         List<BITSPacket> allPackets = new List<BITSPacket>();

        List<BITSPacket> ParsePackets(string bytes, int max = -1)
        {
            var childPackets = new List<BITSPacket>();
            for (int i = 0; i < bytes.Length;)
            {
                //var version = Convert.ToInt32(bytes.Substring(i, 3), 2);
                var typeID = Convert.ToInt32(bytes.Substring(i + 3, 3), 2);
                BITSPacket p;
                int l;
                if (typeID == 4)
                {
                    (p, l) = ParseLiteral(bytes.Substring(i));
                    i += l;
                }
                else
                {
                    (p, l) = ParseOperator(bytes.Substring(i));
                    i += l;
                }
                childPackets.Add(p);

                if (max > 0 && childPackets.Count == max)
                {
                    break;
                }
                if (bytes.Length - i < 11) // 11 is the minimum length of a packet
                {
                    if (bytes.Substring(i).Contains("1"))
                        Console.WriteLine("[-] Not enough bytes left, but not all zero");
                    break;
                }
            }
            return childPackets;
        }

         (BITSPacket p, int l) ParseOperator(string data)
        {
            var l = 0;
            var p = new BITSPacket();
            allPackets.Add(p);

            p.Version = Convert.ToInt32(data.Substring(0, 3), 2);
            p.TypeID = Convert.ToInt32(data.Substring(3, 3), 2);
            l += 6;

            p.LengthID = data[6];
            l++;
            if (p.LengthID == '0')
            {
                p.SubpacketLength = Convert.ToInt32(data.Substring(7, 15), 2);
                l += 15;
                p.Children = ParsePackets(data.Substring(l, p.SubpacketLength));
                l += p.SubpacketLength;
            }
            else
            {
                p.SubpacketCount = Convert.ToInt32(data.Substring(7, 11), 2);
                l += 11;
                p.Children = ParsePackets(data.Substring(l), p.SubpacketCount);
                l += p.Children.Sum(x => x.PacketLength);
            }
            p.PacketLength = l;
            return (p, l);
        }

        (BITSPacket P, int L) ParseLiteral(string data)
        {
            var l = 0;
            var p = new BITSPacket();
            allPackets.Add(p);

            p.Version = Convert.ToInt32(data.Substring(0, 3), 2);
            p.TypeID = Convert.ToInt32(data.Substring(3, 3), 2);
            l += 6;

            var n = "";
            for (int i = 6; i < data.Length; i += 5)
            {
                var d = data.Substring(i + 1, 4);
                n += d;
                l += 5;
                if (data[i] == '0') // Last data blob
                {
                    break;
                }
            }
            p.LiteralValue = Convert.ToInt64(n, 2);
            p.PacketLength = l;
            return (p, l);
        }
    }
}
