// https://open.kattis.com/problems/bitbybit
#include <iostream>
#include <string>

int main()
{
    while (true)
    {
        int n;
        std::cin >> n;

        if (n == 0)
            break;

        int bits [32];
        for (size_t i = 0; i < 32; i++)
        {
            bits[i] = -1;
        }

        for (int i = 0; i < n; i++)
        {
            std::string command;
            std::cin >> command;

            if (command == "SET")
            {
                int x;
                std::cin >> x;
                bits[x] = 1;
            }
            else if (command == "CLEAR")
            {
                int x;
                std::cin >> x;
                bits[x] = 0;
            }
            else if (command == "OR")
            {
                int x, y;
                std::cin >> x >> y;
                if (bits[x] == 1 || bits[y] == 1)
                    bits[x] = 1;
            }
            else if (command == "AND")
            {
                int x, y;
                std::cin >> x >> y;
                if (bits[x] == 1 && bits[y] == 1)
                    bits[x] = 1;
                else if (bits[x] == 0 || bits[y] == 0)
                    bits[x] = 0;
                else
                    bits[x] = -1;
            }
        }

        for (int i = 31; i >= 0; i--)
        {
            if (bits[i] >= 0)
                std::cout << bits[i];
            else
                std::cout << '?';
        }
        std::cout << std::endl;
        
        
    }
}
