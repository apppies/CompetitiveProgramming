#include <string>
#include <iostream>
#include <vector>
#include <fstream>
#include <set>

using namespace std;

int dijkstra(vector<vector<int>> graph, vector<vector<int>> weights, int S, int T)
{
    set<pair<int, int>> q; // weight, node
    vector<int> distances(graph.size(), INT32_MAX);
    distances[S] = 0;

    q.insert(make_pair(0, S));

    while (!q.empty())
    {
        int dist = q.begin()->first;
        int node = q.begin()->second;
        q.erase(q.begin());

        if (node == T)
            break;

        for (int i = 0; i < graph[node].size(); i++)
        {
            int nei = graph[node][i];
            int w = weights[node][i];
            int newdist = dist + w;
            if (newdist < distances[nei])
            {
                q.erase(make_pair(distances[nei], nei));
                distances[nei] = newdist;
                q.insert(make_pair(distances[nei], nei));
            }
        }
    }

    return distances[T];
}

int main()
{
    int N;
    cin >> N;

    for (int i = 0; i < N; i++)
    {
        int n, m, S, T;
        cin >> n >> m >> S >> T;

        vector<vector<int>> graph(n);
        vector<vector<int>> weights(n);

        for (int j = 0; j < m; j++)
        {
            int s1, s2, w;
            cin >> s1 >> s2 >> w;
            graph[s1].push_back(s2);
            weights[s1].push_back(w);

            graph[s2].push_back(s1);
            weights[s2].push_back(w);
        }

        // Find cheapest path from node S to node T
        int cost = dijkstra(graph, weights, S, T);
        if (cost < INT32_MAX)
            cout << "Case #" << i + 1 << ": " << cost << endl;
        else
            cout << "Case #" << i + 1 << ": unreachable" << endl;
    }
}
