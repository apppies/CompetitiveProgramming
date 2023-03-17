// https://open.kattis.com/problems/completingthesquare
#include <iostream>
#include <vector>
#include <cmath>
#include <tuple>
using namespace std;

int main() {

    std::vector<pair<int,int>> coords;

    for (int i=0; i<3; i++){
        int x,y;
        std::cin >> x >> y;
        coords.push_back({x,y});
    }

    std::vector<double> lines(3);
    lines[0] = sqrt(abs(coords[0].first - coords[1].first) * abs(coords[0].first - coords[1].first)  +  abs(coords[0].second - coords[1].second) * abs(coords[0].second - coords[1].second)); // 0 -> 1
    lines[1] = sqrt(abs(coords[1].first - coords[2].first) * abs(coords[1].first - coords[2].first)  +  abs(coords[1].second - coords[2].second) * abs(coords[1].second - coords[2].second)); // 1 -> 2
    lines[2] = sqrt(abs(coords[2].first - coords[0].first) * abs(coords[2].first - coords[0].first)  +  abs(coords[2].second - coords[0].second) * abs(coords[2].second - coords[0].second)); // 2 -> 0

    int tx = 0,ty = 0;
  
    if (lines[0] < lines[1]){
        if (lines[1] < lines[2]){
            tx =  coords[1].first + coords[0].first - coords[2].first;
            ty =  coords[1].second + coords[0].second - coords[2].second;
        } else {
            tx =  coords[1].first + coords[2].first - coords[0].first;
            ty =  coords[1].second + coords[2].second - coords[0].second;
        }
    } else {
        if (lines[0] < lines[2]){
            tx = coords[0].first + coords[2].first - coords[1].first;
            ty = coords[0].second + coords[2].second - coords[1].second;
        } else {
            tx = coords[0].first + coords[1].first - coords[2].first;
            ty = coords[0].second + coords[1].second - coords[2].second;
        }
    }

    std::cout << tx << " "<< ty << endl;

}

