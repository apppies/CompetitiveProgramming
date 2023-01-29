#include <vector>
#include <iostream>
#include <string>
#include <queue>
using namespace std;

int main()
{
    int cases;
    cin >> cases;

    for (int c = 0; c < cases; c++)
    {
        if (c > 0)
            cout << endl;
        int N, E, T, M;
        cin >> N >> E >> T >> M;
        N++;

        vector<vector<int>> invgraph(N);
        vector<vector<int>> invweights(N);
        for (int i = 0; i < M; i++)
        {
            int from, to, weight;
            cin >> from >> to >> weight;

            invgraph[to].push_back(from);
            invweights[to].push_back(weight);
        }

        // bfs search with ordered queue, starting at E, running for T steps, step backwards and count cells reached
        // Ordered as there might be multiple ways to a cell: a path with more steps will be added later but might need less time
        // using an orderd queue will insert it before the other route is handled (which might be inserted earlier as it needs less steps)
        vector<int> visited(N, -1);
        priority_queue<pair<int, int>, vector<pair<int, int>>, std::greater<pair<int, int>>> q; // pair<int,int> = time, node
        q.push(make_pair(0, E));
        int mice = 0;

        while (!q.empty())
        {
            auto n = q.top();
            q.pop();

            if (visited[n.second] == -1)
            {
                mice++;
                visited[n.second] = n.first;
                auto parents = invgraph[n.second];
                for (int i = 0; i < parents.size(); i++)
                {
                    int t = n.first + invweights[n.second][i];
                    if (t <= T && visited[parents[i]] == -1)
                    {
                        q.push(make_pair(t, parents[i]));
                    }
                }
            }
        }
        cout << mice << endl;
    }
}
