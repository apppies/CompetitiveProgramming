#include <iostream>
#include <sstream>
#include <string>
#include <vector>

using namespace std;

void print(vector<vector<int>> stack){

    for (size_t i = 0; i < stack.size(); i++)
    {
        cout << i << ":";
        for (size_t j = 0; j < stack[i].size(); j++)
        {
            cout << " " << stack[i][j];
        }
        cout << endl;        
    }
}

int main()
{
    string line;

    int n;
    cin >> n;

    // Keep track in which stack each block is
    vector<int> blocks(n);
    // Put every block on the bottom of its own stack
    vector<vector<int>> stack(n);
    for (size_t i = 0; i < n; i++)
    {
        blocks[i] = i;
        stack[i].push_back(i);
    }

    while (getline(cin, line))
    {
        if (line == "quit")
            break;
        if (line == "")
            continue;

        stringstream ss;
        ss << line;

        string action, position;
        int a, b;
        ss >> action >> a >> position >> b;

        if (a == b || blocks[a] == blocks[b]){
            continue;
        }
        
        if (action == "move")
        {
            // Move blocks above a to initial position
            while (stack[blocks[a]].back() != a)
            {
                int x = stack[blocks[a]].back();
                stack[blocks[a]].pop_back();
                stack[x].push_back(x);
                blocks[x] = x;
            }

            if (position == "onto")
            {
                // Same for stack b
                while (stack[blocks[b]].back() != b)
                {
                    int x = stack[blocks[b]].back();
                    stack[blocks[b]].pop_back();
                    blocks[x] = x;
                    stack[x].push_back(x);
                }
            }

            // Move a onto b
            stack[blocks[a]].pop_back();
            stack[blocks[b]].push_back(a);
            blocks[a] = blocks[b];
        }
        else if (action == "pile")
        {
            vector<int> q(0);
            // Move blocks above a into queue
            while (stack[blocks[a]].back() != a)
            {
                int x = stack[blocks[a]].back();
                stack[blocks[a]].pop_back();
                q.push_back(x);
            }
            stack[blocks[a]].pop_back(); // include a
            q.push_back(a);

            if (position == "onto")
            {
                // remove all blocks above b
                while (stack[blocks[b]].back() != b)
                {
                    int x = stack[blocks[b]].back();
                    stack[blocks[b]].pop_back();
                    stack[x].push_back(x);
                    blocks[x] = x;
                }
            }

            // Stack a and blocks above on b
            while (!q.empty()) {
                int x = q.back();
                q.pop_back();
                stack[blocks[b]].push_back(x);
                blocks[x] = blocks[b];
            }
        }

        
    }
print(stack);
    
}