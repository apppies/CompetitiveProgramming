#include <iostream>
#include <sstream>
#include <string>

using namespace std;

int main()
{
    string line;
    while (getline(cin, line))
    {
        stringstream ss;
        ss << line;
        int i, j;
        ss >> i >> j;
        cout << i << " " << j << " ";

        if (i > j)
            swap(i,j);

        int max_steps = 0;

        for (size_t k = i; k <= j; k++)
        {
            int steps = 1;
            int n = k;
            while (n != 1)
            {
                if (n % 2 == 0)
                {
                    n /= 2;
                }
                else
                {
                    n = 3 * n + 1;
                }
                steps++;
            }
            if (steps > max_steps)
                max_steps = steps;
        }
        cout << max_steps << endl;
    }
}