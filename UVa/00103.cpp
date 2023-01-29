#include <algorithm>
#include <iostream>
#include <numeric>
#include <sstream>
#include <string>
#include <vector>

using namespace std;

/// @brief Compare two boxes with sorted measures
/// @param box1
/// @param box2
/// @return -1 if box1 fits in box2, +1 if box2 fits in box1, 0 if neither
int boxcompare(pair<int, vector<int>> box1, pair<int, vector<int>> box2) {

  bool doesfit = true;
  // Does box 1 fit in box 2
  for (size_t i = 0; i < box1.second.size(); i++) {
    if (box1.second[i] >= box2.second[i]) {
      doesfit = false;
      break;
    }
  }
  if (doesfit)
    return -1;

  doesfit = true;
  // Fit box 2 in box 1
  for (size_t i = 0; i < box1.second.size(); i++) {
    if (box2.second[i] >= box1.second[i]) {
      doesfit = false;
      break;
    }
  }
  if (doesfit)
    return 1;

  return 0;
}

bool boxcomparesize(pair<int, vector<int>> box1, pair<int, vector<int>> box2) {
  return (accumulate(box1.second.begin(), box1.second.end(), 0) <
          accumulate(box2.second.begin(), box2.second.end(), 0));
}

vector<int> boxtree(vector<pair<int, vector<int>>> boxes, int start,
                    vector<int> chain) {
  chain.push_back(start);
  if (start >= boxes.size())
    return chain;

  auto maxchain = chain;
  for (size_t i = start + 1; i < boxes.size(); i++) {
    // If start box fits in box i, build a new chain
    if (boxcompare(boxes[start], boxes[i]) < 0){
        auto newchain = chain;
        auto retchain = boxtree(boxes, i, newchain);
        if (retchain.size() > maxchain.size())
        maxchain = retchain;
    }
  }
  return maxchain;
}

int main() {
  string line;
  while (getline(cin, line)) {
    if (line == "")
      continue;

    stringstream ss;
    ss << line;
    int k, n;
    ss >> k >> n;

    vector<pair<int, vector<int>>> boxes;
    // Get box measures
    for (size_t i = 0; i < k; i++) {
      vector<int> newbox;
      for (size_t j = 0; j < n; j++) {
        int m;
        cin >> m;
        newbox.push_back(m);
      }

      // Sort box measures
      sort(newbox.begin(), newbox.end());

      boxes.push_back({i, newbox});
    }

    // Sort k boxes on length
    sort(boxes.begin(), boxes.end(), boxcomparesize);

    // Determine if boxes fit
    vector<int> chain;
    vector<int> maxchain;
    for (size_t i = 0; i < k - 1; i++) {
      chain.clear();
      // chain.push_back(i);

      chain = boxtree(boxes, i, chain);
      if (chain.size() > maxchain.size())
        maxchain = chain;
    }

    for (size_t i = 0; i < maxchain.size(); i++) {
      cout << boxes[maxchain[i]].first + 1 << " ";
    }
    cout << endl;
  }
}