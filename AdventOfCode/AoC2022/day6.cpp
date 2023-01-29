#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>

using namespace std;

int main()
{
    ifstream file("day6.txt");
    vector<string> lines;
    while (file.good())
    {
        string line;
        getline(file, line);
        lines.push_back(line);
    }
    file.close();

    string line = lines[0];
    int tag = -1;
    for (int i = 3; i < line.size(); i++)
    {
        if (
            line[i] != line[i - 1] && line[i] != line[i - 2] && line[i] != line[i - 3] &&
            line[i - 1] != line[i - 2] && line[i - 1] != line[i - 3] &&
            line[i - 2] != line[i - 3])
        {
            tag = i + 1;
            break;
        }
    }
    cout << "Part 1: " << tag << endl;

    int tag2 = -1;
    for (int i = 13; i < line.size(); i++)
    {
        bool valid = true;
        for (int j = 0; j < 14; j++)
        {
            for (int k = j + 1; k < 14; k++)
            {
                valid &= line[i - j] != line[i - k];
            }
        }

        if (valid)
        {
            tag2 = i + 1;
            break;
        }
    }
    cout << "Part 2: " << tag2 << endl;
}
