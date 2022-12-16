#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>

using namespace std;

struct point
{
    int x;
    int y;
};

vector<point> process_line(const string &line)
{
    vector<point> actions;
    int b = 0;
    int x = 0;
    int y = 0;
    for (int i = 0; i < line.length(); i++)
    {
        if (line[i] == ',')
        {
            x = stoi(line.substr(b, i - b));
            i++;
            b = i;
        }
        else if (line[i] == '-')
        {
            y = stoi(line.substr(b, i - b));
            i += 3;
            b = i;
            point newpoint = {x, y};
            actions.push_back(newpoint);
        }
    }
    y = stoi(line.substr(b));
    point newpoint = {x, y};
    actions.push_back(newpoint);

    return actions;
}

bool drop_sand(vector<vector<char>> &map, int sy, int sx, bool has_void = true)
{
    int y = sy;
    int x = sx;
    int maxy = map.size();
    int maxx = map[0].size();
    while (true)
    {
        if (y < maxy - 1 && map[y + 1][x] == '.')
        {
            y = y + 1;
        }
        else if (y < maxy - 1 && x > 0 && map[y + 1][x - 1] == '.')
        {
            y = y + 1;
            x = x - 1;
        }
        else if (y < maxy - 1 && x < maxx - 1 && map[y + 1][x + 1] == '.')
        {
            y = y + 1;
            x = x + 1;
        }
        else if (x > 0 && x < maxx - 1 && y > 0 && y < maxy - 1)
        {
            map[y][x] = 'o';
            return true;
        }
        else if (has_void)
        {
            return false;
        }
        else if (y == sy && x == sx)
        {
            return false;
        }
        else
        {
            map[y][x] = 'o';
            return true;
        }
    }
}

void draw_map(vector<vector<char>> &map)
{
    int h = map.size();
    int w = map[0].size();
    for (int y = 0; y < h; y++)
    {
        for (int x = 0; x < w; x++)
        {
            cout << map[y][x];
        }
        cout << endl;
    }
}

int main()
{
    ifstream file("day14.txt");
    vector<vector<point>> lines;
    while (file.good())
    {
        string line;
        getline(file, line);
        if (line.length() == 0)
            continue;

        lines.push_back(process_line(line));
    }
    file.close();

    int minx = 500, miny = 0;
    int maxx = 500, maxy = 0;
    for (auto const &line : lines)
    {
        for (auto const &p : line)
        {
            if (p.x < minx)
                minx = p.x;
            if (p.y < miny)
                miny = p.y;
            if (p.x > maxx)
                maxx = p.x;
            if (p.y > maxy)
                maxy = p.y;
        }
    }
    vector<vector<char>> map;
    int w = maxx - minx + 1;
    int h = maxy - miny + 1;
    for (int i = 0; i < h; i++)
    {
        map.push_back(vector<char>(w, '.'));
    }

    for (auto const &line : lines)
    {
        for (int i = 1; i < line.size(); i++)
        {
            int x1 = min(line[i - 1].x, line[i].x);
            int x2 = max(line[i - 1].x, line[i].x);
            for (int x = x1; x <= x2; x++)
            {
                int y1 = min(line[i - 1].y, line[i].y);
                int y2 = max(line[i - 1].y, line[i].y);
                for (int y = y1; y <= y2; y++)
                {
                    map[y - miny][x - minx] = '#';
                }
            }
        }
    }
    map[0 - miny][500 - minx] = ',';

    int count = 0;
    while (drop_sand(map, 0 - miny, 500 - minx))
    {
        count++;
        // draw_map(map);
    }

    cout << "Part 1: " << count << endl;

    for (int y = 0; y < h; y++)
    {
        for (int x = 0; x < w; x++)
        {
            if (map[y][x] == 'o')
                map[y][x] = '.';
        }
        map[y].insert(map[y].begin(), h + 1, '.');
        map[y].insert(map[y].end(), h + 1, '.');
    }
    minx -= h + 1;
    w = map[0].size();
    map.push_back(vector<char>(w, '.'));
    map.push_back(vector<char>(w, '#'));

    int count2 = 0;
    while (drop_sand(map, 0 - miny, 500 - minx, false))
    {
        count2++;
        // draw_map(map);
    }

    cout << "Part 2: " << count2 + 1 << endl;
}