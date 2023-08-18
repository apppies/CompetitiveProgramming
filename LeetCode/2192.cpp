// https://leetcode.com/problems/all-ancestors-of-a-node-in-a-directed-acyclic-graph/
#include <vector>
#include <set>

using namespace std;
class Solution
{
private:
    vector<bool> visited;
    vector<set<int>> parents;

    set<int> getAllParents(int n)
    {
        if (!visited[n])
        {
            visited[n] = true;
            set<int> allparents;
            for (auto const &parent : parents[n])
            {
                auto parent_parents = getAllParents(parent);
                allparents.insert(parent_parents.begin(), parent_parents.end());
            }
            parents[n].insert(allparents.begin(),allparents.end());
        }

        return parents[n];
    }

public:
    vector<vector<int>> getAncestors(int n, vector<vector<int>> &edges)
    {
        visited = vector<bool>(n, false);
        parents = vector<set<int>>(n);
        vector<vector<int>> answer;

        // Define direct parents
        for (auto const &edge : edges)
        {
            parents[edge[1]].insert(edge[0]);
        }

        // Get all parents
        for (int i = 0; i < n; i++)
        {
            getAllParents(i);
            answer.push_back(vector<int>(parents[i].begin(), parents[i].end()));
        }
        
        return answer;
    }
};

int main()
{
    Solution s;
    vector<vector<int>> in = {{0,3},{0,4},{1,3},{2,4},{2,7},{3,5},{3,6},{3,7},{4,6}};
    s.getAncestors(8, in);
}