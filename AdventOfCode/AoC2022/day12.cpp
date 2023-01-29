#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <queue>
#include <functional>

using namespace std;

vector<vector<int>> map;

int w, h;
list<pair<int, int>> get_neighbours(int const x, int const y)
{
    list<pair<int, int>> neighbours;
    if (x > 0)
        neighbours.push_back({x - 1, y});
    if (x < w - 1)
        neighbours.push_back({x + 1, y});
    if (y > 0)
        neighbours.push_back({x, y - 1});
    if (y < h - 1)
        neighbours.push_back({x, y + 1});
    return neighbours;
}

int main()
{
    ifstream file("day12.txt");

    while (file.good())
    {
        string line;
        getline(file, line);
        vector<int> row;
        for (int i = 0; i < line.length(); i++)
            row.push_back((int)(line[i] - 'a'));
        map.push_back(row);
    }
    file.close();

    h = map.size();
    w = map[0].size();

    vector<vector<int>> visited;
    for (int i = 0; i < h; i++)
    {
        visited.push_back(vector<int>(w, INT32_MAX));
    }

    int heighestvalue = (int)('E' - 'a');
    int startvalue = (int)('S' - 'a');
    pair<int, int> start = {0, 0};
    pair<int, int> end = {0, 0};
    for (int y = 0; y < h; y++)
    {
        for (int x = 0; x < w; x++)
        {
            if (map[y][x] == heighestvalue)
            {
                end = {x, y};
                map[y][x] = (int)('z' - 'a') + 1;
            }

            if (map[y][x] == startvalue)
            {
                start = {x, y};
                map[y][x] = 0;
            }
        }
    }

    queue<pair<int, int>> edge;
    edge.push(start);
    visited[start.second][start.first] = 0;
    while (!edge.empty())
    {
        auto p = edge.front();
        edge.pop();

        int currentheight = map[p.second][p.first];
        int currentprogress = visited[p.second][p.first];

        auto ns = get_neighbours(p.first, p.second);
        for (auto const &n : ns)
        {
            if (map[n.second][n.first] <= currentheight + 1 && visited[n.second][n.first] > currentprogress + 1)
            {
                visited[n.second][n.first] = currentprogress + 1;
                edge.push({n.first, n.second});
            }
        }
    }

    cout << "Part 1: " << visited[end.second][end.first] << endl;

    vector<vector<int>> visited2;
    for (int i = 0; i < h; i++)
    {
        visited2.push_back(vector<int>(w, INT32_MAX));
    }
    queue<pair<int, int>> edge2;
    edge2.push(end);
    visited2[end.second][end.first] = 0;
    while (!edge2.empty())
    {
        auto p = edge2.front();
        edge2.pop();

        int currentheight = map[p.second][p.first];
        int currentprogress = visited2[p.second][p.first];

        auto ns = get_neighbours(p.first, p.second);
        for (auto const &n : ns)
        {
            if (map[n.second][n.first] >= currentheight - 1 && visited2[n.second][n.first] > currentprogress + 1)
            {
                visited2[n.second][n.first] = currentprogress + 1;
                edge2.push({n.first, n.second});
            }
        }
    }

    int mina = INT32_MAX;
    for (int y = 0; y < h; y++)
    {
        for (int x = 0; x < w; x++)
        {
            if (map[y][x] == (int)('a' - 'a'))
            {
                if (visited2[y][x] < mina)
                {
                    mina = visited2[y][x];
                }
            }
        }
    }

    cout << "Part 2: " << mina << endl;
}