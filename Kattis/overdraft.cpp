// https://open.kattis.com/problems/overdraft
#include <iostream>
int main(){
    int n;
    std::cin >> n;
    int sum = 0;
    int min = 0;
    for (int i=0; i<n; i++){
        int in;
        std::cin >> in;
        sum += in;
        if (sum < min) min = sum;
    }
    
    std::cout << -1 * min << std::endl;
}