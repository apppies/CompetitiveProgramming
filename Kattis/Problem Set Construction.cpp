#include <string>
#include <iostream>
#include <vector>


struct problem {
    int id;
    double p;
    int s;
};

int main() {

    int n,k,t;
    std::cin >> n >> k >> t;

    std::vector<problem> p;
    for (int i = 0; i < n; i++)
    {
        double p_;
        int s_;
        std::cin >> p_ >> s_;
        p.push_back({i, p_, s_});
    }
    
    // get the subset it will solve: maximize number of problems, minimize total time
    // no clue what the math should be.

}