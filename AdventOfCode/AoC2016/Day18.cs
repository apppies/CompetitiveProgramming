namespace AoC2016
{
    class Day18
    {
        public void Solve()
        {
            Run(40);
            Run(400000);
        }

        void Run(int n){
            
            var input = "^..^^.^^^..^^.^...^^^^^....^.^..^^^.^.^.^^...^.^.^.^.^^.....^.^^.^.^.^.^.^.^^..^^^^^...^.....^....^.";

            char[] s = input.ToCharArray();
            int total = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    total += s.Count(c => c == '.');
                }
                else
                {
                    var localcount = input.Length;
                    var news = new string('.', input.Length).ToCharArray(); ;
                    for (int j = 0; j < s.Length; j++)
                    {

                        if (j > 0 && j < s.Length - 1)
                        {
                            if (s[j + 1] == '^' && s[j - 1] == '.')
                            {
                                localcount--;
                                news[j] = '^';
                            }
                            if (s[j - 1] == '^' && s[j + 1] == '.')
                            {
                                localcount--;
                                news[j] = '^';
                            }
                        }
                        else if (j == s.Length - 1)
                        {

                            if (s[j - 1] == '^')
                            {
                                localcount--;
                                news[j] = '^';
                            }
                        }
                        else if (j == 0)
                        {
                            if (s[j + 1] == '^')
                            {
                                localcount--;
                                news[j] = '^';
                            }
                        }
                    }
                    s = news;
                    total += localcount;
                }

            }
            Console.WriteLine(total);
        }
    }
}