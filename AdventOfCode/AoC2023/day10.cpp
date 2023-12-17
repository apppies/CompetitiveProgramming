#include <string>
#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include <cmath>
#include <algorithm>
#include <unordered_map>

void A()
{
    std::ifstream file("day10.txt");
    std::string line;
    std::vector<std::string> map;
    int s_x = -1;
    int s_y = -1;

    int line_index = 0;
    while (std::getline(file, line))
    {
        map.push_back(line);
        auto s_pos = line.find('S');
        if (s_pos != std::string::npos)
        {
            s_x = (int)s_pos;
            s_y = line_index;
        }
        line_index++;
    }

    std::vector<std::vector<int>> visited(map.size(), std::vector<int>(map[0].length(), 0));

    std::vector<std::pair<int, int>> cur_nodes;
    int w = map[0].length();
    int h = map.size();

    std::string parts = "-|LF7J";
    std::unordered_map<char, bool> s_options = {
        {'7', true},
        {'F', true},
        {'J', true},
        {'L', true},
        {'-', true},
        {'|', true},
    };
    // Find starting directions from S
    if (s_y > 0 && (map[s_y - 1][s_x] == '7' || map[s_y - 1][s_x] == 'F' || map[s_y - 1][s_x] == '|'))
    {
        cur_nodes.push_back({s_y - 1, s_x});
        s_options['7'] = false;
        s_options['F'] = false;
        s_options['-'] = false;
    }
    if (s_x > 0 && (map[s_y][s_x - 1] == 'L' || map[s_y][s_x - 1] == 'F' || map[s_y][s_x - 1] == '-'))
    {
        cur_nodes.push_back({s_y, s_x - 1});
        s_options['L'] = false;
        s_options['F'] = false;
        s_options['|'] = false;
    }
    if (s_y < h - 1 && (map[s_y + 1][s_x] == 'L' || map[s_y + 1][s_x] == 'J' || map[s_y + 1][s_x] == '|'))
    {
        cur_nodes.push_back({s_y + 1, s_x});
        s_options['L'] = false;
        s_options['J'] = false;
        s_options['-'] = false;
    }
    if (s_x < w - 1 && (map[s_y][s_x + 1] == 'J' || map[s_y][s_x + 1] == '7' || map[s_y][s_x + 1] == '-'))
    {
        cur_nodes.push_back({s_y, s_x + 1});
        s_options['J'] = false;
        s_options['7'] = false;
        s_options['|'] = false;
    }

    visited[s_y][s_x] = 1;

    // Set pipe type of S
    for (const auto &[c, v] : s_options)
    {
        if (v)
        {
            map[s_y][s_x] = c;
            break;
        }
    }

    int steps = 0;
    int node_count = 1;
    while (cur_nodes.size() > 0)
    {
        steps++;
        for (auto const &node : cur_nodes)
        {
            // Visit both
            visited[node.first][node.second] = steps;
            node_count++;
        }
        std::vector<std::pair<int, int>> new_nodes;
        for (auto const &node : cur_nodes)
        {
            // Move to next unvisited node
            switch (map[node.first][node.second])
            {
            case '-':
                if (node.second > 0 && visited[node.first][node.second - 1] == 0) // moving east
                    new_nodes.push_back({node.first, node.second - 1});
                if (node.second < w - 1 && visited[node.first][node.second + 1] == 0) // moving west
                    new_nodes.push_back({node.first, node.second + 1});
                break;
            case '|':
                if (node.first > 0 && visited[node.first - 1][node.second] == 0) // moving north
                    new_nodes.push_back({node.first - 1, node.second});
                if (node.first < h - 1 && visited[node.first + 1][node.second] == 0) // moving south
                    new_nodes.push_back({node.first + 1, node.second});
                break;
            case 'L':
                if (node.first > 0 && visited[node.first - 1][node.second] == 0) // moving north
                    new_nodes.push_back({node.first - 1, node.second});
                if (node.second < w - 1 && visited[node.first][node.second + 1] == 0) // moving west
                    new_nodes.push_back({node.first, node.second + 1});
                break;
            case 'J':
                if (node.first > 0 && visited[node.first - 1][node.second] == 0) // moving north
                    new_nodes.push_back({node.first - 1, node.second});
                if (node.second > 0 && visited[node.first][node.second - 1] == 0) // moving east
                    new_nodes.push_back({node.first, node.second - 1});
                break;
            case '7':
                if (node.first < h - 1 && visited[node.first + 1][node.second] == 0) // moving south
                    new_nodes.push_back({node.first + 1, node.second});
                if (node.second > 0 && visited[node.first][node.second - 1] == 0) // moving east
                    new_nodes.push_back({node.first, node.second - 1});
                break;
            case 'F':
                if (node.first < h - 1 && visited[node.first + 1][node.second] == 0) // moving south
                    new_nodes.push_back({node.first + 1, node.second});
                if (node.second < w - 1 && visited[node.first][node.second + 1] == 0) // moving west
                    new_nodes.push_back({node.first, node.second + 1});
                break;

            default:
                break;
            }
        }
        std::swap(cur_nodes, new_nodes);
    }
    std::cout << "1: " << steps << std::endl;

    // double map size, it opens path ways to fill and add outside border
    // reduce size later

    std::vector<std::vector<int>> flood_map(map.size() * 2 + 2, std::vector<int>(map[0].length() * 2 + 2, 0));
    // Copy current loop
    for (size_t i = 0; i < visited.size(); i++)
    {
        for (size_t j = 0; j < visited[0].size(); j++)
        {
            flood_map[1 + i * 2][1 + j * 2] = visited[i][j];
            switch (map[i][j])
            {
            case '-':
                flood_map[1 + i * 2][1 + j * 2 + 1] = visited[i][j];
                break;
            case '|':
                flood_map[1 + i * 2 + 1][1 + j * 2] = visited[i][j];
                break;
            case '7':
                flood_map[1 + i * 2 + 1][1 + j * 2] = visited[i][j];
                break;
            case 'F':
                flood_map[1 + i * 2 + 1][1 + j * 2] = visited[i][j];
                flood_map[1 + i * 2][1 + j * 2 + 1] = visited[i][j];
                break;
            case 'J':
                break;
            case 'L':
                flood_map[1 + i * 2][1 + j * 2 + 1] = visited[i][j];
                break;

            default:
                break;
            }
        }
    }

    // bfs
    std::vector<std::pair<int, int>> front;
    front.push_back({0, 0}); // Start front at top corner
    while (front.size() > 0)
    {
        std::vector<std::pair<int, int>> new_front;
        for (auto const &node : front)
        {
            int n1 = node.first, n2 = node.second;
            if (n1 > 0 && flood_map[n1 - 1][n2] == 0)
            {
                flood_map[n1 - 1][n2] = -1;
                new_front.push_back({n1 - 1, n2});
            }
            if (n1 < flood_map.size() - 1 && flood_map[n1 + 1][n2] == 0)
            {
                flood_map[n1 + 1][n2] = -1;
                new_front.push_back({n1 + 1, n2});
            }
            if (n2 > 0 && flood_map[n1][n2 - 1] == 0)
            {
                flood_map[n1][n2 - 1] = -1;
                new_front.push_back({n1, n2 - 1});
            }
            if (n2 < flood_map[0].size() - 1 && flood_map[n1][n2 + 1] == 0)
            {
                flood_map[n1][n2 + 1] = -1;
                new_front.push_back({n1, n2 + 1});
            }
        }
        std::swap(front, new_front);
    }

    // Count inner ones in reduced size
    int inner = 0;
    for (size_t i = 1; i < flood_map.size() - 1; i += 2)
    {
        for (size_t j = 1; j < flood_map[0].size() - 1; j += 2)
        {
            if (flood_map[i][j] == 0)
            {
                inner++;
            }
        }
    }

    std::cout << "2: " << inner << std::endl;
}

int main()
{
    A();
}