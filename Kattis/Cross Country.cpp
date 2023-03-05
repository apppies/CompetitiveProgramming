#include <iostream>
#include <string>
#include <vector>
#include <set>

using namespace std;
int dijkstra(std::vector<std::vector<int>> graph, std::vector<std::vector<int>> weights, int S, int T)
{
    std::set<std::pair<int, int>> q; // weight, node
    std::vector<int> distances(graph.size(), INT32_MAX);
    distances[S] = 0;

    q.insert(std::make_pair(0, S));

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
                q.erase(std::make_pair(distances[nei], nei));
                distances[nei] = newdist;
                q.insert(std::make_pair(distances[nei], nei));
            }
        }
    }

    return distances[T];
}

int main(){
    int N, S, T;
    std::cin >> N >> S >> T;

    std::vector<std::vector<int>> graph(N);
    std::vector<std::vector<int>> weights(N);

    for (int i = 0; i < N; i++)
    {
        std::vector<int> Di(N);
        for (int j = 0; j < N; j++)
        {
            graph[i].push_back(j);
            std::cin >> Di[j] ;
        }
        weights[i] = std::move(Di);
    }

    int t = dijkstra(graph, weights, S, T);
    cout << t << endl;    
}