#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <unordered_set>
#include <unordered_map>
#include <map>

using namespace std;

struct pair_hash {
    inline std::size_t operator()(const std::pair<int,int> & v) const {
        return v.first*31+v.second;
    }
};

void draw_map(vector<pair<int,int>> &elves) {
    int minx = INT32_MAX, miny = INT32_MAX, maxx = INT32_MIN, maxy = INT32_MIN;
    unordered_set<pair<int,int>, pair_hash> cur_map;
    for (auto const& elf: elves)
    {
        minx = min(elf.first, minx);
        miny = min(elf.second, miny);
        maxx = max(elf.first, maxx);
        maxy = max(elf.second, maxy);
        cur_map.insert(elf);
    }

    for (int y = miny; y <= maxy; y++){
        for (int x = minx; x <= maxx; x++)
        {
            if (cur_map.count({x,y})) cout << '#';
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

    vector<pair<int, int>> elves;
    unordered_set<pair<int, int>, pair_hash> cur_map;

    for (int y = 0; y < lines.size(); y++)
    {
        for (int x = 0; x < lines[y].length(); x++)
        {
            if (lines[y][x] == '#')
            {
                elves.push_back({x, y});
                cur_map.insert({x,y});
            }
        }
    }

    long round = 0;
    long elves_moving = 1;
    while (elves_moving > 0)
    {
        unordered_map<pair<int, int>, vector<pair<int, int> *>, pair_hash> new_map;
        elves_moving = 0;

        for (auto &elf : elves)
        {

            pair<int, int> ne = {elf.first + 1, elf.second - 1};
            pair<int, int> n = {elf.first, elf.second - 1};
            pair<int, int> nw = {elf.first - 1, elf.second - 1};
            pair<int, int> w = {elf.first - 1, elf.second};
            pair<int, int> sw = {elf.first - 1, elf.second + 1};
            pair<int, int> s = {elf.first, elf.second + 1};
            pair<int, int> se = {elf.first + 1, elf.second + 1};
            pair<int, int> e = {elf.first + 1, elf.second};

            pair<int, int> newelf = elf;

            if (cur_map.count(ne) || cur_map.count(n) || cur_map.count(nw) || cur_map.count(w) ||
                cur_map.count(sw) || cur_map.count(s) || cur_map.count(se) || cur_map.count(e))
            {
                for (int i = 0; i < 4; i++)
                {
                    int to_check = (round + i) % 4;
                    if (to_check == 0)
                    {
                        if (!(cur_map.count(ne) || cur_map.count(n) || cur_map.count(nw)))
                        {
                            newelf = n;
                            break;
                        }
                    }
                    else if (to_check == 1)
                    {
                        if (!(cur_map.count(sw) || cur_map.count(s) || cur_map.count(se)))
                        {
                            newelf = s;break;
                        }
                    }
                    else if (to_check == 2)
                    {
                        if (!(cur_map.count(sw) || cur_map.count(w) || cur_map.count(nw)))
                        {
                            newelf = w;break;
                        }
                    }
                    else if (to_check == 3)
                    {
                        if (!(cur_map.count(se) || cur_map.count(e) || cur_map.count(ne)))
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

        // Update elves that moved to an empty position
        for (auto const &p : new_map)
        {
            if (p.second.size() == 1)
            {
                p.second[0]->first = p.first.first;
                p.second[0]->second = p.first.second;
            }
        }

        // Create new map
        cur_map.clear();
        for (auto const& elf: elves)
        {
            cur_map.insert(elf);
        }

        //draw_map(elves);

        round++;
        if (round == 10) {
            int minx = INT32_MAX, miny = INT32_MAX, maxx = INT32_MIN, maxy = INT32_MIN;
            for (auto const& elf: elves)
            {
                minx = min(elf.first, minx);
                miny = min(elf.second, miny);
                maxx = max(elf.first, maxx);
                maxy = max(elf.second, maxy);
            }
            cout << "Part 1: " << (maxx - minx + 1) * (maxy - miny + 1) - elves.size() << endl;
        }
    }
    cout << "Part 2: " << round << endl;
}