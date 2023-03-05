#include <string>
#include <iostream>
#include <vector>
#include <sstream>
#include <cmath>
#include <iomanip>

int main()
{
    const double pi = std::acos(-1);

    while (true)
    {
        int n;
        std::cin >> n;
        if (n == 0)
            break;

        std::vector<std::pair<double, double>> endpoints;

        for (int i = 0; i < n; i++)
        {
            std::string line;
            while (line == "")
            {std::getline(std::cin, line);}
            std::stringstream ss(line);

            double angle = 0;
            double x = 0, y = 0;
            ss >> x >> y;
            std::string command;
            while(ss >> command){
                double n;
                ss >> n;

                if (command == "start") {
                    angle = n;
                }
                else if (command == "turn") {
                    angle += n;
                }
                else if (command == "walk") {
                    x += std::cos(angle / 180 * pi) * n;
                    y += std::sin(angle / 180 * pi) * n;
                }
            }
            endpoints.push_back(std::make_pair(x,y));
        }

        double x_avg = 0, y_avg = 0;
        for (int i = 0; i < n; i++)
        {
            x_avg += endpoints[i].first / n;
            y_avg += endpoints[i].second / n;
        }

        double worst = 0;
        for (int i = 0; i < n; i++)
        {
            double dist = std::sqrt(std::pow(endpoints[i].first - x_avg, 2) + std::pow(endpoints[i].second - y_avg, 2));
            if (dist > worst)
                worst = dist;
        }
        
        

        std::cout << std::fixed;
        std::cout << std::setprecision(4);
        std::cout << x_avg << " " << y_avg << " " << std::setprecision(5) << worst << std::endl;
        
    }
}