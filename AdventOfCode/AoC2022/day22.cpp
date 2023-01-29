#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>

using namespace std;

bool debug = false;

string merge(string const& s1, string const& s2){
    string ret(s1.length(), ' ');
    for (size_t i = 0; i < s1.length(); i++)
    {
        if (s2[i] != ' ') ret[i]=s2[i];
        else ret[i] = s1[i];
    }
    return ret;
}

int main()
{
    ifstream file("day22.txt");
    vector<string> map;
    bool is_map = true;
    string actions;

    while (file.good())
    {
        string line;
        getline(file, line);
        if (line.length() == 0)
            is_map = false;
        else if (is_map)
            map.push_back(line);
        else
            actions = line;
    }
    file.close();

    size_t max_length = 0;
    for (auto &line : map)
    {
        max_length = max(line.length(), max_length);
    }
    for (auto &line : map)
    {
        if (line.length() < max_length)
        {
            line.append(max_length - line.length(), ' ');
        }
    }

    int startx = 0, starty = 0;
    for (int i = 0; i < map[0].length(); i++)
    {
        if (map[0][i] == '.')
        {
            startx = i;
            break;
        }
    }

    int dir = 0;
    int x = startx, y = starty;
    int w = map[0].length(), h = map.size();

    for (size_t a = 0; a < actions.length(); a++)
    {
        if (actions[a] == 'R')
        {
            dir = (dir + 1) % 4;
        }
        else if (actions[a] == 'L')
        {
            dir = ((dir - 1) + 4) % 4;
        }
        else
        {
            size_t n = 1;
            while (a + n < actions.length() && actions[a + n] != 'R' && actions[a + n] != 'L')
            {
                n++;
            }
            int steps = stoi(actions.substr(a, n));
            a += n - 1;

            for (size_t i = 0; i < steps; i++)
            {
                int newx = x, newy = y;
                if (dir == 0) // >
                {
                    do
                    {
                        newx++;
                        if (newx >= w)
                            newx = 0;
                    } while (map[newy][newx] == ' ');
                    if (map[newy][newx] == '.')
                        x = newx;
                }
                else if (dir == 1) // v
                {
                    do
                    {
                        newy++;
                        if (newy >= h)
                            newy = 0;
                    } while (map[newy][newx] == ' ');
                    if (map[newy][newx] == '.')
                        y = newy;
                }
                else if (dir == 2) // <
                {
                    do
                    {
                        newx--;
                        if (newx < 0)
                            newx = w - 1;
                    } while (map[newy][newx] == ' ');
                    if (map[newy][newx] == '.')
                        x = newx;
                }
                else if (dir == 3) // ^
                {
                    do
                    {
                        newy--;
                        if (newy < 0)
                            newy = h - 1;
                    } while (map[newy][newx] == ' ');
                    if (map[newy][newx] == '.')
                        y = newy;
                }
            }
        }
    }

    cout << "Part 1: " << (y + 1) * 1000 + (x + 1) * 4 + dir << endl;


    // Cube sides
    // - 0 1 
    // - 2 -
    // 3 4 -
    // 5 - -


    vector<vector<string>> cube;
    vector<vector<string>> path;
    for (size_t i = 0; i < 6; i++)
    {
        cube.push_back(vector<string>());
        path.push_back(vector<string>());
    }
    
    int side = map[0].length() / 3;
    for (size_t i = 0; i < side; i++)  
    {
        cube[0].push_back(map[i].substr(side,side));
        cube[1].push_back(map[i].substr(2*side,side));
        cube[2].push_back(map[i + 1 * side].substr(side,side));
        cube[3].push_back(map[i + 2 * side].substr(0,side));
        cube[4].push_back(map[i + 2 * side].substr(side,side));
        cube[5].push_back(map[i + 3 * side].substr(0,side));
        for (size_t j = 0; j < 6; j++)
        {
            path[j].push_back(string(side, ' '));
        }
        
    }

    ofstream output;
    if (debug) output = ofstream("output22.txt");
    
    dir = 0;
    x = 0; y = 0;
    w = side; h = side;
    int s = 0;
    path[s][y][x] = '>';

    for (size_t a = 0; a < actions.length(); a++)
    {
        if (actions[a] == 'R')
        {
            if (debug) output << 'R' << endl;
            dir = (dir + 1) % 4;
            if (dir == 0) path[s][y][x] = '>';
            else if (dir == 1) path[s][y][x] = 'v';
            else if (dir == 2) path[s][y][x] = '<';
            else if (dir == 3) path[s][y][x] = '^';  

        }
        else if (actions[a] == 'L')
        {
            if (debug) output << 'L' << endl;
            dir = ((dir - 1) + 4) % 4;

            if (dir == 0) path[s][y][x] = '>';
            else if (dir == 1) path[s][y][x] = 'v';
            else if (dir == 2) path[s][y][x] = '<';
            else if (dir == 3) path[s][y][x] = '^';  
        }
        else
        {
            size_t n = 1;
            while (a + n < actions.length() && actions[a + n] != 'R' && actions[a + n] != 'L')
            {
                n++;
            }
            int steps = stoi(actions.substr(a, n));
            a += n - 1;
            if (debug) output << steps << " steps" << endl;
            for (size_t i = 0; i < steps; i++)
            {
                int newx = x, newy = y, newdir = dir, news = s;
                if (dir == 0) // >
                {
                    newx++;
                    if (newx >= w) {
                        if (s == 0)      { news = 1; newy = y;          newx = 0;       newdir = 0;}
                        else if (s == 1) { news = 4; newy = h - y - 1;  newx = w - 1;   newdir = 2;}
                        else if (s == 2) { news = 1; newy = h - 1;      newx = y;       newdir = 3;}
                        else if (s == 3) { news = 4; newy = y;          newx = 0;       newdir = 0;}
                        else if (s == 4) { news = 1; newy = h - y - 1;  newx = w - 1;   newdir = 2;}
                        else if (s == 5) { news = 4; newy = h - 1;      newx = y;       newdir = 3;}
                    }
                }
                else if (dir == 1) // v
                {
                    newy++;
                    if (newy >= h) {
                        if (s == 0)      { news = 2; newy = 0;          newx = x;       newdir = 1;}
                        else if (s == 1) { news = 2; newy = x;          newx = w - 1;   newdir = 2;}
                        else if (s == 2) { news = 4; newy = 0;          newx = x;       newdir = 1;}
                        else if (s == 3) { news = 5; newy = 0;          newx = x;       newdir = 1;}
                        else if (s == 4) { news = 5; newy = x;          newx = w - 1;   newdir = 2;}
                        else if (s == 5) { news = 1; newy = 0;          newx = x;       newdir = 1;}
                    }  
                }
                else if (dir == 2) // <
                {
                    newx--;
                    if (newx < 0) {
                        if (s == 0)      { news = 3; newy = h - y - 1; newx = 0;       newdir = 0;}
                        else if (s == 1) { news = 0; newy = y;         newx = w - 1;   newdir = 2;}
                        else if (s == 2) { news = 3; newy = 0;         newx = y;       newdir = 1;}
                        else if (s == 3) { news = 0; newy = h - y - 1; newx = 0;       newdir = 0;}
                        else if (s == 4) { news = 3; newy = y;         newx = w - 1;   newdir = 2;}
                        else if (s == 5) { news = 0; newy = 0;         newx = y;       newdir = 1;}
                    }
                }
                else if (dir == 3) // ^
                {
                    newy--;
                    if (newy < 0) {
                        if (s == 0)      { news = 5; newy = x;          newx = 0;   newdir = 0;}
                        else if (s == 1) { news = 5; newy = h-1;        newx = x;   newdir = 3;}
                        else if (s == 2) { news = 0; newy = h-1;        newx = x;   newdir = 3;}
                        else if (s == 3) { news = 2; newy = x;          newx = 0;   newdir = 0;}
                        else if (s == 4) { news = 2; newy = h-1;        newx = x;   newdir = 3;}
                        else if (s == 5) { news = 3; newy = h-1;        newx = x;   newdir = 3;}
                    }
                }

                if (cube[news][newy][newx] == '.'){
                    x = newx;
                    y = newy;
                    dir = newdir;
                    s= news;

                    if (dir == 0) path[s][y][x] = '>';
                    else if (dir == 1) path[s][y][x] = 'v';
                    else if (dir == 2) path[s][y][x] = '<';
                    else if (dir == 3) path[s][y][x] = '^';                    
                }
            }
        }

        if (debug) {
        for (size_t i = 0; i < side; i++)
        {
            output << string(side, ' ') << merge(cube[0][i], path[0][i]) << merge(cube[1][i], path[1][i]) << endl;
        }
        for (size_t i = 0; i < side; i++)
        {
            output << string(side, ' ') << merge(cube[2][i], path[2][i]) << endl;
        }
        for (size_t i = 0; i < side; i++)
        {
            output << merge(cube[3][i], path[3][i]) << merge(cube[4][i], path[4][i]) << endl;
        }
        for (size_t i = 0; i < side; i++)
        {
            output << merge(cube[5][i], path[5][i])<< endl;
        }
        output << endl;
        }
    }

    if (debug) output << s << " " << x << " "<< y << " " << dir<< endl;

    if (s == 0) {x += side;}
    else if (s== 1) {x+=2*side;}
    else if (s == 2) {x+=side; y+= side;}
    else if (s == 3) {x+=0; y+= 2*side;}
    else if (s == 4) {x+=side; y+= 2*side;}
    else if (s == 5) {x+=0; y+= 3*side;}

    if (debug)  output <<  " " << x << " "<< y << " " << dir << endl;

    cout << "Part 2: " << (y + 1) * 1000 + (x + 1) * 4 + dir << endl;
}