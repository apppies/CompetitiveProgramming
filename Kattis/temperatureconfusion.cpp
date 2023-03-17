// https://open.kattis.com/problems/temperatureconfusion
#include <iostream>
#include <string>
#include <algorithm>

int main(){
    std::string line;
    while (std::getline(std::cin, line)){
        auto slash = line.find('/');
        auto a  = stoi(line.substr(0, slash));
        auto b = stoi(line.substr(slash + 1));

        // F = 9/5 * C + 32
        // C = 5/9 * (F - 32)
        // F = a / b
        // -> C = 5/9 * (a/b - 32b/b) = 5/9*((a-32b)/b) = 5a-5*32b / 9b
        auto up = 5 * a - 5 * 32 * b;
        auto down = 9 * b;

        auto gcd = std::__gcd(up, down);
        if (gcd < 0) // Kattis wants all outputs to have a positive denominator, so we need a positive gcd
            gcd = gcd * -1;

        std::cout << up/gcd << "/" << down/gcd << std::endl;
    }
}