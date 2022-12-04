#include <fstream>
#include <vector>
#include <iostream>

using namespace std;

int main() {
    ifstream file;
    string line;
    vector<string> lines;

    file.open("day4.txt");
    while (getline(file,line)){
        lines.push_back(line);
    }
    file.close();

    int count = 0;
    int count2 = 0;
    for(auto const& line: lines) {
        int start1, end1, start2, end2;
        sscanf(line.c_str(), "%d-%d,%d-%d", &start1, &end1, &start2, &end2); // Not safe, but good enough for AoC
        // int idx1 = line.find('-');
        // int start1 = stoi(line.substr(0, idx1));
        // int idx2 = line.find(',', idx1 + 1);
        // int end1 = stoi(line.substr(idx1 + 1, idx2 - idx1 - 1));
        // int idx3 = line.find('-', idx2 + 1);
        // int start2 = stoi(line.substr(idx2 + 1, idx3 -  idx2 - 1));
        // int end2 = stoi(line.substr(idx3 + 1));
        if ((start1 >= start2 && end1 <= end2) || (start1 <= start2 && end1 >= end2)) {
            count++;
        }

        if ((start1 >= start2 && start1 <= end2) || start2 >= start1 && start2 <= end1) {
            count2++;
        }
    }

    cout << "Part 1: " << count << endl;
    cout << "Part 2: " << count2 << endl;
}