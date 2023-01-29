#include <string>
#include <cassert>

using namespace std;

class disjoint_set {

private:
    int ids[26];

public:
    disjoint_set() {
        for (uint8_t i = 0; i < 26; i++)
        {
            ids[i] = i;
        }
    }

    uint8_t get_id(uint8_t i) {
        if (ids[i] != i){
            ids[i] = get_id(ids[i]);
        }
        return ids[i];
    }

    void merge(uint8_t i1, uint8_t i2){
        uint8_t id1 = get_id(i1);
        uint8_t id2 = get_id(i2);

        if (id1 < id2) {
            ids[id2] = id1;
        } else {
            ids[id1] = id2;
        }
    }
};

class Solution {
public:
    string smallestEquivalentString(string s1, string s2, string baseStr) {
        disjoint_set set;

        for (int i = 0; i < s1.length(); i++)
        {
            set.merge(s1[i] - 'a', s2[i] - 'a');
        }
        
        for (size_t i = 0; i < baseStr.length(); i++)
        {
            baseStr[i] = set.get_id(baseStr[i]- 'a') + 'a';
        }
        
        return baseStr;
    }
};

int main(){
    Solution sol;
    assert(sol.smallestEquivalentString("parker", "morris", "parser") == "makkek");
    assert(sol.smallestEquivalentString("hello", "world", "hold") == "hdld");
    assert(sol.smallestEquivalentString("leetcode", "programs", "sourcecode") == "aauaaaaada");
}