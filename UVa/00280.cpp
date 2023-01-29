#include <iostream>
#include <vector>

std::vector<std::vector<int>> graph;
std::vector<bool> visited;

void dfs(int start) {
	for (size_t i = 0; i < graph[start].size(); i++)
	{
		if (!visited[graph[start][i]]) {
			visited[graph[start][i]] = true;
			dfs(graph[start][i]);
		}
	}
}

int main() {
	while (true)
	{
		int nodecount;
		std::cin >> nodecount;
		if (nodecount == 0)
			break;

		graph = std::vector<std::vector<int>>(nodecount);

		int from;
		int target;
		while (true) { // Get vertices
			std::cin >> from;
			if (from != 0) {
				while (true)
				{
					std::cin >> target;
					if (target != 0)
					{
						graph[from - 1].push_back(target - 1);
					}
					else {
						break;
					}
				}
			}
			else {
				break;
			}
		}
		int invest_count;
		std::cin >> invest_count;
		std::vector<int> invest_starts(invest_count);
		for (size_t i = 0; i < invest_count; i++)
		{
			int start;
			std::cin >> start;
			--start;

			//clear visited
			visited = std::vector<bool>(nodecount, false);
			//traverse through graph
			dfs(start);
			// find non visited
			std::vector<int> notvisited;
			for (size_t j = 0; j < nodecount; j++)
			{
				if (!visited[j]) {
					notvisited.push_back(j);
				}
			}
			//print
			std::cout << notvisited.size();
			for (size_t j = 0; j < notvisited.size(); j++)
			{
				std::cout << " " << notvisited[j] + 1;
			}
			std::cout << std::endl;
		}

	}
}
