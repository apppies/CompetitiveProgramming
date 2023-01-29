#include <iostream>
#include <vector>
#include <stack>
#include <string>
#include <sstream>

using namespace std;

int matrix[25][25];
int n;

vector<pair<int, int>> getNeighbours(int x, int y)
{
    vector<pair<int, int>> neighbours;

    if (x > 0 && y > 0 && matrix[x - 1][y - 1] == 1)
        neighbours.push_back{{x - 1, y - 1});
    if (y > 0 && matrix[x][y - 1] == 1)
        neighbours.push_back({x, y - 1});
    if (x < n - 1 && y > 0 && matrix[x + 1][y - 1] == 1)
        neighbours.push_back({x + 1, y - 1});

    if (x > 0 && matrix[x - 1][y] == 1)
        neighbours.push_back({x - 1, y});

    if (x < n - 1 && matrix[x + 1][y] == 1)
        neighbours.push_back({x + 1, y});

    if (x > 0 && y < n - 1 && matrix[x - 1][y + 1] == 1)
        neighbours.push_back({x - 1, y + 1});
    if (y < n - 1 && matrix[x][y + 1] == 1)
        neighbours.push_back({x, y + 1});
    if (x < n - 1 && y < n - 1 && matrix[x + 1][y + 1] == 1)
        neighbours.push_back({x + 1, y + 1});

    return neighbours;
}

void clearGroup(int x, int y)
{
    vector<pair<int, int>> neighbours;

    matrix[x][y] = -1;
    neighbours = getNeighbours(x, y);
    while (neighbours.size() > 0)
    {
        for (size_t i = 0; i < neighbours.size(); i++)
        {
            matrix[neighbours[i].first][neighbours[i].second] = -1;
        }

        vector<pair<int, int>> newNeighbours;
        for (size_t i = 0; i < neighbours.size(); i++)
        {
            auto addNeighbours = getNeighbours(neighbours[i].first, neighbours[i].second);
            for (auto nei : addNeighbours)
            {
                newNeighbours.push_back(nei);
            }
        }
        neighbours = newNeighbours;
    }
}

int count()
{
    int c = 0;

    for (size_t y = 0; y < n; y++)
    {
        for (size_t x = 0; x < n; x++)
        {
            if (matrix[x][y] == 1)
            {
                clearGroup(x, y);
                c++;
            }
        }
    }
    return c;
}

int main()
{
int image = 1;
    for (string line; getline(cin, line);)
    {
        if (line.empty())
        {
            return 0;
        }

        stringstream stream(line);

        stream >> n;

        for (size_t i = 0; i < n; i++)
        {
            getline(cin, line);
            for (size_t j = 0; j < line.length(); j++)
            {
                matrix[j][i] = (int)(line[j] - '0');
            }
        }

        int ret = count();
        cout << "Image number "<< image <<" contains " << ret << " war eagles." << endl;
        image++;
    }
}