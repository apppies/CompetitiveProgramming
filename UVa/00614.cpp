#include <stdio.h>
#include <sstream>
#include <vector>
#include <iostream>
#include <iomanip>

using namespace std;

vector<int> map;
vector<int> visits;

struct point
{
    int x;
    int y;
    point(int x_, int y_)
    {
        x = x_;
        y = y_;
    }

    int index(int w)
    {
        return x + y * w;
    }
};

bool solveMaze(point start, point finish, int w, int h, int step)
{
    visits[start.index(w)] = step;
    // Check if we made it
    if (start.x == finish.x && start.y == finish.y)
        return true;

    // Check in order: west > north > east > south
    point west(start.x - 1, start.y);
    point east(start.x + 1, start.y);
    point north(start.x, start.y - 1);
    point south(start.x, start.y + 1);
    if (start.x > 0 && (map[west.index(w)] & 1) == 0 && visits[west.index(w)] == -1)
    {
        if (solveMaze(west, finish, w, h, step + 1))
            return true;
    }
    if (start.y > 0 && (map[north.index(w)] & 2) == 0 && visits[north.index(w)] == -1)
    {
        if (solveMaze(north, finish, w, h, step + 1))
            return true;
    }
    if (start.x < w - 1 && (map[start.index(w)] & 1) == 0 && visits[east.index(w)] == -1)
    {
        if (solveMaze(east, finish, w, h, step + 1))
            return true;
    }
    if (start.y < h - 1 && (map[start.index(w)] & 2) == 0 && visits[south.index(w)] == -1)
    {
        if (solveMaze(south, finish, w, h, step + 1))
            return true;
    }

    visits[start.index(w)] = 999; // walk back equals question marks
    return false;
}

int main()
{
    int mazenr = 0;
    int w, h;
    while (cin >> h >> w, w && h)
    {
        // load map
        map = vector<int>(w * h);
        visits = vector<int>(w * h, -1);
        int x, y;
        cin >> y >> x;

        point start(x - 1, y - 1);

        cin >> y >> x;
        point finish(x - 1, y - 1);

        for (size_t i = 0; i < h; i++)
        {
            for (size_t j = 0; j < w; j++)
            {
                int v;
                cin >> v;
                map[i * w + j] = v;
            }
        }

        // run through
        solveMaze(start, finish, w, h, 1);

        mazenr++;
        cout << "Maze " << mazenr << endl
             << endl;

        // print
        for (size_t i = 0; i < h; i++)
        {
            if (i == 0)
            {
                for (size_t j = 0; j < w; j++)
                {
                    cout << "+---";
                }
                cout << "+" << endl;
            }

            for (size_t j = 0; j < w; j++)
            {
                if (j == 0)
                {
                    cout << "|";
                }

                if (visits[j + i * w] == 999)
                {
                    cout << "???";
                }
                else if (visits[j + i * w] > 0)
                {
                    cout << setw(3) << visits[j + i * w];
                }
                else
                {
                    cout << "   ";
                }

                if (map[i * w + j] & 1 || j == w - 1)
                {
                    cout << "|";
                }
                else
                {
                    cout << " ";
                }
            }
            cout << endl;

            // south of points
            for (size_t j = 0; j < w; j++)
            {
                if (j == 0)
                {
                    cout << "+";
                }

                if (map[i * w + j] & 2 || i == h - 1)
                {
                    cout << "---+";
                }
                else
                {
                    cout << "   +";
                }
            }

            cout << endl;
        }
        cout << endl
             << endl;
    }
}