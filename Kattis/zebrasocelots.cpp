// https://open.kattis.com/problems/zebrasocelots
#include <vector>
#include <iostream>

int main()
{
    int n;
    std::cin >> n;
    unsigned long answer = 0;
    for (int i=0; i<n; i++) {
        char in;
        std::cin >> in;
        if (in == 'O') {
            answer |= (1UL << (n - i - 1));
        }
    }
    
    std::cout << answer << std::endl;    
}