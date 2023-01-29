#include <fstream>
#include <vector>
#include <iostream>

using namespace std;

int main() {
    ifstream file;
    string line;
    vector<string> lines;

    file.open("day2.txt");
    while (getline(file,line)){
        lines.push_back(line);
    }
    file.close();

    int score = 0;
    for (auto const& line: lines) {
        char elf = line[0];
        char me = line[2] - 'X' + 'A';
        if (elf == me) {
            score += 3;
        } else {
            if ( (elf == 'A' && me =='B') || (elf == 'B' && me =='C') || (elf == 'C' && me =='A') ){
                score += 6;
            }
        }
        score += me - 'A' + 1;
    }

    cout << "Part 1: " << score << endl;

    score = 0;

    for (auto const& line: lines) {
        char elf = line[0];
        char action = line[2];
        char me = elf;
        if (action == 'X') { // win
            if (elf == 'A') me = 'C';
            if (elf == 'B') me = 'A';
            if (elf == 'C') me = 'B';
        } else if (action == 'Z') { // win
            if (elf == 'A') me = 'B';
            if (elf == 'B') me = 'C';
            if (elf == 'C') me = 'A';
        }

        if (elf == me) {
            score += 3;
        } else {
            if ( (elf == 'A' && me =='B') || (elf == 'B' && me =='C') || (elf == 'C' && me =='A') ){
                score += 6;
            }
        }
        score += me - 'A' + 1;
    }

    cout << "Part 2: " << score << endl;
}