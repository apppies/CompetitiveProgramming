#include <string>
#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>

int main()
{
    std::ifstream file("day2.txt");

    std::string line;
    int sum = 0;
    long powersum = 0;
    while (std::getline(file, line))
    {
        std::stringstream ss(line);
        std::string t;
        int game;

        ss >> t;    // Game
        ss >> game; // Game nr
        ss >> t;    //:

        int count; // num color
        std::string color;

        std::vector<int> max_count(3, 0); // r,g,b
        while (ss >> count)
        {
            ss >> color;
            if (color[0] == 'r')
            {
                max_count[0] = std::max(count, max_count[0]);
            }
            else if (color[0] == 'g')
            {
                max_count[1] = std::max(count, max_count[1]);
            }
            else if (color[0] == 'b')
            {
                max_count[2] = std::max(count, max_count[2]);
            }
        }

        if (max_count[0] <= 12 && max_count[1] <= 13 && max_count[2] <= 14) {
            sum+=game;
        }
        
        int power = max_count[0] * max_count[1] * max_count[2];
        powersum += power;
    }

    std::cout << "A: " << sum << std::endl;
    std::cout << "B: " << powersum << std::endl;
}
