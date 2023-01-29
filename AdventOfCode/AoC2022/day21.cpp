#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <map>
#include <queue>

using namespace std;

struct monkey
{
    string name = "";
    string svar1 = "";
    string svar2 = "";
    char op = ' ';

    long answer = 0;
    long lval1 = 0;
    long lval2 = 0;

    bool has_answer = false;
    bool has_lval1 = false;
    ;
    bool has_lval2 = false;
    ;

    monkey()
    {
    }

    monkey(string _name, string _svar1, char _op, string _svar2)
    {
        name = _name;
        svar1 = _svar1;
        op = _op;
        svar2 = _svar2;
    }

    monkey(string _name, long _answer)
    {
        name = _name;
        answer = _answer;
        has_answer = true;
    }
};

int main()
{
    ifstream file("day21.txt");
    vector<string> lines;
    queue<monkey *> q;
    map<string, monkey> monkeys;

    while (file.good())
    {
        string line;
        getline(file, line);
        if (line.length() > 0)
            lines.push_back(line);
    }
    file.close();

    for (auto const &line : lines)
    {
        string s1 = line.substr(0, 4);
        if (line.find(' ', 6) != string::npos)
        {
            string s2 = line.substr(6, 4);
            char s3 = line[11];
            string s4 = line.substr(13, 4);
            monkeys.insert({s1, monkey(s1, s2, s3, s4)});
        }
        else
        {
            long l1 = stol(line.substr(6));
            monkeys.insert({s1, monkey(s1, l1)});
        }
    }

    for (auto &m : monkeys)
    {
        q.push(&(m.second));
    }

    while (!q.empty())
    {
        auto m = q.front();
        q.pop();

        if (m->has_answer)
        {
            continue;
        }

        if (monkeys[m->svar1].has_answer)
        {
            m->lval1 = monkeys[m->svar1].answer;
            m->has_lval1 = true;
        }
        if (monkeys[m->svar2].has_answer)
        {
            m->lval2 = monkeys[m->svar2].answer;
            m->has_lval2 = true;
        }
        if (m->has_lval1 && m->has_lval2)
        {
            if (m->op == '+')
                m->answer = m->lval1 + m->lval2;
            else if (m->op == '-')
                m->answer = m->lval1 - m->lval2;
            else if (m->op == '*')
                m->answer = m->lval1 * m->lval2;
            else if (m->op == '/')
                m->answer = m->lval1 / m->lval2;
            m->has_answer = true;

            if (m->name == "root")
                break;
            continue;
        }

        q.push(m);
    }

    cout << "Part 1: " << monkeys["root"].answer << endl;

    // reset queue and map
    q = {};
    monkeys.clear();

    for (auto const &line : lines)
    {
        string s1 = line.substr(0, 4);
        if (line.find(' ', 6) != string::npos)
        {
            string s2 = line.substr(6, 4);
            char s3 = line[11];
            string s4 = line.substr(13, 4);
            monkeys.insert({s1, monkey(s1, s2, s3, s4)});
        }
        else
        {
            long l1 = stol(line.substr(6));
            monkeys.insert({s1, monkey(s1, l1)});
        }
    }

    monkeys["root"].op = '=';
    monkeys["humn"].has_answer = false;

    for (auto &m : monkeys)
    {
        q.push(&(m.second));
    }

    while (!q.empty())
    {
        auto m = q.front();
        q.pop();

        if ((m->has_answer && m->has_lval1 && m->has_lval2) || (m->has_answer && m->op == ' '))
        {
            continue;
        }

        if (monkeys[m->svar1].has_answer)
        {
            m->lval1 = monkeys[m->svar1].answer;
            m->has_lval1 = true;
        }
        if (monkeys[m->svar2].has_answer)
        {
            m->lval2 = monkeys[m->svar2].answer;
            m->has_lval2 = true;
        }

        if (m->has_lval1 && m->has_lval2)
        {
            if (m->op == '+')
                m->answer = m->lval1 + m->lval2;
            else if (m->op == '-')
                m->answer = m->lval1 - m->lval2;
            else if (m->op == '*')
                m->answer = m->lval1 * m->lval2;
            else if (m->op == '/')
                m->answer = m->lval1 / m->lval2;
            m->has_answer = true;
            continue;
        }
        if (m->has_answer && m->has_lval1)
        {
            if (m->op == '+')
                m->lval2 = m->answer - m->lval1;
            else if (m->op == '-')
                m->lval2 = m->lval1 - m->answer;
            else if (m->op == '*')
                m->lval2 = m->answer / m->lval1;
            else if (m->op == '/')
                m->lval2 = m->lval1 / m->answer;
            m->has_lval2 = true;
            monkeys[m->svar2].answer = m->lval2;
            monkeys[m->svar2].has_answer = true;
            continue;
        }
        if (m->has_answer && m->has_lval2)
        {
            if (m->op == '+')
                m->lval1 = m->answer - m->lval2;
            else if (m->op == '-')
                m->lval1 = m->lval2 + m->answer;
            else if (m->op == '*')
                m->lval1 = m->answer / m->lval2;
            else if (m->op == '/')
                m->lval1 = m->lval2 * m->answer;
            m->has_lval1 = true;
            monkeys[m->svar1].answer = m->lval1;
            monkeys[m->svar1].has_answer = true;
            continue;
        }
        if (m->op == '=' && m->has_lval1)
        {
            m->lval2 = m->lval1;
            m->has_lval2 = true;

            monkeys[m->svar2].answer = m->lval1;
            monkeys[m->svar2].has_answer = true;
            continue;
        }
        if (m->op == '=' && m->has_lval2)
        {
            m->lval1 = m->lval2;
            m->has_lval1 = true;
            monkeys[m->svar1].answer = m->lval2;
            monkeys[m->svar1].has_answer = true;
            continue;
        }

        q.push(m);
    }

    cout << "Part 2: " << monkeys["humn"].answer << endl;
}
