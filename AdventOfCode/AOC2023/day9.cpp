#include <string>
#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include <cmath>

long get_next_number(std::vector<long> numbers) {
    std::vector<long> diffs;
    long min = __LONG_MAX__;
    long max = 0;

    for (size_t i = 0; i < numbers.size() - 1; i++)
    {
        auto diff = numbers[i+1] - numbers[i];
        if (diff < min) min = diff;
        if (diff > max) max = diff;
        diffs.push_back(diff);
    }
    
    if (min == max) {
        return numbers.back() + min;
    }
    else {
        return numbers.back() + get_next_number(diffs);
    }
}

long get_previous_number(std::vector<long> numbers) {
    std::vector<long> diffs;
    long min = __LONG_MAX__;
    long max = 0;

    for (size_t i = 0; i < numbers.size() - 1; i++)
    {
        auto diff = numbers[i+1] - numbers[i];
        if (diff < min) min = diff;
        if (diff > max) max = diff;
        diffs.push_back(diff);
    }
    
    if (min == max) {
        return numbers.front() - min;
    }
    else {
        return numbers.front() - get_previous_number(diffs);
    }
}

void AB() {
    std::ifstream file("day9.txt");
    std::string line;
    long sumA = 0;
    long sumB = 0;

    while (std::getline(file, line))
    {
        std::stringstream ss(line);
        long n;
        std::vector<long> numbers;
        while (ss >> n) {
            numbers.push_back(n);
        }

        auto next = get_next_number(numbers);
        sumA += next;
        auto prev = get_previous_number(numbers);
        sumB += prev;
    }

    std::cout << "1: " << sumA << std::endl;
    std::cout << "2: " << sumB << std::endl;
}

int main() {
    AB();
}