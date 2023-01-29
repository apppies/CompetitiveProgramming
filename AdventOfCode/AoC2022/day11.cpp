#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <cstring>
#include <queue>
#include <functional>

using namespace std;

// Copied from O'Reilly. Modified to return input string when delim is not found
void split(const string &s, char c,
           vector<string> &v)
{
    string::size_type i = 0;
    string::size_type j = s.find(c);

    while (j != string::npos)
    {
        v.push_back(s.substr(i, j - i));
        i = ++j;
        j = s.find(c, j);
    }
    if (j == string::npos)
        v.push_back(s.substr(i, s.length()));
}

struct monkey
{
    long itemsinspected = 0;
    function<long(long)> operation;
    // function<bool(long)> test;
    int testdivider;
    deque<long> items;
    int truemonkey;
    int falsemonkey;

    void inspect(vector<monkey> &monkeys, int worrydivider, int common_denom)
    {
        while (!items.empty())
        {
            long item = items.front();
            items.pop_front();

            long newval = operation(item);
            itemsinspected++;
            newval /= worrydivider;
            newval %= common_denom;

            // if (test(newval))
            if (newval % testdivider == 0)
            {
                monkeys[truemonkey].items.push_back(newval);
            }
            else
            {
                monkeys[falsemonkey].items.push_back(newval);
            }
        }
    }
};

long calculate_monkey_business(vector<monkey> &monkeys)
{
    long max1 = monkeys[0].itemsinspected;
    long max2 = monkeys[1].itemsinspected;
    for (long m = 2; m < monkeys.size(); m++)
    {
        if (monkeys[m].itemsinspected > max1)
        {
            if (max1 > max2)
            {
                max2 = max1;
            }
            max1 = monkeys[m].itemsinspected;
        }
        else if (monkeys[m].itemsinspected > max2)
        {
            max2 = monkeys[m].itemsinspected;
        }
    }
    return max1 * max2;
}

void load_monkeys(string const filename, vector<monkey> &monkeys, long &common_denom)
{
    ifstream file(filename);
    vector<string> lines;
    monkeys.clear();
    common_denom = 1;
    while (file.good())
    {
        string line;
        getline(file, line);

        if (line.substr(0, 6) == "Monkey")
        {
            monkey m;
            getline(file, line);

            vector<string> items;
            split(line.substr(strlen("  Starting items: ")), ',', items);
            for (auto item : items)
            {
                m.items.push_back(stol(item));
            }

            getline(file, line);
            vector<string> operations;
            split(line.substr(strlen("  Operation: new = ")), ' ', operations);
            if (operations[1] == "+")
            {
                if (operations[2] == "old")
                    m.operation = [](long old) { return old + old; };
                else
                {
                    auto param2 = stoi(operations[2]);
                    m.operation = [param2](long old) { return old + param2; };
                }
            }
            else if (operations[1] == "*")
            {
                if (operations[2] == "old")
                    m.operation = [](long old) { return old * old; };
                else
                {
                    auto param2 = stol(operations[2]);
                    m.operation = [param2](long old) { return old * param2; };
                }
            }

            getline(file, line);
            long divider = stoi(line.substr(strlen("  Test: divisible by ")));
            common_denom *= divider;
            // m.test = [divider](long old) { return (old % divider) == 0; };
            m.testdivider = divider;

            getline(file, line);
            int truemonkey = stoi(line.substr(strlen("    If true: throw to monkey ")));
            m.truemonkey = truemonkey;

            getline(file, line);
            int falsemonkey = stoi(line.substr(strlen("    If false: throw to monkey ")));
            m.falsemonkey = falsemonkey;

            monkeys.push_back(m);
        }
    }
    file.close();
}

int main()
{
    vector<monkey> monkeys;
    long common_denom;
    string filename = "day11.txt";

    load_monkeys(filename, monkeys, common_denom);
    for (int i = 0; i < 20; i++)
    {
        for (int m = 0; m < monkeys.size(); m++)
        {
            monkeys[m].inspect(monkeys, 3, common_denom);
        }
    }

    cout << "Part 1: " << calculate_monkey_business(monkeys) << endl;

    load_monkeys(filename, monkeys, common_denom);
    for (int i = 0; i < 10000; i++)
    {
        for (int m = 0; m < monkeys.size(); m++)
        {
            monkeys[m].inspect(monkeys, 1, common_denom);
        }
    }

    cout << "Part 2: " << calculate_monkey_business(monkeys) << endl;
}