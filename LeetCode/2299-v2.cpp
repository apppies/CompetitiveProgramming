#include <string>
#include <cassert>
using namespace std;

class Solution
{
private:
public:
    bool strongPasswordCheckerII(string password)
    {
        string specials = "!@#$%^&*()-+";
        if (password.length() < 8)
            return false;
        uint8_t bits = 0;
        for (int i = 0; i < password.length(); i++)
        {
            if (i < password.length() && password[i + 1] == password[i])
                return false;

            if (!(bits & 1) && password[i] >= 'a' && password[i] <= 'z')
                bits |= 1;

            if (!(bits & 2) && password[i] >= 'A' && password[i] <= 'Z')
                bits |= 2;

            if (!(bits & 4) && password[i] >= '0' && password[i] <= '9')
                bits |= 4;

            if (!(bits & 8) && specials.find(password[i]) != string::npos)
                bits |= 8;
        }

        return (bits == 15);
    }
};

int main()
{
    Solution sol;
    assert(true == sol.strongPasswordCheckerII("IloveLe3tcode!"));
    assert(false == sol.strongPasswordCheckerII("Me+You--IsMyDream"));
    assert(false == sol.strongPasswordCheckerII("1aB!"));
    assert(false == sol.strongPasswordCheckerII("ecuwcfoyajkolntovfniplayrxhzpmhrkhzonopcwxgupzhoupw"));
}