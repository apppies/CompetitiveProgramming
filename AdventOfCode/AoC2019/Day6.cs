using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day6
    {
        string rawInput;
        public Day6()
        {
            rawInput = File.ReadAllText("input6.txt");
        }
        
        public  void Solve()
        {
            var input = rawInput.Split('\n').Select(s => s.Trim());
        }

    }
}
