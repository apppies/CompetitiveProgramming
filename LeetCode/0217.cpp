// https://leetcode.com/problems/contains-duplicate/
#include <unordered_set>
#include <vector>

class Solution {
public:
    bool containsDuplicate(std::vector<int>& nums) {
        std::unordered_set<int> uniques;
        for( auto const & num: nums) {
            if (!uniques.insert(num).second) {
                return true;
            }
        }
        return false;
    }
};