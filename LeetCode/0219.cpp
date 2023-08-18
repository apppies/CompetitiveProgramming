// https://leetcode.com/problems/contains-duplicate-ii/
#include <unordered_map>
#include <vector>

class Solution {
public:
    bool containsNearbyDuplicate(std::vector<int>& nums, int k) {
        std::unordered_map<int,int> map;
        for (int i=0; i<nums.size(); i++) {
            if (map.count(nums[i])) {
                if (i - map[nums[i]] <= k)
                    return true;
            } 
            map[nums[i]] = i;
        }
        return false;
    }
};