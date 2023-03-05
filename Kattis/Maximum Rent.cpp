#include <string>
#include <iostream>

int main() {
    int a, b;
    std::cin >> a >> b;
    int m, s;
    std::cin >> m >> s;

    int n_a;
    int n_b;
    if (a >= b) {
        // max on a
        n_a = m - 1;
        n_b = 1;
        
    } else {
        // max on b, then correct what is needed to have 2a+b >= s
        n_a = 1;
        n_b = m - 1;
        int t_s = 2 * n_a + n_b;
        if (t_s < s) {
            int diff = s - t_s;
            n_a += diff;
            n_b -= diff;
        }
    }

    std::cout << n_a * a + n_b * b << std::endl;
}