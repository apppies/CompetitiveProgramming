#include <iostream>
#include <fstream>
#include <vector>
#include <bits/stdc++.h>
#include <algorithm>

using namespace std;

int main(){
    ifstream file;
    file.open("day1.txt");
    string line;

    vector<int> sums;
    int sum = 0;

    while (getline(file,line)){
        if (line.size() > 0){
            sum += stoi(line);
        }
        else {
            if (sum > 0) {
                sums.push_back(sum);
                sum = 0;
            }
        }
    }
    file.close();

    sort(sums.begin(), sums.end(), greater<int>());
    
    // Part 1
    cout << "Part 1:" <<sums[0] << endl;
    // Part 2
    cout << "Part 2:" << reduce(sums.begin(), sums.begin() + 3) << endl;
    
}