// https://open.kattis.com/problems/scalingrecipe
#include <iostream>
#include <vector>
#include <cmath>

int main() {
    int n, x, y;
    std::cin >> n >> x >> y;
    double f = (double)y / x;

    for (int i=0; i<n; i++){
        int a;
        std::cin >> a;
        int b = round(a * f);  // max value is 40k*40K. should fit in int, fix precision errors by rounding
        std::cout << b << std::endl;
    }    
}