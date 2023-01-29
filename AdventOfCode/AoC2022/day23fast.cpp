#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <unordered_set>
#include <unordered_map>
#include <map>
#include <array>

using namespace std;

const long SX = 1000;
const long DX = 500000;
const long WX = 1000;
const long OFFSET = 10000;

long xytoi(long x, long y){
    return (x + 10000) * WX + (y + 10000);
}

void draw_map(vector<long> &elves) {
    long minx = INT64_MAX, miny = INT64_MAX, maxx = INT64_MIN, maxy = INT64_MIN;
    vector<int> cur_map(WX * SX,0);
    for (auto const& elf: elves)
    {
        minx = min(elf / WX, minx);
        miny = min(elf % WX, miny);
        maxx = max(elf/ WX, maxx);
        maxy = max(elf % WX, maxy);
        cur_map[elf] = 1;
    }

    for (int y = miny; y <= maxy; y++){
        for (int x = minx; x <= maxx; x++)
        {
            if (cur_map[xytoi(x,y)]) cout << '#';
            else cout << '.';
        }
        cout << endl;
    }
    cout << endl;
}

int main()
{
    ifstream file("day23.txt");
    vector<string> lines;
    while (file.good())
    {
        string line;
        getline(file, line);
        lines.push_back(line);
    }
    file.close();

    vector<int> elves;
    int cur_map[WX*WX] = { 0 };

    for (int y = 0; y < lines.size(); y++)
    {
        for (int x = 0; x < lines[y].length(); x++)
        {
            if (lines[y][x] == '#')
            {
                elves.push_back(xytoi(x, y));
                cur_map[xytoi(x,y)] = 1;
            }
        }
    }

    long round = 0;
    long elves_moving = 1;
    while (elves_moving > 0)

    {
        map<int, vector<int *>> new_map;
        elves_moving = 0;

        for (auto &elf : elves)
        {

            int ne = elf + 1 - WX; // xytoi(elf.x + 1, elf.y - 1);
            int n =  elf - WX; //xytoi(elf.x,     elf.y - 1);
            int nw = elf - 1 - WX;//xytoi(elf.x - 1, elf.y - 1);
            int w =  elf -1; //xytoi(elf.x - 1, elf.y);
            int sw = elf - 1 + WX;//xytoi(elf.x - 1, elf.y + 1);
            int s =  elf + WX;//xytoi(elf.x,     elf.y + 1);
            int se = elf + 1 + WX;//xytoi(elf.x + 1, elf.y + 1);
            int e = elf + 1;//xytoi(elf.x + 1, elf.y);

            int newelf = elf;

            if (cur_map[ne] || cur_map[n] || cur_map[nw] || cur_map[w] ||
                cur_map[sw] || cur_map[s] || cur_map[se] || cur_map[e])
            {
                for (int i = 0; i < 4; i++)
                {
                    int to_check = (round + i) % 4;
                    if (to_check == 0)
                    {
                        if (!(cur_map[ne] || cur_map[n] || cur_map[nw]))
                        {
                            newelf = n;
                            break;
                        }
                    }
                    else if (to_check == 1)
                    {
                        if (!(cur_map[sw] || cur_map[s] || cur_map[se]))
                        {
                            newelf = s;break;
                        }
                    }
                    else if (to_check == 2)
                    {
                        if (!(cur_map[sw]|| cur_map[w] || cur_map[nw]))
                        {
                            newelf = w;break;
                        }
                    }
                    else if (to_check == 3)
                    {
                        if (!(cur_map[se] || cur_map[e] || cur_map[ne]))
                        {
                            newelf = e;break;
                        }
                    }
                }
            }
            if (newelf != elf)
                elves_moving++;

            // Store pointer to elf at new position
            if (new_map.count(newelf) == 0)
            {
                new_map.insert({newelf, {}});
            }
            new_map[newelf].push_back(&elf);
        }

        for (auto const& elf: elves)
        {
            cur_map[elf] = 0;
        }

        // Update elves that moved to an empty position
        for (auto const &p : new_map)
        {
            if (p.second.size() == 1)
            {
                *(p.second[0]) = p.first;
            }
        }

        // Create new map
        for (auto const& elf: elves)
        {
            cur_map[elf] = 1;
        }

        //draw_map(elves);

        round++;
        if (round == 10) {
            long minx = INT64_MAX, miny = INT64_MAX, maxx = INT64_MIN, maxy = INT64_MIN;
            for (auto const& elf: elves)
            {
                minx = min(elf / WX, minx);
                miny = min(elf % WX, miny);
                maxx = max(elf / WX, maxx);
                maxy = max(elf % WX, maxy);
            }
            cout << "Part 1: " << (maxx - minx + 1) * (maxy - miny + 1) - elves.size() << endl;
        }
    }
    cout << "Part 2: " << round << endl;
}