// https://open.kattis.com/problems/tournamentseeding
#include <vector>
#include <string>
#include <iostream>
#include <algorithm>

int main()
{
    int rounds, k;
    std::cin >> rounds >> k;
    std::vector<int> ratings;

    for (int i = 0; i < (1 << rounds); i++)
    {
        int r;
        std::cin >> r;
        ratings.push_back(r);
    }

    std::sort(ratings.begin(), ratings.end(), std::greater<int>());

    // Play games
    int close = 0;

    for (int n = 0; n < rounds; n++)
    {
        int inTournament = 1 << n;
        int playersInRound = 2 << n;
        int firstPlayerToCheck = playersInRound; // start by checking the lowest ranked player
        for (int j = inTournament - 1; j >= 0; j--)
        {
            int toFight = -1;
            for (int i = firstPlayerToCheck - 1; i >= inTournament; i--)
            {
                // find first one that counts as close, starting from the back of the field
                if (ratings[j] - ratings[i] <= k)
                {
                    toFight = i;
                    break;    
                }
            }
            if (toFight >= inTournament)
            {
                // found suitable match that is close
                firstPlayerToCheck = toFight;
                close++;
            }
            else
            {
                // found no close match, no possibility for others to find one, as they are higher ranked
                break;
            }
        }
        
    }


    std::cout << close << std::endl;
}