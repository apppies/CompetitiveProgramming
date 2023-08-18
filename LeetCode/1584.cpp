// https://leetcode.com/problems/min-cost-to-connect-all-points/
#include <bits/stdc++.h>
#include <iostream>
#include <vector>

using namespace std;

class Solution {
private:
  int manhattanDistance(vector<int> &p1, vector<int> &p2) {
    return abs(p1[0] - p2[0]) + abs(p1[1] - p2[1]);
  }

public:
  static bool sortweights(const pair<int, vector<int>> &v1,
                          const pair<int, vector<int>> &v2) {
    return v1.first > v2.first;
  }

  int minCostConnectPoints(vector<vector<int>> &points) {

    vector<pair<int, vector<int>>> weights;
    for (int i = 0; i < points.size(); i++) {
      for (int j = i + 1; j < points.size(); j++) {
        int weight = manhattanDistance(points[i], points[j]);
        weights.push_back({weight, {i, j}});
      }
    }

    make_heap(weights.begin(), weights.end(), sortweights);

//    sort(weights.begin(), weights.end(), sortweights);

    vector<int> visited(points.size(), -1);
    vector<vector<int>> pointgroups;
    vector<int> pointsum;

    int counter = 0;
    int visitedcount = 0;
    int groupcount = 0;

    //for (int i = 0; i < weights.size(); i++) {
    while (!weights.empty()) {
        pop_heap(weights.begin(), weights.end(), sortweights);
        auto w = weights.back();
        weights.pop_back();
        int p1 = w.second[0];
        int p2 = w.second[1];
        int weight = w.first;

    //   int p1 = weights[i].second[0];
    //   int p2 = weights[i].second[1];
    //   int weight = weights[i].first;
      if (visited[p1] == -1 && visited[p2] == -1) {
        // new cluster
        visited[p1] = counter;
        visited[p2] = counter;
        pointgroups.push_back({p1, p2});
        pointsum.push_back(weight);
        counter++;
        visitedcount += 2;
        groupcount++;

      } else if (visited[p1] > -1 && visited[p2] > -1 &&
                 visited[p1] != visited[p2]) {
        // join two clusters
        int minPoint = min(visited[p1], visited[p2]);
        int maxPoint = max(visited[p1], visited[p2]);
        int lenMin = pointgroups[minPoint].size();
        int lenMax = pointgroups[maxPoint].size();
        if (lenMin < lenMax) {
          // Move all points from the minPoint group to maxPoint group
          for (int j = 0; j < pointgroups[minPoint].size(); j++) {
            pointgroups[maxPoint].push_back(pointgroups[minPoint][j]);
            visited[pointgroups[minPoint][j]] = maxPoint;
          }
          pointgroups[minPoint].clear();
          pointsum[maxPoint] += pointsum[minPoint] + weight;
          pointsum[minPoint] = 0;
        } else {
          for (int j = 0; j < pointgroups[maxPoint].size(); j++) {
            pointgroups[minPoint].push_back(pointgroups[maxPoint][j]);
            visited[pointgroups[maxPoint][j]] = minPoint;
          }
          pointgroups[maxPoint].clear();
          pointsum[minPoint] += pointsum[maxPoint] + weight;
          pointsum[maxPoint] = 0;
        }
        groupcount--;

      } else if (visited[p1] > -1 && visited[p2] == -1) {
        // add one to the other existing cluster
        visited[p2] = visited[p1];
        pointgroups[visited[p1]].push_back(p2);
        pointsum[visited[p1]] += weight;
        visitedcount += 1;
      } else if (visited[p2] > -1 && visited[p1] == -1) {
        // add one to the other existing cluster
        visited[p1] = visited[p2];
        pointgroups[visited[p2]].push_back(p1);
        pointsum[visited[p2]] += weight;
        visitedcount += 1;
      }
      if (visitedcount == points.size() && groupcount == 1) {
        break;
      }
    }

    for (int i = 0; i < pointsum.size(); i++) {
      if (pointsum[i] != 0)
        return pointsum[i];
    }
    return 0;
  }
};

int main() {
  Solution solution;

  vector<vector<int>> points;

//   points = {{0, 0}, {2, 2}, {3, 10}, {5, 2}, {7, 0}};
//   cout << solution.minCostConnectPoints(points) << endl;

//   points = {{3, 12}, {-2, 5}, {-4, 1}};
//   cout << solution.minCostConnectPoints(points) << endl;

  points = {{2, -3}, {-17, -8}, {13, 8}, {-17, -15}};
  cout << solution.minCostConnectPoints(points) << endl;

  points = {{-8,14},{16,-18},{-19,-13},{-18,19},{20,20},{13,-20},{-15,9},{-4,-8}};
    cout << solution.minCostConnectPoints(points) << endl;
}