#include <iostream>
#include <vector>
#include <stack>
using namespace std;

vector<vector<int>> m_graph;
stack<int> m_stack;
vector<bool> m_instack;
vector<int> m_ids;
vector<int> m_lowlinks;
int m_counter = 0;
int scc_counter = 0;

void dfs(int p_node){
    m_counter++;

    m_ids[p_node] = m_counter;
    m_lowlinks[p_node] = m_counter;
    
    m_stack.push(p_node);
    m_instack[p_node] = true;

    for (size_t i = 0; i < m_graph[p_node].size(); i++)
    {
        if (m_ids[i] == -1) {
            dfs(i);
        }

        m_lowlinks[p_node] = min(m_lowlinks[p_node], m_lowlinks[i]);
    }

    if (m_ids[p_node] == m_lowlinks[p_node]){
        scc_counter++;
        while (true)
        {
            int idx = m_stack.top();
            m_stack.pop();
            m_instack[idx] = false;

            if (idx == p_node) // we are back at the start
                break;
        }

    }
    
}

int count_groups(){
    for (size_t i = 0; i < m_graph.size(); i++)
    {
        if (m_ids[i] == -1){
            dfs(i);
        }
    }
    
    return scc_counter;
}

int main ( ) {

    while (true) {

        

    }


}