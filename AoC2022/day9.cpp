#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <math.h>
#include <set>

using namespace std;

int main()
{
    ifstream file("day9.txt");
    vector<string> lines;
    while (file.good())
    {
        string line;
        getline(file, line);
        lines.push_back(line);
    }
    file.close();

    bool printmap = false;
    set<int> trace;
    int hx = 0, hy = 0;
    int tx = 0, ty = 0;

    for (auto const &line : lines)
    {
        if (line.length() == 0)
            continue;

        int step = stoi(line.substr(2));

        for (int i = 0; i < step; i++)
        {

            if (line[0] == 'U')
                hy += 1;
            else if (line[0] == 'D')
                hy -= 1;
            else if (line[0] == 'R')
                hx += 1;
            else if (line[0] == 'L')
                hx -= 1;

            int dx = tx - hx;
            int dy = ty - hy;
            if (abs(dx) > 1 && dy == 0)
                tx += -copysign(1, dx);
            else if (abs(dy) > 1 && dx == 0)
                ty += -copysign(1, dy);
            else if (abs(dy) > 1 || abs(dx) > 1)
            {
                tx += -copysign(1, dx);
                ty += -copysign(1, dy);
            }
            trace.insert(tx * 10000 + ty);

            if (printmap)
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 6; x++)
                    {
                        if (y == hy && x == hx)
                            cout << 'H';
                        else if (y == ty && x == tx)
                            cout << 'T';
                        else
                            cout << '.';
                    }
                    cout << endl;
                }
                cout << endl
                     << endl;
            }
        }
    }

    cout << "Part 1: " << trace.size() << endl;
    pair<int, int> head = make_pair(0, 0);
    vector<pair<int, int>> knots(10);
    set<int> trace2;

    for (auto const &line : lines)
    {
        if (line.length() == 0)
            continue;

        int step = stoi(line.substr(2));

        for (int i = 0; i < step; i++)
        {
            if (line[0] == 'U')
                knots[0].first += 1;
            else if (line[0] == 'D')
                knots[0].first -= 1;
            else if (line[0] == 'R')
                knots[0].second += 1;
            else if (line[0] == 'L')
                knots[0].second -= 1;

            hy = knots[0].first;
            hx = knots[0].second;
            for (int k = 1; k < 10; k++)
            {
                ty = knots[k].first;
                tx = knots[k].second;
                int dx = tx - hx;
                int dy = ty - hy;
                if (abs(dx) > 1 && dy == 0)
                    tx += -copysign(1, dx);
                else if (abs(dy) > 1 && dx == 0)
                    ty += -copysign(1, dy);
                else if (abs(dy) > 1 || abs(dx) > 1)
                {
                    tx += -copysign(1, dx);
                    ty += -copysign(1, dy);
                }

                knots[k].first = ty;
                knots[k].second = tx;
                hy = ty;
                hx = tx;
            }
            trace2.insert(knots[9].first * 10000 + knots[9].second);

            if (printmap)
            {
                for (int y = 15; y >= -15; y--)
                {
                    for (int x = -15; x < 15; x++)
                    {
                        bool hit = false;
                        for (int k = 0; k < 10; k++)
                        {
                            if (y == knots[k].first && x == knots[k].second)
                            {
                                cout << k;
                                hit = true;
                                break;
                            }
                        }
                        if (!hit)
                            cout << '.';
                    }
                    cout << endl;
                }
                cout << endl
                     << endl;
            }
        }
    }

    cout << "Part 2: " << trace2.size() << endl;
}