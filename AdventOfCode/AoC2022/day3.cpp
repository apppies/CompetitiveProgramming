#include <fstream>
#include <vector>
#include <iostream>
#include <set>
#include <algorithm>
using namespace std;

int main() {
    ifstream file;
    string line;
    vector<string> lines;

    file.open("day3.txt");
    while (getline(file,line)){
        lines.push_back(line);
    }
    file.close();

    int sum = 0;
    for (auto const& line: lines) {
        set<char> left;
        set<char> right;
        for (int i = 0; i < line.length() / 2; i++)
        {
            left.insert(line[i]);
            right.insert(line[i + line.length() / 2]);
        }
        set<char> intersection;
        set_intersection(left.begin(), left.end(), right.begin(), right.end(), inserter(intersection, intersection.begin()));

        char item = *intersection.begin();
        int value = 0;
        if (item >= 'a') value = (int)(item - 'a') + 1;
        else value = (int)(item - 'A') + 27;

        sum += value;
    }
    cout << "Part 1: " << sum << endl;

    sum = 0;
    for (int i = 0; i < lines.size(); i+= 3)
    {
        set<char> elf1;
        for (auto c: lines[i])
            elf1.insert(c);
        set<char> elf2;
        for (auto c: lines[i+1])
            elf2.insert(c);
        set<char> elf3;
        for (auto c: lines[i+2])
            elf3.insert(c);

        set<char> intersection;
        set_intersection(elf1.begin(), elf1.end(), elf2.begin(), elf2.end(), inserter(intersection, intersection.begin()));

        set<char> intersection2;
        set_intersection(intersection.begin(), intersection.end(), elf3.begin(), elf3.end(), inserter(intersection2, intersection2.begin()));

        char item = *intersection2.begin();
        int value = 0;
        if (item >= 'a') value = (int)(item - 'a') + 1;
        else value = (int)(item - 'A') + 27;

        sum += value;
    }
    
    cout << "Part 2: " << sum << endl;
}