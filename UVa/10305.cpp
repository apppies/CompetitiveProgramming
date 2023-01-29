#include <string>
#include <iostream>
#include <vector>
#include <queue>
#include <fstream>

using namespace std;

vector<vector<int>> children;
vector<vector<int>> parents;
vector<int> tasksleft;

vector<int> bfs(vector<vector<int>> &parents, vector<vector<int>> children){
    queue<int> q;
    for (size_t i = 0; i < parents.size(); i++)
    {
        if (parents[i].size() == 0) {
            q.push(i);
        }
    }
    
    vector<int> path;
    while (!q.empty()){
        int h = q.front();
        q.pop();
        path.push_back(h);

        for (auto const & child: children[h])
        {
            tasksleft[child]--;
            if (tasksleft[child] == 0){
                q.push(child);
            }
        }      
    }
    return path;
}

int main() {
    int n, m;

    while (true)
    {    
        cin >> n >> m;
        if (n == 0 && m == 0)
            break; 

        children = vector<vector<int>>(n);
        parents = vector<vector<int>>(n);
        tasksleft = vector<int>(n, 0);

        for (size_t j = 0; j < m; j++)
        {
            int from, to;
            cin >> from >> to;
            from--;
            to--;
            children[from].push_back(to);
            parents[to].push_back(from);
            tasksleft[to]++;
        }
        
        vector<int> path = bfs(parents, children);
        cout << path[0] + 1;
        for (size_t j = 1; j < path.size(); j++)
        {   
            cout  << ' ' << path[j] + 1;
        }
        cout << endl;
    }
}
