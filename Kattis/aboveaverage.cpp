// https://open.kattis.com/problems/aboveaverage
#include <iostream>
#include <vector>
#include <iomanip>

int main()
{
    std::cout << std::fixed;
    std::cout << std::setprecision(3);

    int C;
    std::cin >> C;
    for (int i = 0; i < C; i++)
    {
        int N;
        std::cin >> N;
        std::vector<int> grades(N);
        double sum = 0;
        for (int j = 0; j < N; j++)
        {
            std::cin >> grades[j];
            sum += grades[j];
        }
        double avg = sum / N;
        int greater = 0;
        for (int j = 0; j < N; j++)
        {
            if (grades[j] > avg)
            {
                greater++;
            }
        }
        std::cout << (double)greater/N*100.0 << "%" << std::endl;
    }
}