// https://open.kattis.com/problems/boatparts
#include <iostream>
#include <string>
#include <vector>
#include <set>

int main() {

    int P, N;
    std::cin >> P >> N;

    std::set<std::string> uniques;

    for (int n = 0; n < N; n++)
    {
        std::string part;
        std::cin >> part;
        uniques.insert(part);
        if (uniques.size() == P) {
            std::cout << n + 1 << std::endl;
            return 0;
        }
    }
    std::cout << "paradox avoided" << std::endl;

}