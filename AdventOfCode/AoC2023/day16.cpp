#include <string>
#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include <cmath>
#include <algorithm>
#include <unordered_map>
#include <queue>
#include <bitset>

enum State
{
    NONE = 0,
    UP = 1,
    DOWN = 2,
    LEFT = 4,
    RIGHT = 8
};


long solve(std::vector<std::string> map, std::pair<std::pair<int, int>, State> first) {


    int w = map[0].length();
    int h = map.size();
    std::vector<std::vector<int>> visited(map.size(), std::vector<int>(map[0].size(), State::NONE));

    std::queue<std::pair<std::pair<int, int>, State>> front;
    front.push(first);

    while (front.size() > 0)
    {
        auto node = front.front();
        front.pop();
        if (node.first.first < 0 || node.first.first >= h || node.first.second < 0 || node.first.second >= w)
        {
            continue;
        }
        if ((visited[node.first.first][node.first.second] & node.second) != 0)
        {
            // Already been here
            continue;
        }
        visited[node.first.first][node.first.second] |= node.second;

        auto field = map[node.first.first][node.first.second];
        int newdir = State::NONE;
        if (field == '.')
        {
            if (node.second == UP)
                newdir = State::UP;
            else if (node.second == DOWN)
                newdir = State::DOWN;
            else if (node.second == LEFT)
                newdir = State::LEFT;
            else if (node.second == RIGHT)
                newdir = State::RIGHT;
        }
        else if (field == '/')
        {
            if (node.second == UP)
                newdir = State::RIGHT;
            else if (node.second == DOWN)
                newdir = State::LEFT;
            else if (node.second == LEFT)
                newdir = State::DOWN;
            else if (node.second == RIGHT)
                newdir = State::UP;
        }
        else if (field == '\\')
        {
            if (node.second == UP)
                newdir = State::LEFT;
            else if (node.second == DOWN)
                newdir = State::RIGHT;
            else if (node.second == LEFT)
                newdir = State::UP;
            else if (node.second == RIGHT)
                newdir = State::DOWN;
        }
        else if (field == '|')
        {
            if (node.second == UP)
                newdir = State::UP;
            else if (node.second == DOWN)
                newdir = State::DOWN;
            else if (node.second == LEFT)
                newdir = State::UP | State::DOWN;
            else if (node.second == RIGHT)
                newdir = State::UP | State::DOWN;
        }
        else if (field == '-')
        {
            if (node.second == UP)
                newdir = State::LEFT | State::RIGHT;
            else if (node.second == DOWN)
                newdir = State::LEFT | State::RIGHT;
            else if (node.second == LEFT)
                newdir = State::LEFT;
            else if (node.second == RIGHT)
                newdir = State::RIGHT;
        }
        if (newdir & State::UP)
            front.push({{node.first.first - 1, node.first.second}, State::UP});
        if (newdir & State::DOWN)
            front.push({{node.first.first + 1, node.first.second}, State::DOWN});
        if (newdir & State::LEFT)
            front.push({{node.first.first, node.first.second - 1}, State::LEFT});
        if (newdir & State::RIGHT)
            front.push({{node.first.first, node.first.second + 1}, State::RIGHT});
    }

    long energized = 0;
    for (size_t i = 0; i < h; i++)
    {
        for (size_t j = 0; j < w; j++)
        {
            if (visited[i][j] != 0)
                energized++;

            // if (map[i][j] != '.')
            //     std::cout << map[i][j];
            // else if (visited[i][j] == UP)
            //     std::cout << '^';
            // else if (visited[i][j] == DOWN)
            //     std::cout << 'v';
            // else if (visited[i][j] == LEFT)
            //     std::cout << '<';
            // else if (visited[i][j] == RIGHT)
            //     std::cout << '>';
            // else if (visited[i][j] == 0)
            //     std::cout << '.';
            // else
            //     std::cout << std::bitset<32>(visited[i][j]).count();
        }

        // std::cout << std::endl;
    }
    return energized;
}

void A()
{
    std::ifstream file("day16.txt");
    std::string line;
    std::vector<std::string> map;
    while (std::getline(file, line))
    {
        map.push_back(line);
    }
    auto answer = solve(map, {{0,0}, RIGHT});
    std::cout << "1: " << answer << std::endl;
}

void B()
{
    std::ifstream file("day16.txt");
    std::string line;
    std::vector<std::string> map;
    while (std::getline(file, line))
    {
        map.push_back(line);
    }

    long max = 0;
    for (size_t i = 0; i < map.size(); i++)
    {
        auto answer = solve(map, {{i,0}, RIGHT});
        if (answer > max) max = answer;
        answer = solve(map, {{i, map[0].length() - 1}, LEFT});
        if (answer > max) max = answer;
    }
    for (size_t i = 0; i < map[0].size(); i++)
    {
        auto answer = solve(map, {{0,i}, DOWN});
        if (answer > max) max = answer;
        answer = solve(map, {{map.size() - 1, i}, UP});
        if (answer > max) max = answer;
    }
    std::cout << "2: " << max << std::endl;
    

}



int main()
{
    A();
    B();
}