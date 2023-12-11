#include <string>
#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include <cmath>

bool is_part(char c)
{
    return c != '.' && (c < '0' || c > '9');
}

bool get_touches_part(int x, int y, std::vector<std::string> map)
{
    bool touches = false;
    if (x > 0)
    {
        touches |= is_part(map[y][x - 1]);

        if (y > 0)
        {
            touches |= is_part(map[y - 1][x - 1]);
        }
        if (y < map.size() - 1)
        {
            touches |= is_part(map[y + 1][x - 1]);
        }
    }
    if (x < map[0].length() - 1)
    {
        touches |= is_part(map[y][x + 1]);

        if (y > 0)
        {
            touches |= is_part(map[y - 1][x + 1]);
        }
        if (y < map.size() - 1)
        {
            touches |= is_part(map[y + 1][x + 1]);
        }
    }
    if (y > 0)
    {
        touches |= is_part(map[y - 1][x]);
    }
    if (y < map.size() - 1)
    {
        touches |= is_part(map[y + 1][x]);
    }
    return touches;
}

void part1()
{
    std::ifstream file("day3.txt");

    std::string line;
    long sum = 0;
    std::vector<std::string> map;

    while (std::getline(file, line))
    {
        map.push_back(line);
    }
    for (size_t j = 0; j < map.size(); j++)
    {
        bool touches_part = false;
        bool in_number = false;
        int current_number = 0;
        int decimal = 0;
        line = map[j];
        for (size_t i = 0; i < line.length(); i++)
        {

            if (line[i] >= '0' && line[i] <= '9')
            {
                current_number = current_number * 10 + (line[i] - '0');
                decimal++;
                in_number = true;
                touches_part |= get_touches_part(i, j, map);
            }
            else
            {
                if (in_number && touches_part)
                {
                    sum += current_number;
                }
                in_number = false;
                touches_part = false;
                current_number = 0;
                decimal = 0;
            }
        }

        // Number at end of line
        if (in_number && touches_part)
        {
            sum += current_number;
        }
        in_number = false;
        touches_part = false;
        current_number = 0;
        decimal = 0;
    }
    std::cout << "A: " << sum << std::endl;
}

bool is_number(int x, int y, std::vector<std::string> map)
{
    return map[y][x] >= '0' && map[y][x] <= '9';
}
bool is_number(char c)
{
    return c >= '0' && c <= '9';
}

int get_number(int x, int y, std::vector<std::string> map, int direction)
{

    int num = map[y][x] - '0';
    int i = 1;
    if (direction <= 0)
    {
        while (x - i >= 0 && is_number(x - i, y, map))
        {
            num = num + (map[y][x - i] - '0') * std::pow(10, i);
            i++;
        }
    }

    i = 1;
    if (direction >= 0)
    {
        while (x + i >= 0 && is_number(x + i, y, map))
        {
            num = num * 10 + (map[y][x + i] - '0');
            i++;
        }
    }
    return num;
}

void part2()
{
    std::ifstream file("day3.txt");

    std::string line;
    long sum = 0;
    std::vector<std::string> map;

    while (std::getline(file, line))
    {
        map.push_back(line);
    }

    for (int y = 0; y < map.size(); y++)
    {
        for (int x = 0; x < map[y].length(); x++)
        {

            if (map[y][x] == '*')
            {
                // Gear, find numbers next to it
                long gear_factor = 1;
                int gear_part_count = 0;

                // Left
                if (x > 0 && is_number(x - 1, y, map))
                {
                    gear_factor *= get_number(x - 1, y, map, -1);
                    gear_part_count++;
                }
                // Right
                if (x < map[y].length() - 1 && is_number(x + 1, y, map))
                {
                    gear_factor *= get_number(x + 1, y, map, +1);
                    gear_part_count++;
                }
                // Check top mid
                if (y > 0 && is_number(map[y - 1][x]))
                {
                    gear_factor *= get_number(x, y - 1, map, 0);
                    gear_part_count++;
                }
                else if (y > 0)
                {
                    if (x > 0 && is_number(x - 1, y - 1, map))
                    {
                        gear_factor *= get_number(x - 1, y - 1, map, -1);
                        gear_part_count++;
                    }
                    // Right
                    if (x < map[y - 1].length() - 1 && is_number(x + 1, y - 1, map))
                    {
                        gear_factor *= get_number(x + 1, y - 1, map, +1);
                        gear_part_count++;
                    }
                }

                // Check bottom mid
                if (y < map.size() - 1 && is_number(map[y + 1][x]))
                {
                    gear_factor *= get_number(x, y + 1, map, 0);
                    gear_part_count++;
                }
                else if (y < map.size() - 1)
                {
                    if (x > 0 && is_number(x - 1, y + 1, map))
                    {
                        gear_factor *= get_number(x - 1, y + 1, map, -1);
                        gear_part_count++;
                    }
                    // Right
                    if (x < map[y - 1].length() - 1 && is_number(x + 1, y + 1, map))
                    {
                        gear_factor *= get_number(x + 1, y + 1, map, +1);
                        gear_part_count++;
                    }
                }

                if (gear_part_count > 1)
                    sum += gear_factor;
            }
        }
    }
    std::cout << "B: " << sum << std::endl;
}

int main()
{
    part1();
    part2();
}