#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>

using namespace std;

int main()
{
    ifstream file("day10.txt");
    vector<string> lines;
    while (file.good())
    {
        string line;
        getline(file, line);
        lines.push_back(line);
    }
    file.close();

    vector<int> cycles;
    vector<int> values;
    int currentcycle = 0;
    int currentvalue = 1;
    for (auto const &line : lines)
    {
        if (line.substr(0, 4) == "noop")
        {
            currentcycle++;
        }
        else if (line.substr(0, 4) == "addx")
        {
            currentvalue += stoi(line.substr(5));
            currentcycle += 2;
            cycles.push_back(currentcycle);
            values.push_back(currentvalue);
        }
    }

    vector<int> checks = {20,60,100,140,180,220};
    int check = 0;
    int sum = 0;
    for (int i = 0; i < cycles.size(); i++)
    {
        if (cycles[i] >= checks[check]) {
            int strength = checks[check] * values[i - 1];
            sum += strength;
            check++;
        }
        if (check >= checks.size())
            break;
    }
    
    cout << "Part 1: " << sum << endl;

    cout << "Part 2: " << endl;
        int spritepos = 1;
    int index = 0;
    for (int i = 0; i < 240; i++)
    {
        if (i >= cycles[index]){
            index++;
            spritepos = values[index - 1];
        }
        int drawing = (i % 40);

        if (spritepos -1 <= drawing && spritepos + 1 >=  drawing) {
            cout << "█";
        } else {
            cout << " ";
        }
        if (drawing == 39){
            cout << endl;
        }

    }

    cout << endl;

}