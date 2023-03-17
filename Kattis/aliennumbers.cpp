// https://open.kattis.com/problems/aliennumbers
#include <iostream>
#include <string>

int toDec(std::string number, std::string base){
    int ret = 0;
    auto n_base = base.length();
    auto n_num = number.length();
    int power = 1;
    for (size_t i = 0; i < n_num; i++)
    {
        auto c = number[n_num - i - 1];
        auto const index = base.find(c);
        ret += index * power;
        power = power * n_base;
    }
    return ret;
}

std::string fromDec(int number, std::string base) {
    std::string ret;
    auto n_base = base.length();

    while (number > 0) {
        int b = number % n_base;
        auto c = base[b];
        ret.insert(ret.begin(), c);
        number = number / n_base;
    }

    return ret;
}

int main()
{
    int T;
    std::cin >> T;
    for (int t = 0; t < T; t++)
    {
        std::string number;
        std::cin >> number;
        std::string source;
        std::cin >> source;
        std::string target;
        std::cin >> target;

        auto n = toDec(number, source);
        auto out = fromDec(n, target);
        std::cout << "Case #" << t + 1 << ": " << out << std::endl;
    }
    
}


int test()
{
    std::string base = "oF8";
    std::cout << fromDec(toDec("F", base), base) << std::endl;
    std::cout << fromDec(toDec("8", base), base) << std::endl;
    std::cout << fromDec(toDec("Fo", base), base) << std::endl;
    std::cout << fromDec(toDec("FF", base), base) << std::endl;
    std::cout << fromDec(toDec("F8", base), base) << std::endl;
    std::cout << fromDec(toDec("8o", base), base) << std::endl;
    std::cout << fromDec(toDec("8F", base), base) << std::endl;
    std::cout << fromDec(toDec("88", base), base) << std::endl;
    std::cout << fromDec(toDec("Foo", base), base) << std::endl;
    std::cout << fromDec(toDec("FoF", base), base) << std::endl;
}