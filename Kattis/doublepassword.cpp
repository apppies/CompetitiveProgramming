// https://open.kattis.com/problems/doublepassword
#include <iostream>
#include <string>
int main()
{
    std::string a, b;
    std::cin >> a >> b;

    int n = 1;
    for (int i = 0; i < 4; i++)
    {
        if (a[i] != b[i])
        {
            n *= 2;
        }
    }

    std::cout << n << std::endl;
}