// https://open.kattis.com/problems/reversiblecyclic
#include <iostream>
#include <string>
#include <algorithm>

int main() {
    std::string line;    
    std::cin >> line;
    
    // Determine proper cyclic substrings t of s
    // Determine if reverse of t is still a cyclic substring of s
    // cyclic substring: is t a substring of strcat(s,s)
    // reverse cyclic: is reverse of t a substring of strcat(s,s)
    // must work for all substring t of s
    // reverse of s must be in strcat(s,s)
    auto ss = line + line;
    std::reverse(line.begin(), line.end());
    auto found = ss.find(line);
    if (found == std::string::npos)
        std::cout << '0' << std::endl;
    else
        std::cout << '1' << std::endl;
    
    
}