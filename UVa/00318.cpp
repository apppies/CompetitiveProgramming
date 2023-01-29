#include <stdio.h>
#include <vector>
#include <iostream>
#include <tuple>
#include <string>
#include <sstream>
#include <math.h>
#include <queue>
#include <iomanip>
#include <algorithm>

using graph = std::vector<std::vector<std::pair<int, int>>>;
graph m_graph(510);
std::vector<int> keys(510, -1);

void dominoes(int start)
{

	// BFS to find when all keys drop
	std::queue<int> q;
	q.push(start);
	keys[start] = 0;

	while (!q.empty())
	{
		int k = q.front();
		q.pop();
		for (size_t i = 0; i < m_graph[k].size(); i++)
		{
			auto edge = m_graph[k][i];
			if (keys[edge.first] == -1 || keys[edge.first] > keys[k] + edge.second)
			{
				keys[edge.first] = keys[k] + edge.second;
				q.push(edge.first);
			}
		}
	}

	// find final fall time, from key domino and dominoes in between
	float maxfinaltime = 0;
	std::vector<int> finalkeys = {1};
	for (size_t i = 0; i < keys.size(); i++)
	{
		if (keys[i] != -1)
		{
			if (keys[i] > maxfinaltime || (keys[i] == maxfinaltime && finalkeys.size() > 1))
			{
				maxfinaltime = keys[i];
				finalkeys.clear();
				finalkeys.push_back(i);
			}

			// check paths in between
			for (size_t j = 0; j < m_graph[i].size(); j++)
			{
				int to = m_graph[i][j].first;
				int time = m_graph[i][j].second;

				if (keys[to] + time > keys[i]) // dominoes are still falling in between
				{
					float finaltime = keys[i] + (time - (keys[i] - keys[to])) / 2.0;
					if (finaltime > maxfinaltime)
					{
						maxfinaltime = finaltime;
						finalkeys.clear();
						finalkeys.push_back(i);
						finalkeys.push_back(to);
					}
				}
			}
		}
	}

	std::cout << std::fixed;
	std::cout << std::setprecision(1);
	if (finalkeys.size() == 1)
	{
		std::cout << "The last domino falls after " << maxfinaltime << " seconds, at key domino " << finalkeys[0] << "." << std::endl;
	}
	else
	{
		std::sort(finalkeys.begin(), finalkeys.end());
		std::cout << "The last domino falls after " << maxfinaltime << " seconds, between key dominoes " << finalkeys[0] << " and " << finalkeys[1] << "." << std::endl;
	}
}

int main()
{
	int system = 1;
	std::string line;

	int n; // number of key dominoes
	int m; // number of rows
	while (true)
	{
		std::cin >> n;
		std::cin >> m;
		
		if (n == 0 && m == 0)
			break;

		for (size_t i = 0; i < m_graph.size(); i++)
		{
			m_graph[i].clear();
			keys[i] = -1;
		}

		for (size_t i = 0; i < m; i++)
		{
			int a, b, l;
			std::cin >> a >> b >> l;
			m_graph[a].push_back(std::pair<int, int>{b, l});
			m_graph[b].push_back(std::pair<int, int>{a, l});
		}

		std::cout << "System #" << system << std::endl;
		dominoes(1);

		std::cout << std::endl;
		system++;
	}
}