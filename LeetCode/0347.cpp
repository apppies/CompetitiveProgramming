
// Not correct yet

#include <string>
#include <list>
#include <iterator>
#include <cassert>
#include <vector>
#include <unordered_map>
using namespace std;

class Solution
{
private:
public:
    vector<int> topKFrequent(vector<int>& nums, int k) {
        unordered_map<int, int> counts;
        
        vector<int> output;
        for (int i = 0; i < nums.size(); i++){
            int num = nums[i];
            counts[num]++;
        }
        vector<vector<int>> buckets(nums.size() + 1);
        for (auto const & pair: counts) {
            int num = pair.first;
            int count = pair.second;
            buckets[count].push_back(num);
        }
        for (int i = buckets.size() - 1; i >= 0; i--)
        {
            for (int j = 0; j < buckets[i].size(); j++){
                output.push_back(buckets[i][j]);
                if (output.size() == k)
                    break;
            }
            if (output.size() == k)
                    break;
        }
        return output;
    }

    vector<int> topKFrequentSlow(vector<int>& nums, int k) {
        vector<int> counts (2 * 10E4 + 1 + 1);
        vector<vector<int>> buckets(nums.size() + 1);
        vector<int> output;
        for (int i = 0; i < nums.size(); i++){
            int num = nums[i];
            counts[num + 10E4]++;
        }
        for (int i = 0; i < counts.size(); i++){
            int num = i - 10E4;
            int count = counts[i];
            buckets[count].push_back(num);
        }
        for (int i = buckets.size() - 1; i >= 0; i--)
        {
            for (int j = 0; j < buckets[i].size(); j++){
                output.push_back(buckets[i][j]);
                if (output.size() == k)
                    break;
            }
            if (output.size() == k)
                    break;
        }
        return output;
    }
};

int main()
{
    Solution sol;
    vector<int> input = {1,1,1,2,2,3};
    sol.topKFrequent(input,2);
    input = {-1,-1};
    sol.topKFrequent(input,12);
}
