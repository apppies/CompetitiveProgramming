#pragma once
#include <iostream>
#include <vector>
#include <string>
std::vector<int> caverns[27];
bool lit[27];


int main() {
	std::string line;
	while (std::getline(std::cin, line)) {
		if (line == "#" || line == "")
			break;

		for (int i = 0; i < 26; ++i)
		{
			lit[i] = false;
			caverns[i].clear();
		}

		int state = 0;
		int caveid = 0;
		int minotaur = 0;
		int theseus = 1;
		int k = 3;
		for (size_t i = 0; i < line.length(); i++)
		{
			switch (state)
			{
			case 0:
				if (line[i] == ':') {
					state++;
				}
				else if (line[i] == '.') {
					state+=2;
				}
				else {
					caveid = line[i] - 'A';
				}
				break;
			case 1:
				if (line[i] == ';') {
					state--;
				}
				else if (line[i] == '.') {
					state++;
				}
				else {
					caverns[caveid].push_back(line[i] - 'A');
				}
				break;
			case 2:
			{
				if (line[i] != ' ') {
					minotaur = line[i] - 'A';
					theseus = line[i + 2] - 'A';
					k = atoi(line.substr(i + 4).c_str());
					i += 4;
					state = 3;
				}
				break;
			}

			default:
				break;
			}
		}

		// Solve
		int steps = 0;
		while (true)
		{
			int next = -1;
			for (int i : caverns[minotaur]) {
				if (i != theseus && lit[i] == false) {
					next = i;
					break;
				}
			}
			if (next == -1) {
				// Death
				std::cout << "/" << (char)(minotaur + 'A') << std::endl;
				break;
			}
			else {
				theseus = minotaur;
				minotaur = next;
				steps++;
				// Light a candle?
				if (steps % k == 0) {
					lit[theseus] = true;
					std::cout << (char)(theseus + 'A') << ' ';
				}
			}

		}
	}
}
