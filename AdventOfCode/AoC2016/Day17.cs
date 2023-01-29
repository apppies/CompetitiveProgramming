using System.Security.Cryptography;

namespace AoC2016
{
    class Day17
    {
        string shortestroute = "";
        int shortestlength = 10;

        public void Solve()
        {
            md5 = MD5.Create();
            var input = "pxxbnzuo";
            int x = 0, y = 0;

            nextRoom(x, y, input);
            Console.WriteLine(shortestroute.Substring(input.Length));
        }
        MD5 md5; byte[] hash; int counter;

        class Field
        {
            public string Route { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }
        private void nextRoom(int newx, int newy, string input)
        {
            var paths = new List<Field>();
            int longestroute = 0;
            paths.Add(new Field() { Route = input, x = newx, y = newy });
            while (paths.Count > 0)
            {
                var newpaths = new List<Field>();
                for (int i = 0; i < paths.Count; i++)
                {
                    var f = paths[i];
                    //We have arrived
                    if (f.x == 3 && f.y == 3)
                    {
                        if (f.Route.Length > longestroute)
                            longestroute = f.Route.Length;
                        if (shortestroute.Length == 0 || f.Route.Length < shortestroute.Length)
                            shortestroute = f.Route;
                    }
                    else
                    {

                        //Find next doors
                        hash = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(f.Route));
                        var code = hash[0].ToString("x2") + hash[1].ToString("x2");
                        if (code[0] > 'a' && f.y > 0)
                            newpaths.Add(new Field() { Route = f.Route + "U", x = f.x, y = f.y - 1 });
                        if (code[1] > 'a' && f.y < 3)
                            newpaths.Add(new Field() { Route = f.Route + "D", x = f.x, y = f.y + 1 });
                        if (code[2] > 'a' && f.x > 0)
                            newpaths.Add(new Field() { Route = f.Route + "L", x = f.x - 1, y = f.y });
                        if (code[3] > 'a' && f.x < 3)
                            newpaths.Add(new Field() { Route = f.Route + "R", x = f.x + 1, y = f.y });
                    }
                }
                paths = newpaths;
            }
            Console.WriteLine(longestroute - input.Length);
        }


    }
}