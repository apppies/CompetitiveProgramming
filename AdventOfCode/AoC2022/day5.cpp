#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>

using namespace std;

int main() {
    ifstream file("day5.txt");
    vector<string> lines;
    while (file.good()){
        string line;
        getline(file, line);
        lines.push_back(line);
    }
    file.close();

    vector<stack<char>> stacks;
    vector<list<char>> stacks2;

    for (int i = 0; i < lines.size(); i++)
    {
        if (lines[i].length() == 0)
            continue;

        if (lines[i][1] == '1') // Build container stack
        {
            int n = stoi(lines[i].substr(lines[i].length() - 3));
            stacks = vector<stack<char>>(n);
            stacks2 = vector<list<char>>(n);
            for (int j = 0; j < n; j++)
            {
                stacks[j] = stack<char>();
                stacks2[j] = list<char>();
            }
            
            for (int j = i - 1; j >= 0 ; j--)
            {
                for (int k = 0; k < n; k++)
                {
                    char c = lines[j][k * 4 + 1];
                    if (c != ' '){
                        stacks[k].push(c);
                        stacks2[k].push_back(c);
                    }
                }
            }
        }
        else if (lines[i][0] == 'm') // Move containers around
        {
            int amount, from, to;
            sscanf(lines[i].c_str(), "move %d from %d to %d", &amount, &from, &to);

            // Part 1
            for (int j = 0; j < amount; j++)
            {
                char T = stacks[from - 1].top();
                stacks[from - 1].pop();
                stacks[to - 1].push(T);
            }
            
            // Part 2
            stacks2[to - 1].splice(stacks2[to - 1].end(), stacks2[from - 1], prev(stacks2[from - 1].end(), amount), stacks2[from - 1].end());
        }
    }

    cout << "Part 1: ";
    for( auto const& st: stacks){
        cout << st.top();
    }    
    cout << endl;
    
    cout << "Part 2: ";
    for( auto const& st: stacks2){
        cout << st.back();
    }    
    cout << endl;
}
