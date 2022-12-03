using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day14
    {
        string rawInput;
        public Day14()
        {
            rawInput = File.ReadAllText("input14.txt");
        }
        
        class Ingredient
        {
            public string Name { get; set; }
            public Dictionary<Ingredient, int> Ingredients { get; set; } = new Dictionary<Ingredient, int>();
            public int Amount { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }


         Dictionary<Ingredient, long> produced = new Dictionary<Ingredient, long>();
         Dictionary<Ingredient, long> consumed = new Dictionary<Ingredient, long>();
        public  void Solve()
        {

            var lines = rawInput.Split('\n').Select(s => Regex.Matches(s, @"(\d+)\s(\w+)"));
            var recipes = new List<Ingredient>();


            // Create all ingredients
            foreach (var line in lines)
            {
                var i = new Ingredient();
                i.Name = line[line.Count - 1].Groups[2].Value;
                i.Amount = int.Parse(line[line.Count - 1].Groups[1].Value);
                recipes.Add(i);
                produced.Add(i, 0);
                consumed.Add(i, 0);
            }
            var fuel = recipes.First(r => r.Name == "FUEL");
            var ore = new Ingredient { Name = "ORE", Amount = 1 };
            recipes.Add(ore);
            produced.Add(ore, 0);
            consumed.Add(ore, 0);

            // Add all recipies
            foreach (var line in lines)
            {
                var name = line[line.Count - 1].Groups[2].Value;
                for (int i = 0; i < line.Count - 1; i++)
                {
                    var name2 = line[i].Groups[2].Value;
                    var amount2 = int.Parse(line[i].Groups[1].Value);
                    recipes.First(r => r.Name == name).Ingredients.Add(recipes.First(r2 => r2.Name == name2), amount2);
                }
            }

            // Build 1 FUEL
            Build(fuel, 1);
            Console.WriteLine($"Ore: {consumed[ore]}");
            var oreCost = consumed[ore];


            // Reset production
            foreach (var item in recipes)
            {
                produced[item] = 0;
                consumed[item] = 0;
            }
            produced[ore] = 1000000000000;

            // Build maximum fuel
            var fuelCount = produced[ore] / oreCost;
            Build(fuel, fuelCount);

            for (int i = 8; i >= 0; i--)
            {
                var step = (long)Math.Pow(10, i);
                while (produced[ore] >= oreCost * step)
                {
                    Build(fuel, step);
                    fuelCount+=step;
                }
            }
            
            Console.WriteLine($"Fuel: {fuelCount}");
        }

         void Build(Ingredient recipe, long amount)
        {
            if (recipe.Name != "ORE")
            {
                while (produced[recipe] < amount)
                {
                    var needed = (long)Math.Ceiling( (amount - produced[recipe]) / (1.0 * recipe.Amount));
                    foreach (var item in recipe.Ingredients)
                    {
                        Build(item.Key, item.Value * needed);
                    }
                    produced[recipe] += recipe.Amount * needed;
                }
            }
            produced[recipe] -= amount;
            consumed[recipe] += amount;
        }

         string testInput = @"10 ORE => 10 A
1 ORE => 1 B
7 A, 1 B => 1 C
7 A, 1 C => 1 D
7 A, 1 D => 1 E
7 A, 1 E => 1 FUEL";
         string testInput2 = @"9 ORE => 2 A
8 ORE => 3 B
7 ORE => 5 C
3 A, 4 B => 1 AB
5 B, 7 C => 1 BC
4 C, 1 A => 1 CA
2 AB, 3 BC, 4 CA => 1 FUEL";

         string testInput3 = @"171 ORE => 8 CNZTR
7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
114 ORE => 4 BHXH
14 VRPVC => 6 BMBT
6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
5 BMBT => 4 WPTQ
189 ORE => 9 KTJDG
1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
12 VRPVC, 27 CNZTR => 2 XDBXC
15 KTJDG, 12 BHXH => 5 XCVML
3 BHXH, 2 VRPVC => 7 MZWV
121 ORE => 7 VRPVC
7 XCVML => 6 RJRHP
5 BHXH, 4 VRPVC => 5 LTCX";

    }
}
