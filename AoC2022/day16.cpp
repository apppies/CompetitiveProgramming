#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <set>
#include <map>
#include <queue>

#include <regex>

std::string ltrim(const std::string &s)
{
    return std::regex_replace(s, std::regex("^\\s+"), std::string(""));
}

std::string rtrim(const std::string &s)
{
    return std::regex_replace(s, std::regex("\\s+$"), std::string(""));
}

std::string trim(const std::string &s)
{
    return ltrim(rtrim(s));
}

using namespace std;

vector<string> ids;
vector<vector<int>> graph;
vector<int> flows;
vector<vector<int>> r_graph;
vector<int> r_flows;
vector<int> r_ids;

int get_key(string name)
{
    for (int i = 0; i < ids.size(); i++)
    {
        if (ids[i] == name)
        {
            return i;
        }
    }
    return -1;
}

int add_key(string name)
{
    ids.push_back(name);
    return ids.size() - 1;
}

int get_path(int i, int j)
{
    // return path length between i and j
    vector<int> visited(ids.size(), -1);
    queue<int> q;
    q.push(i);
    visited[i] = 0;
    while (!q.empty())
    {
        int n = q.front();
        q.pop();
        for (auto const &nei : graph[n])
        {
            if (visited[nei] == -1 || visited[nei] > visited[n] + 1)
            {
                q.push(nei);
                visited[nei] = visited[n] + 1;
            }
        }
    }
    return visited[j];
}

vector<string> split(const string &s, char c)
{
    string::size_type i = 0;
    string::size_type j = s.find(c);
    vector<string> v;

    while (j != string::npos)
    {
        v.push_back(s.substr(i, j - i));
        i = ++j;
        j = s.find(c, j);
    }
    if (j == string::npos)
        v.push_back(s.substr(i, s.length()));

    return v;
}

int mostpressure = 0;
vector<int> mostpressure2;

void solve(int cur_node, int transit, vector<int> remaining, int pressure_releasing, int pressure_released, int time)
{
    time--;
    transit--;
    pressure_released += pressure_releasing;

    if (time == 0)
    {
        if (pressure_released > mostpressure)
            mostpressure = pressure_released;
        return;
    }

    if (transit == 0 && r_flows[cur_node] > 0)
    {
        pressure_releasing += r_flows[cur_node];
    }

    if (transit == -1)
    {
        remaining.erase(std::remove(remaining.begin(), remaining.end(), cur_node), remaining.end());
        if (remaining.size() > 0)
        {
            for (auto const &next : remaining)
            {
                solve(next, r_graph[cur_node][next], remaining, pressure_releasing, pressure_released, time);
            }
        }
        else
        {
            solve(cur_node, transit, remaining, pressure_releasing, pressure_released, time);
        }
    }
    else
    {
        solve(cur_node, transit, remaining, pressure_releasing, pressure_released, time);
    }
}

