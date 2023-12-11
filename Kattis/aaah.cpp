// https://open.kattis.com/problems/aaah
#include <iostream>

int main() {
    std::string jon, doc;
    std::cin >> jon;
    std::cin >> doc;
    if (jon.size() >= doc.size()) {
        std::cout << "go" << std::endl;
    } else {
        std::cout << "no" << std::endl;
    }
}