using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day9
    {
        string rawInput;
        public Day9()
        {
            rawInput = File.ReadAllText("input9.txt");
        }
        
        public  void Solve()
        {
            Console.WriteLine("Day 9 - Tests");
            var pr = new Intcode("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99");
            pr.Run();
            foreach (var item in pr.Output) { Console.WriteLine(item); };
            pr = new Intcode("1102,34915192,34915192,7,4,7,99,0");
            pr.Run();
            foreach (var item in pr.Output) { Console.WriteLine(item); };
            pr = new Intcode("104,1125899906842624,99");
            pr.Run();
            foreach (var item in pr.Output) { Console.WriteLine(item); };

            Console.WriteLine("Day 9 - 1");
            pr = new Intcode(rawInput);
            pr.RunWithInput(1);
            foreach (var item in pr.Output) { Console.WriteLine(item); };
            Console.WriteLine("Day 9 - 2");
            pr = new Intcode(rawInput);
            pr.RunWithInput(2);
            foreach (var item in pr.Output) { Console.WriteLine(item); };
        }
    }
}
