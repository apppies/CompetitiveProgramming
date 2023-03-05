#include <iostream>
#include <queue>
#include <vector>
#include <set>
#include <tuple>

using namespace std;
using graph = vector<int>;

graph m_graph;
int m_height;
int m_width;

int idx(int x, int y) { return x + y * m_width; }

vector<int> get_neighbours(int index, vector<bool> &visited)
{
  int x = index % m_width;
  int y = index / m_width;

  // Get unvisited neighbours
  vector<int> neighbours;
  if (x < m_width - 1 && !visited[idx(x + 1, y)])
    neighbours.push_back(idx(x + 1, y));
  if (x > 0 && !visited[idx(x - 1, y)])
    neighbours.push_back(idx(x - 1, y));
  if (y > 0 && !visited[idx(x, y - 1)])
    neighbours.push_back(idx(x, y - 1));
  if (y < m_height - 1 && !visited[idx(x, y + 1)])
    neighbours.push_back(idx(x, y + 1));

  return neighbours;
}

int find_cheapest_route(graph g, int x, int y, int endx, int endy)
{
  vector<int> costs(g.size(), INT32_MAX);
  vector<bool> visited(g.size(), false);
  vector<int> prev(g.size(), -1);
  set<pair<int, int>> edge_nodes;

  int current = idx(x, y);
  int end = idx(endx, endy);
  costs[current] = 0;

  while (current != end)
  {
    visited[current] = true;

    vector<int> neighbours = get_neighbours(current, visited);

    // Update cost to get to neighbour
    for (int nei : neighbours)
    {
      int step = max(g[nei] - g[current], 0);

      if (costs[nei] == INT32_MAX)
      {
        costs[nei] = step;
        edge_nodes.insert(make_pair(costs[nei], nei));
      }
      else
      {
        if (step < costs[nei])
        {
          edge_nodes.erase(edge_nodes.find(make_pair(costs[nei], nei)));
          costs[nei] = step;
          edge_nodes.insert(make_pair(costs[nei], nei));
        }
      }
    }

    auto cheapest = *(edge_nodes.begin());

    int next = cheapest.second;
    prev[next] = current;
    edge_nodes.erase(edge_nodes.begin());

    current = next;
  }

  int largest_ladder = 0;
  while (prev[current] >= 0)
  {
    largest_ladder = max(costs[current], largest_ladder);
    current = prev[current];
  }

  return largest_ladder;
}

int main()
{
  int M, N;
  cin >> M >> N;
  m_width = N;
  m_height = M;

  int min_value = INT32_MAX;
  for (int i = 0; i < M; i++)
  {
    for (int j = 0; j < N; j++)
    {
      int v;
      cin >> v;
      m_graph.push_back(v);
    }
  }

  cout << find_cheapest_route(m_graph, 0, 0, m_width - 1, m_height - 1) << endl;
}