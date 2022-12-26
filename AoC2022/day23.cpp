#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <set>

using namespace std;

struct Position
{
    int x;
    int y;
};

struct Blizzard
{
    int x;
    int y;
    int dir;

    int hash() { return (y + 1) * 1000 + (x + 1) * 10 + dir; }
    void move(int maxx, int maxy)
    {
        switch (dir)
        {
        case 0:
            x++;
            if (x > maxx)
                x = 1;
            break;

        case 1:
            y++;
            if (y > maxy)
                y = 1;
            break;

        case 2:
            x--;
            if (x < 1)
                x = maxx;
            break;

        case 3:
            y--;
            if (y < 1)
                y = maxy;
            break;
        }
    }
};
vector<Blizzard> blizzards;

void drawmap(set<pair<int, int>> &positions, int maxx, int maxy)
{
    vector<string> output;
    output.push_back(string(maxx + 2, '#'));
    for (int i = 0; i < maxy; i++)
    {
        output.push_back(string(maxx + 2, ' '));
    }
    output.push_back(string(maxx + 2, '#'));

    output[0][1] = '.';
    output[maxy + 1][maxx] = '.';

    for (auto b : blizzards)
    {
        if (b.dir == 0)
            output[b.y][b.x] = '>';
        if (b.dir == 1)
            output[b.y][b.x] = 'v';
        if (b.dir == 2)
            output[b.y][b.x] = '<';
        if (b.dir == 3)
            output[b.y][b.x] = '^';
    }
    for (auto p : positions)
    {
        output[p.second][p.first] = 'p';
    }

    for (auto &line : output)
    {
        cout << line << endl;
    }
    cout << endl;
}

int solve(int startx, int starty, int endx, int endy, int maxx, int maxy)
{
    int minutes = 0;
    int x = startx, y = starty;
    set<pair<int, int>> positions;
    set<pair<int, int>> newpositions;
    positions.insert({x, y});
    bool done = false;
    while (!done)
    {
        vector<vector<int>> blizzmap;
        for (int i = 0; i < maxy + 2; i++)
        {
            blizzmap.push_back(vector<int>(maxx + 2, 0));
        }

        for (auto &b : blizzards)
        {
            b.move(maxx, maxy);
            blizzmap[b.y][b.x]++;
        }

        for (auto &pos : positions)
        {

            if (blizzmap[pos.second][pos.first] == 0)
                newpositions.insert(pos);

            if (pos.first == 1 && pos.second == 0 && blizzmap[pos.second + 1][pos.first] == 0)
                newpositions.insert({pos.first, pos.second + 1});
            if (pos.first == maxx && pos.second == maxy + 1 && blizzmap[pos.second - 1][pos.first] == 0)
                newpositions.insert({pos.first, pos.second - 1});

            if (pos.first == 1 && pos.second == 1)
                newpositions.insert({pos.first, pos.second - 1});

            if (pos.first == maxx && pos.second == maxy)
                newpositions.insert({pos.first, pos.second + 1});

            if (pos.second > 0 && pos.second <= maxy)
            {
                if (pos.first < maxx && blizzmap[pos.second][pos.first + 1] == 0)
                    newpositions.insert({pos.first + 1, pos.second});
                if (pos.first > 1 && blizzmap[pos.second][pos.first - 1] == 0)
                    newpositions.insert({pos.first - 1, pos.second});
                if (pos.second < maxy && blizzmap[pos.second + 1][pos.first] == 0)
                    newpositions.insert({pos.first, pos.second + 1});
                if (pos.second > 1 && blizzmap[pos.second - 1][pos.first] == 0)
                    newpositions.insert({pos.first, pos.second - 1});
            }
        }

        positions.clear();
        swap(positions, newpositions);

        for (auto &pos : positions)
        {
            if (pos.first == endx && pos.second == endy)
            {
                done = true;
                return minutes + 1;
            }
        }

        cout << minutes << endl;
        drawmap(positions, maxx, maxy);
        minutes++;
    }
    return minutes;
}

int main()
{
    vector<string> map;

    ifstream file("day23.txt");

    while (file.good())
    {
        string line;
        getline(file, line);
        if (line.length() == 0)
            break;
        else
            map.push_back(line);
    }
    file.close();

    for (int i = 0; i < map.size(); i++)
    {
        for (int j = 0; j < map[0].size(); j++)
        {
            int dir = -1;
            if (map[i][j] == '>')
                dir = 0;
            else if (map[i][j] == 'v')
                dir = 1;
            else if (map[i][j] == '<')
                dir = 2;
            else if (map[i][j] == '^')
                dir = 3;

            if (dir >= 0)
                blizzards.push_back({j, i, dir});
        }
    }

    int startx = 1, starty = 0;
    int endx = map[0].size() - 2, endy = map.size() - 1;
    int maxx = map[0].size() - 2, maxy = map.size() - 2;

    int minutes = solve(startx, starty, endx, endy, maxx, maxy);

    cout << "Part 1: " << minutes << endl;

    int back = solve(endx, endy, startx, starty, maxx, maxy);
    int forward = solve(startx, starty, endx, endy, maxx, maxy);
    cout << "Part 2 back: " << back << endl;
    cout << "Part 2 forward: " << forward << endl;
    cout << "Part 2 total: " << minutes + back + forward << endl;
}