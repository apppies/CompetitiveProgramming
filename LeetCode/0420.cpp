
// Not correct yet

#include <string>
#include <list>
#include <iterator>
#include <cassert>
using namespace std;

class Solution
{
private:
public:
    int strongPasswordChecker(string password)
    {
        int actionsNeeded = 0;

        int removalNeeded = 0;
        int insertsNeeded = 0;
        if (password.size() < 6)
            insertsNeeded += (6 - password.size());
        if (password.size() > 20)
            removalNeeded += (password.size() - 20);

        bool hasUpper = false;
        bool hasLower = false;
        bool hasDigit = false;
        for(auto const & c: password){
            if (!hasUpper && c >= 'A' && c <= 'Z')
                hasUpper = true;

            if (!hasLower && c >= 'a' && c <= 'z')
                hasLower = true;

            if (!hasDigit && c >= '0' && c <= '9')
                hasDigit = true;
        }

        int replacementsNeeded = 0;
        int offset = 0;
        if (password.size() > 2){
        for (int i = 0; i < password.size() - 2; i++)
        {
            if (password[i] == password[i+1] && password[i] == password[i+2])
            {
                if (insertsNeeded > 0) { // insert something between i+1 and i+2
                    actionsNeeded++;
                    insertsNeeded--;
                    if (hasUpper == false) {
                        hasUpper = true;
                    }
                    else if (hasLower == false) {
                        hasLower = true;
                    }
                    else if (hasDigit == false) {
                        hasDigit = true;
                    }
                    i++; // no need to check i+1,i+2,i+3 as an insert should be placed between i+1 and i+2.
                } else if (removalNeeded > 0) {
                    // remove i
                    actionsNeeded++;
                    removalNeeded--;
                } else {
                    //replace i + 2 
                    actionsNeeded++;
                    if (hasUpper == false) {
                        hasUpper = true;
                    }
                    else if (hasLower == false) {
                        hasLower = true;
                    }
                    else if (hasDigit == false) {
                        hasDigit = true;
                    }
                    i+=2;
                }
            }
        }
        }
        for (int i = 0; i < insertsNeeded; i++)
        {
            actionsNeeded++;
            if (hasUpper == false) {
                hasUpper = true;
            }
            else if (hasLower == false) {
                hasLower = true;
            }
            else if (hasDigit == false) {
                hasDigit = true;
            }
        }

        if (hasUpper == false) {
            actionsNeeded++;
        }
        if (hasLower == false) {
           actionsNeeded++;
        }
        if (hasDigit == false) {
            actionsNeeded++;
        }

        actionsNeeded += removalNeeded;

        return actionsNeeded;
    }
};

int main()
{
    Solution sol;
    assert(8 == sol.strongPasswordChecker("bbaaaaaaaaaaaaaaacccccc"));
    assert(5 == sol.strongPasswordChecker("a"));
    assert(3 == sol.strongPasswordChecker("aA1"));
    assert(0 == sol.strongPasswordChecker("1337C0d3"));
    assert(1 == sol.strongPasswordChecker("aaa123"));
    assert(3 == sol.strongPasswordChecker("1111111111"));
    assert(2 == sol.strongPasswordChecker("ABABABABABABABABABAB1"));
}
