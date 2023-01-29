#include <iostream>
#include <sstream>
#include <string>
#include <vector>

using namespace std;

int main()
{
    string line;

    int bins[3][3];
    string names[] = {"B", "G", "C"};

    while (getline(cin, line))
    {
        stringstream ss;
        ss << line;

        // Parse line with bottle info
        int n;
        for (size_t i = 0; i < 9; i++)
        {
            ss >> n;
            bins[i / 3][i % 3] = n;
        }

        // bruteforce, note that this does not scale well.. Nicer would be something recursive
        int minbottlesmoved = INT32_MAX;
        int bottlesmoved = 0;
        string bottlesmovedconfig = "ZZZ";
        for (size_t i = 0; i < 3; i++)
        {
            for (size_t j = 0; j < 3; j++)
            {
                if (j != i)
                {
                    for (size_t k = 0; k < 3; k++)
                    {
                        if (k != i && k != j)
                        {
                            // Config:
                            // bin 0 contains bottles i
                            // bin 1 contains bottles j != i
                            // bin 2 contains bottles k != i && !=j
                            bottlesmoved = bins[0][j] + bins[0][k] + bins[1][i] + bins[1][k] + bins[2][i] + bins[2][j];
                            if (bottlesmoved <= minbottlesmoved)
                            {
                                string newbottlesmovedconfig = names[i] + names[j] + names[k];
                                if (newbottlesmovedconfig < bottlesmovedconfig || bottlesmoved < minbottlesmoved)
                                    bottlesmovedconfig = newbottlesmovedconfig;
                                minbottlesmoved = bottlesmoved;
                            }
                        }
                    }
                }
            }
        }

        cout << bottlesmovedconfig << " " << minbottlesmoved << endl;
    }
}