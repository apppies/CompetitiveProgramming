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

enum Direction
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
};

struct node_state
{
    int X;
    int Y;
    Direction dir = UP;
    int steps = 0;
    long cost = 0;
};

void A()
{
    std::ifstream file("day17.txt");
    std::string line;
    std::vector<std::string> map;
    while (std::getline(file, line))
    {
        map.push_back(line);
    }
    int W = map[0].length();
    int H = map.size();

    // matrix with at each location vector of 3 holding min costs of 1,2 ,3 steps
    std::vector<std::vector<std::vector<std::vector<long>>>> map_state(H, std::vector<std::vector<std::vector<long>>>(W, std::vector<std::vector<long>>(3, std::vector<long>(5,__LONG_MAX__))));

    std::queue<node_state> front;
    front.push({0, 0, NONE, 1});

    bool found = false;
    while (!found)
    {
        auto node = front.front();
        front.pop();

        // Check if visited before on a cheaper route, trace per number of steps
        auto new_cost = node.cost + (map[node.Y][node.X] - '0');
        if (node.dir == NONE)
            new_cost = 0;

        bool cheap = true;
        for (size_t i = 0; i < node.steps; i++)
        {
            if (map_state[node.Y][node.X][i][node.dir] <= new_cost) {
                cheap = false;
            }
        }
        
            
        if (cheap)//map_state[node.Y][node.X][node.steps - 1][node.dir] > new_cost)
        {
            // We found a cheaper route to here for the given amount of steps
            map_state[node.Y][node.X][node.steps - 1][node.dir] = new_cost;

            // Add possible directions, cant step outside grid, cant reverse, no more than 3 steps in one direction
            if (node.X > 0 && node.dir != RIGHT && (node.dir != LEFT || node.steps < 3))
            {
                front.push({node.X - 1, node.Y, LEFT, node.dir == LEFT ? node.steps + 1 : 1, new_cost});
            }
            if (node.X < W - 1 && node.dir != LEFT && (node.dir != RIGHT || node.steps < 3))
            {
                front.push({node.X + 1, node.Y, RIGHT, node.dir == RIGHT ? node.steps + 1 : 1, new_cost});
            }
            if (node.Y > 0 && node.dir != DOWN && (node.dir != UP || node.steps < 3))
            {
                front.push({node.X, node.Y - 1, UP, node.dir == UP ? node.steps + 1 : 1, new_cost});
            }
            if (node.Y < H - 1 && node.dir != UP && (node.dir != DOWN || node.steps < 3))
            {
                front.push({node.X, node.Y + 1, DOWN, node.dir == DOWN ? node.steps + 1 : 1, new_cost});
            }
        }
        found = front.size() == 0;
    }

    long min = __LONG_MAX__;
    for (size_t i = 0; i < 3; i++)
    {
        for (size_t j = 0; j < 5; j++)
        {
            min = std::min(map_state[H - 1][W - 1][i][j], min);
        }
        
    }
    
    std::cout << "1: " << min << std::endl;
}

void B()
{
    std::ifstream file("day17.txt");
    std::string line;
    std::vector<std::string> map;
    while (std::getline(file, line))
    {
        map.push_back(line);
    }
    int W = map[0].length();
    int H = map.size();

    // matrix with at each location vector of 3 holding min costs of 1,2 ,3 steps
    std::vector<std::vector<std::vector<std::vector<long>>>> map_state(H, std::vector<std::vector<std::vector<long>>>(W, std::vector<std::vector<long>>(10, std::vector<long>(5,__LONG_MAX__))));

    std::queue<node_state> front;
    front.push({0, 0, NONE, 1});

    bool found = false;
    while (!found)
    {
        auto node = front.front();
        front.pop();

        // Check if visited before on a cheaper route, trace per number of steps
        auto new_cost = node.cost + (map[node.Y][node.X] - '0');
        if (node.dir == NONE)
            new_cost = 0;

        bool cheap = true;
        for (size_t i = 3; i < node.steps; i++)
        {
            if (map_state[node.Y][node.X][i][node.dir] <= new_cost) {
                cheap = false;
            }
        }
        
            
        if (cheap)//map_state[node.Y][node.X][node.steps - 1][node.dir] > new_cost)
        {
            // We found a cheaper route to here for the given amount of steps
            map_state[node.Y][node.X][node.steps - 1][node.dir] = new_cost;

            // Add possible directions, cant step outside grid, cant reverse, no more than 3 steps in one direction
            if (node.X > 0 && node.dir != RIGHT && ((node.dir != LEFT && node.steps >= 4) || (node.dir == LEFT && node.steps < 10) || node.dir == NONE))
            {
                front.push({node.X - 1, node.Y, LEFT, node.dir == LEFT ? node.steps + 1 : 1, new_cost});
            }
            if (node.X < W - 1 && node.dir != LEFT && ((node.dir != RIGHT && node.steps >= 4) || (node.dir == RIGHT && node.steps < 10) || node.dir == NONE))
            {
                front.push({node.X + 1, node.Y, RIGHT, node.dir == RIGHT ? node.steps + 1 : 1, new_cost});
            }
            if (node.Y > 0 && node.dir != DOWN && ((node.dir != UP && node.steps >= 4) || (node.dir == UP && node.steps < 10) || node.dir == NONE))
            {
                front.push({node.X, node.Y - 1, UP, node.dir == UP ? node.steps + 1 : 1, new_cost});
            }
            if (node.Y < H - 1 && node.dir != UP && ((node.dir != DOWN && node.steps >= 4) || (node.dir == DOWN && node.steps < 10) || node.dir == NONE))
            {
                front.push({node.X, node.Y + 1, DOWN, node.dir == DOWN ? node.steps + 1 : 1, new_cost});
            }
        }
        found = front.size() == 0;
    }

    long min = __LONG_MAX__;
    for (size_t i = 3; i < 10; i++)
    {
        for (size_t j = 0; j < 5; j++)
        {
            min = std::min(map_state[H - 1][W - 1][i][j], min);
        }
        
    }
    
    std::cout << "1: " << min << std::endl;
}

int main()
{
    B();
}