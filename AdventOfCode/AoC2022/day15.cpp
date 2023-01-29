#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <algorithm>

using namespace std;

class sensor{
public:
    int manhattanrange = -1;
    int x;
    int y;
    int bx;
    int by;

    sensor(int _x, int _y, int _bx, int _by){
        x = _x; y = _y; bx = _bx; by = _by;
        manhattanrange = (abs(x-bx) + abs(y-by));
    }

    bool get_range(int row, int& startx, int &endx)
    {
        int distrow = abs(row - y);
        if (distrow > manhattanrange) {
            return false;
        }

        startx = x - (manhattanrange - distrow);
        endx = x + (manhattanrange - distrow);
        return true;
    }
};

vector<pair<int,int>> get_ranges(int y, vector<sensor> &sensors){
    vector<pair<int,int>> ranges;

    for(auto& ts: sensors){
        int startx = 0; int endx = 0;
        if (ts.get_range(y, startx, endx)){
            ranges.push_back({startx, endx});
        }
    }

    sort(ranges.begin(), ranges.end());

    vector<pair<int,int>> reducedranges;

    int s1 = ranges[0].first, e1 = ranges[0].second;
    for (int i = 1; i < ranges.size(); i++)
    {
        if (ranges[i].first <= e1) {
            e1 = max(e1, ranges[i].second);
        } else {
            reducedranges.push_back({s1,e1});
            s1 = ranges[i].first;
            e1 = ranges[i].second;
        }
    }
    reducedranges.push_back({s1,e1});

    return reducedranges;
}

int main()
{
    ifstream file("day15.txt");
    vector<string> lines;
    while (file.good())
    {
        string line;
        getline(file, line);
        if (line.length() > 0)
            lines.push_back(line);
    }
    file.close();


    vector<sensor> sensors;
    for(auto const& line: lines){
        int sx,sy,bx,by;
        sscanf(line.c_str(), "Sensor at x=%d, y=%d: closest beacon is at x=%d, y=%d", &sx, &sy, &bx, &by);

        sensor s(sx,sy,bx,by);
        sensors.push_back(s);
    }

    int part1y = 2000000;
    auto part1_ranges = get_ranges(part1y, sensors);
    
    int count = 0;
    for (auto const& r: part1_ranges)
    {
        count += (r.second - r.first);
    }
        
    cout << "Part 1: " << count << endl;
    
    vector<int> rowsopenspots;
    int maxi = 4000000;

    for (int y = 0; y <= maxi; y++)
    {
        auto r = get_ranges(y, sensors);
        if (r.size() >= 1) {
            if (r[0].first > 0 || r[0].second < maxi){
                rowsopenspots.push_back(y);
            }
        }
    }

    long freey = rowsopenspots[0];
    long freex = 0;
    for(auto const& row: rowsopenspots){
        auto r = get_ranges(row, sensors);
        if (r.size() > 1)
            freex = r[0].second + 1;
        else if (r[0].first > 0)
            freex = 0;
        else 
            freex = maxi;
    }
    long freq = freex * maxi + freey;
    cout << "Part 2: " << freq << endl;

}

