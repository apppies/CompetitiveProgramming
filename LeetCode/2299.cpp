#include <string>
#include <cassert>
using namespace std;

class Solution {
public:
    bool strongPasswordCheckerII(string password) {
        if (password.length() < 8) return false;
        bool lowercase = false;
        bool uppercase = false;
        bool digit = false;
        bool special = false;
        
        string specials = "!@#$%^&*()-+";

        for (size_t i = 0; i < password.length(); i++)
        {
            char c = password[i];
            if (i < password.length() && password[i + 1] == c)
                return false;

            if (!lowercase && c >= 'a' && c <= 'z')
                lowercase =true;

            if (!uppercase && c >= 'A' && c <= 'Z')
                uppercase =true;

            if (!digit && c >= '0' && c <= '9')
                digit = true;

            if (!special && specials.find(c) != string::npos)
                special = true;
        }

        return lowercase && uppercase && digit && special;        
    }
};

int main(){
    Solution sol;
    assert(true == sol.strongPasswordCheckerII("IloveLe3tcode!"));
    assert(false == sol.strongPasswordCheckerII("Me+You--IsMyDream"));
    assert(false == sol.strongPasswordCheckerII("1aB!"));
}