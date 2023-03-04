
// Not correct yet

#include <string>
#include <list>
#include <iterator>
#include <cassert>
#include <vector>
#include <unordered_map>
#include <iostream>
using namespace std;

class Solution
{
private:
public:
    string frequencySort(string s) {
        unordered_map<int, int> counts;
        for (int i = 0; i < s.size(); i++)
        {
            counts[s[i]]++;
        }

        vector<vector<int>> buckets(s.size() + 1);
        for (auto const & pair: counts) {
            buckets[pair.first].push_back(pair.second);
        }
        string output = "";
        for (int i = buckets.size() - 1; i >= 0; i--)
        {
            for (int j = 0; j < buckets[i].size(); j++)
            {
                output += string(i, buckets[i][j]);
            }
        }
        return output;        
    }
};

int main()
{
    Solution sol;
    cout << sol.frequencySort("tree") << endl;
    cout << sol.frequencySort("cccaaa") << endl;

}
