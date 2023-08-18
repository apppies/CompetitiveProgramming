// https://leetcode.com/problems/course-schedule-iv/
#include <iostream>
#include <string>
#include <vector>
#include <unordered_set>

using namespace std;
class Solution {
    
// Simple approach, no clever grouping or what soever, only cache results.
// unordered_set can be replaced with a 128bit number
private:
    vector<unordered_set<int>> allrequisites;
    vector<bool> checked;

public:
    unordered_set<int> getPrerequisites(int courseId){
        if (!checked[courseId]) {
            // Never checked before, recursively get requisites from parents
            unordered_set<int> requisites;
            for (auto const r1: allrequisites[courseId])
            {
                auto newReq = getPrerequisites(r1);
                for (auto const r: newReq)
                {
                    requisites.insert(r);
                }                
            }
            for (auto r: requisites)
            {
                allrequisites[courseId].insert(r);
            }
            checked[courseId] = true;
        }
        return allrequisites[courseId];
    }

    vector<bool> checkIfPrerequisite(int numCourses, vector<vector<int>>& prerequisites, vector<vector<int>>& queries) {
        checked = vector<bool>(numCourses, false);
        allrequisites = vector<unordered_set<int>>(numCourses);
        vector<bool> answer;

        // Insert predefined
        for (auto const p: prerequisites)
        {
            allrequisites[p[0]].insert(p[1]);
        }

        // Fill all requisites
        for (size_t i = 0; i < numCourses; i++)
        {
            getPrerequisites(i);
        }
        
        // Return answers
        for (auto const q: queries){
            answer.push_back(allrequisites[q[0]].find(q[1]) != allrequisites[q[0]].end());
        }

        return answer;
    }
};


int main() {
    Solution s;
    vector<vector<int>> p;
    vector<vector<int>> q;
    vector<bool> answer;

    cout << endl << 1 << endl;
    p = {{1,0}};
    q = {{0,1},{1,0}};
    answer = s.checkIfPrerequisite(2, p, q);
    for (auto const b:answer){
        cout << b << ' ';
    }

    cout << endl << 2 << endl;
    p = {};
    q = {{0,1},{1,0}};
    answer = s.checkIfPrerequisite(2, p, q);
    for (auto const b:answer){
        cout << b << ' ';
    }
     
    cout << endl << 3 << endl;
    p = {{1,2},{1,0},{2,0}};
    q = {{1,0},{1,2}};
    answer = s.checkIfPrerequisite(3, p, q);
    for (auto const b:answer){
        cout << b << ' ';
    }
    cout << endl;
}