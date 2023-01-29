#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <set>
#include <deque>

using namespace std;

string toSnafu(long n){
    long cur = 1;
    vector<char> answer;

    while(n > 0){
        long next = cur * 5;
        long rem = n % next;
        char toAdd = '0';

        if (rem > cur * 2){
            rem = next - rem;
            if (rem == cur) toAdd = '-';
            else if (rem == 2* cur) toAdd = '=';
            n += rem;
        } else {
            if (rem == cur) toAdd = '1';
            else if (rem == 2* cur) toAdd = '2';
            n -= rem;
        }
        answer.push_back(toAdd);
        cur *= 5;
    }

    return string(answer.rbegin(), answer.rend());
}

long fromSnafu(string s){
    long answer = 0;
    long cur = 1;
    for (auto it = s.crbegin() ; it != s.crend(); ++it) {
        if (*it == '2') answer += cur * 2;
        else if (*it == '1') answer += cur;
        else if (*it == '-') answer -= cur;
        else if (*it == '=') answer -= cur * 2;
        cur *= 5;
    }
    return answer;
}

int main(){
    ifstream file("day25.txt");
    long sum = 0;
    while (file.good())
    {
        string line;
        getline(file, line);
        sum += fromSnafu(line);
    }
    file.close();

    cout << "Part 1: " << toSnafu(sum) << endl;
}