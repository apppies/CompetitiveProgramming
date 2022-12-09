#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>

using namespace std;

int main()
{
    ifstream file("day8.txt");
    vector<vector<int>> map;
    while (file.good())
    {
        string line;
        getline(file, line);
        if (line.length() > 0)
        {
            vector<int> row;
            for (int i = 0; i < line.length(); i++)
            {
                row.push_back(int(line[i] - '0'));
            }
            map.push_back(row);
        }
    }
    file.close();

    int h = map.size();
    int w = map[0].size();

    vector<vector<bool>> visible;
    for (int i = 0; i < h; i++)
    {
        vector<bool> row(w);
        visible.push_back(row);
    }

    for (int y = 0; y < h; y++)
    {

        int heighest = -1;
        for (int x = 0; x < w; x++)
        {
            if (map[y][x] > heighest)
            {
                visible[y][x] = true;
                heighest = map[y][x];
            }
            if (heighest == 9)
            {
                break;
            }
        }
        heighest = -1;
        for (int x = w - 1; x >= 0; x--)
        {
            if (map[y][x] > heighest)
            {
                visible[y][x] = true;
                heighest = map[y][x];
            }
            if (heighest == 9)
            {
                break;
            }
        }
    }

    for (int x = 0; x < w; x++)
    {
        int heighest = -1;
        for (int y = 0; y < h; y++)
        {
            if (map[y][x] > heighest)
            {
                visible[y][x] = true;
                heighest = map[y][x];
            }
            if (heighest == 9)
            {
                break;
            }
        }
        heighest = -1;
        for (int y = h - 1; y >= 0; y--)
        {
            if (map[y][x] > heighest)
            {
                visible[y][x] = true;
                heighest = map[y][x];
            }
            if (heighest == 9)
            {
                break;
            }
        }
    }

    int visiblecount = 0;
    for (int y = 0; y < h; y++)
    {
        for (int x = 0; x < w; x++)
        {
            if (visible[y][x])
            {
                visiblecount++;
            }
        }
    }

    cout << "Part 1: " << visiblecount << endl;

    int highestscore = -1;
    for (int y = 0; y < h; y++)
    {
        for (int x = 0; x < w; x++)
        {
            int c = 0;
            for (int y1 = y - 1; y1 >= 0; y1--)
            {
                c++;
                if (map[y1][x] >= map[y][x])
                {
                    break;
                }
            }
            if (c == 0)
                continue;

            int score = c;
            c = 0;
            for (int y1 = y + 1; y1 < h; y1++)
            {
                c++;
                if (map[y1][x] >= map[y][x])
                {
                    break;
                }
            }
            if (c == 0)
                continue;
            score *= c;

            c = 0;
            for (int x1 = x - 1; x1 >= 0; x1--)
            {
                c++;
                if (map[y][x1] >= map[y][x])
                {
                    break;
                }
            }
            if (c == 0)
                continue;
            score *= c;

            c = 0;
            for (int x1 = x + 1; x1 < h; x1++)
            {
                c++;
                if (map[y][x1] >= map[y][x])
                {
                    break;
                }
            }
            if (c == 0)
                continue;
            ;
            score *= c;

            if (score > highestscore)
                highestscore = score;
        }
    }

    cout << "Part 2: " << highestscore << endl;
}