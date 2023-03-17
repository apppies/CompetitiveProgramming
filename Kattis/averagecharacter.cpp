// https://open.kattis.com/problems/averagecharacter
#include <iostream>
#include <string>

int main(){
    std::string in;
    std::getline(std::cin, in);
    int n = in.size();
    int sum = 0;
    for (int i = 0; i < n; i++)
    {
        sum += in[i];
    }
    std::cout << (char)(sum/n) << std::endl;
}