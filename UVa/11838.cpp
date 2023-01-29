#include <iostream>
#include <vector>
#include <stack>

using namespace std;
using graph = vector<std::vector<int>>;

// Case for tarkans algorithm: find if there is a single critical connection. If more -> not all roads are reachable from everywhere

graph m_graph;
vector<int> m_nums;
vector<int> m_lows;
vector<bool> m_visited;
int counter = 0;
int scc_counter = 0;
stack<int> m_stack;
void dfs(int p_node){
    counter++;

    m_nums[p_node] = counter;
    m_lows[p_node] = counter;
    m_visited[p_node] = true;
    m_stack.push(p_node);

    auto& neighbours = m_graph[p_node];
    for (int neighbour : neighbours)
    {
        if (m_nums[neighbour] == -1)        {
            dfs(neighbour);
        }

        if (m_visited[neighbour])        {
            m_lows[p_node] = min(m_lows[p_node], m_lows[neighbour]);
        }
    }

    if (m_nums[p_node] == m_lows[p_node]) {
        scc_counter++;
        while (true) {
            int idx = m_stack.top();
            m_stack.pop();
            m_visited[idx] = false;

            if (idx == p_node) { 
                break;
            }
        }
    }
}

int main()
{
    while (true)
    {
        int n, m;
        cin >> n >> m;

        if (n == 0 & m == 0)
            break;

        m_graph = graph(n);
        for (size_t i = 0; i < m; i++)
        {
            int v, w, p;
            cin >> v >> w >> p;
            v--; w--;
            m_graph[v].push_back(w);
            if (p == 2)
                m_graph[w].push_back(v);
        }

        counter = 0;
        scc_counter = 0;
        m_nums = std::vector<int>(n, -1);
        m_lows = std::vector<int>(n, INT32_MAX);
        m_visited = std::vector<bool>(n, false);
        for (size_t i = 0; i < n; i++)
        {
            if(m_nums[i] == -1)
                dfs(i);
            if (scc_counter > 1)
                break;
        }
        
        if (scc_counter == 1)
            cout << 1 << endl;
        else
            cout << 0 << endl;
    }
}