void solve2(int cur_node1, int cur_node2, int transit1, int transit2, const vector<int> &remaining, int pressure_releasing, int pressure_released, int time)
{

    time--;
    transit1--;
    transit2--;
    pressure_released += pressure_releasing;

    if (pressure_released >= mostpressure2[time])
    {
        mostpressure2[time] = pressure_released;
    }

    if (time == 0)
    {
        return;
    }

    if (transit1 == 0 && r_flows[cur_node1] > 0)
    {
        // open valve
        pressure_releasing += r_flows[cur_node1];
    }
    if (transit2 == 0 && r_flows[cur_node2] > 0)
    {
        // open valve
        pressure_releasing += r_flows[cur_node2];
    }

    if (remaining.size() > 0)
    {
        if (transit1 == -1 && transit2 == -1)
        {
            for (auto const &next1 : remaining)
            {
                for (auto const &next2 : remaining)
                {
                    if (next1 != next2)
                    {
                        vector<int> new_remaining;
                        for (int i = 0; i < remaining.size(); i++)
                        {
                            if (remaining[i] != next1 && remaining[i] != next2)
                                new_remaining.push_back(remaining[i]);
                        }

                        solve2(next1, next2, r_graph[cur_node1][next1], r_graph[cur_node2][next2], new_remaining, pressure_releasing, pressure_released, time);
                    }
                }
            }
        }
        else if (transit1 == -1 && transit2 != -1)
        {
            for (auto const &next1 : remaining)
            {
                if (next1 != cur_node2)
                {
                    vector<int> new_remaining;
                    for (int i = 0; i < remaining.size(); i++)
                    {
                        if (remaining[i] != next1)
                            new_remaining.push_back(remaining[i]);
                    }
                    solve2(next1, cur_node2, r_graph[cur_node1][next1], transit2, new_remaining, pressure_releasing, pressure_released, time);
                }
            }
        }
        else if (transit1 != -1 && transit2 == -1)
        {
            for (auto const &next2 : remaining)
            {
                if (next2 != cur_node1)
                {
                    vector<int> new_remaining;
                    for (int i = 0; i < remaining.size(); i++)
                    {
                        if (remaining[i] != next2)
                            new_remaining.push_back(remaining[i]);
                    }
                    solve2(cur_node1, next2, transit1, r_graph[cur_node2][next2], new_remaining, pressure_releasing, pressure_released, time);
                }
            }
        }
        else
        {
            solve2(cur_node1, cur_node2, transit1, transit2, remaining, pressure_releasing, pressure_released, time);
        }
    }
    else
    {

        solve2(cur_node1, cur_node2, transit1, transit2, remaining, pressure_releasing, pressure_released, time);
    }
}

int main()
{
    ifstream file("day16.txt");
    vector<string> lines;
    while (file.good())
    {
        string line;
        getline(file, line);
        if (line.length() > 0)
            lines.push_back(line);
    }
    file.close();

    for (auto const &line : lines)
    {
        auto src = line.substr(6, 2);
        if (get_key(src) == -1)
            add_key(src);

        auto dests = split(line.substr(line.find("to valve") + 9), ',');
        for (auto &dst : dests)
        {
            string d = trim(dst);
            if (get_key(d) == -1)
                add_key(d);
        }
    }

    graph = vector<vector<int>>(ids.size());
    flows = vector<int>(ids.size());

    int AA = get_key("AA");
    int r_AA = -1;

    int nonzeroflows = 0;

    for (auto const &line : lines)
    {
        auto src = get_key(line.substr(6, 2));
        auto dests = split(line.substr(line.find("to valve") + 9), ',');
        for (auto &dst : dests)
        {
            string d = trim(dst);
            graph[src].push_back(get_key(d));
        }
        int floweq = line.find('=');
        int flowend = line.find(';');
        auto flow = line.substr(floweq + 1, flowend - floweq - 1);
        flows[src] = stoi(flow);
        if (flows[src] > 0 || src == AA)
        {
            nonzeroflows++;
            r_ids.push_back(src);
            r_flows.push_back(flows[src]);
            if (src == AA)
            {
                r_AA = r_ids.size() - 1;
            }
        }
    }

    r_graph = vector<vector<int>>(nonzeroflows);
    for (int i = 0; i < nonzeroflows; i++)
    {
        for (int j = 0; j < nonzeroflows; j++)
        {
            int l = 0;
            if (i != j)
            {
                l = get_path(r_ids[i], r_ids[j]);
            }
            r_graph[i].push_back(l);
        }
    }

    vector<int> remaining;
    for (int i = 0; i < nonzeroflows; i++)
    {
        remaining.push_back(i);
    }

    solve(r_AA,0, remaining, 0, 0, 30);

    cout << "Part 1: " << mostpressure << endl;

    remaining.clear();
    for (int i = 0; i < nonzeroflows; i++)
    {
        if (i != r_AA)
            remaining.push_back(i);
    }

    for (int i = 0; i < 26; i++)
    {
        mostpressure2.push_back(0);
    }
    

    solve2(r_AA, r_AA, 0, 0, remaining, 0, 0, 26);
    cout << "Part 2: " << mostpressure2[0] << endl;
}