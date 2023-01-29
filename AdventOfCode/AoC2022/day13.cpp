#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <algorithm> // std::sort

using namespace std;

string get_first_num(string line, int &s)
{
    int b = s;
    while (line[s] >= '0' && line[s] <= '9')
    {
        s++;
    }
    return line.substr(b, s - b);
}

string get_next_list(string line)
{
    int c = 0;
    for (int i = 0; i < line.length(); i++)
    {
        if (line[i] == ']' && c == 1)
        {
            return line.substr(0, i + 1);
        }
        else if (line[i] == '[')
        {
            c++;
        }
        else if (line[i] == ']')
        {
            c--;
        }
    }
    return line;
}

int compare_list(string list1, string list2, int level)
{
    int i1 = 0, i2 = 0;
    while (i1 < list1.length() && i2 < list2.length())
    {
        if (list1[i1] == '[' && list2[i2] != '[')
        {
            string nextlist1 = get_next_list(list1.substr(i1));
            string nextnum2 = get_first_num(list2, i2);
            if (nextnum2.length() == 0)
                return 1;
            int ret = compare_list(nextlist1, "[" + nextnum2 + "]", level+1);
            if (ret != 0)
                return ret;
            else
            {
                i1 += nextlist1.length();
            }
        }
        else if (list1[i1] != '[' && list2[i2] == '[')
        {
            string nextlist2 = get_next_list(list2.substr(i2));
            string nextnum1 = get_first_num(list1, i1);
            if (nextnum1.length() == 0)
                return -1;
            int ret = compare_list("[" + nextnum1 + "]", nextlist2, level+1);
            if (ret != 0)
                return ret;
            else
            {
                i2 += nextlist2.length();
            }
        }
        else if (list1[i1] == '[' && list2[i2] == '[')
        {
            // increment
        }
        else
        {
            if (list1[i1] >= '0' && list1[i1] <= '9' && list2[i2] >= '0' && list2[i2] <= '9')
            {
                int num1 = stoi(get_first_num(list1, i1));
                int num2 = stoi(get_first_num(list2, i2));
                if (num1 < num2)
                {
                    return -1;
                }
                else if (num2 < num1)
                {
                    return 1;
                }
            }
            else if (list1[i1] >= '0' && list1[i1] <= '9')
            {
                return 1;
            }
            else if (list2[i2] >= '0' && list2[i2] <= '9')
            {
                return -1;
            }
            else if (list1[i1] == ',' && list2[i2] == ']')
            {
                return 1;
            }
            else if (list1[i1] == ']' && list2[i2] == ',')
            {
                return -1;
            }
        }
        i1++;
        i2++;
    }
    if (i1 == list1.length() && i2 == list2.length())
        return 0;
    else if (i1 == list1.length())
        return -1;
    else if (i2 == list2.length())
        return 1;
    else
        return 0;
}

bool compline(string line1, string line2)
{
    int r = compare_list(line1, line2, 0);
    if (r == 1)
        return false;
    else
        return true;
}

int main()
{
    int r;
    r = compare_list("[1,2]", "[2,1]", 0);
    r = compare_list("[[1],2]", "[]", 0);

    ifstream file("day13.txt");
    vector<string> lines;

    int pairindex = 1;
    int totalcorrect = 0;
    while (file.good())
    {
        string line;
        getline(file, line);
        if (line.length() == 0)
            continue;

        string line1 = line;
        lines.push_back(line);

        getline(file, line);
        string line2 = line;
        lines.push_back(line);

        int ans = compare_list(line1, line2, 0);

        if (ans == -1)
        {
            totalcorrect += pairindex;
        }
        pairindex++;
    }
    file.close();

    cout << "Part 1: " << totalcorrect << endl;

    lines.push_back("[[2]]");
    lines.push_back("[[6]]");
    sort(lines.begin(), lines.end(), compline);

    int i2 = 0;
    int i6 = 0;
    for (int i = 0; i < lines.size(); i++)
    {
        if (lines[i] == "[[2]]")
        {
            i2 = i + 1;
        }
        if (lines[i] == "[[6]]")
        {
            i6 = i + 1;
        }
    }
    cout << "Part 2: " << i2 * i6 << endl;
}