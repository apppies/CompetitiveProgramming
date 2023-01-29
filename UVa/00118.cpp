#define _CRT_SECURE_NO_WARNINGS
#include <string.h>
#include <iostream>
#include <vector>
#include <stdio.h>


int main() {
	int w;
	int h;
//	scanf("%i %i", &w, &h);
	std::cin >> w >> h;
	++w;
	++h;
	std::vector<int> map = std::vector<int>(w*h, 0);

	int x, y;
	char heading;
	std::string line;
	//while (scanf("%i %i %c", &x, &y, &heading)) {
	while (std::cin >> x >> y >> heading >> line) {
		bool death = false;
		for (size_t i = 0; i < line.length(); i++)
		{
			switch (line[i])
			{
			case 'R':
				if (heading == 'E') heading = 'S';
				else if (heading == 'S') heading = 'W';
				else if (heading == 'W') heading = 'N';
				else if (heading == 'N') heading = 'E';
				break;
			case 'L':
				if (heading == 'E') heading = 'N';
				else if (heading == 'N') heading = 'W';
				else if (heading == 'W') heading = 'S';
				else if (heading == 'S') heading = 'E';
				break;
			case 'F':
			{
				int newx = x;
				int newy = y;
				if (heading == 'E') ++newx;
				else if (heading == 'S') --newy;
				else if (heading == 'W') --newx;
				else if (heading == 'N') ++newy;

				if (newx < 0 || newx >= w || newy < 0 || newy >= h) {
					if (map[(x * w) + y] == 0) {
						// leave trace and die
						map[(x * w) + y] = 1;
						death = true;
					}
				}
				else {
					x = newx;
					y = newy;
				}
				break;
			}
			default:
				break;
			}

			if (death)
				break;
		}
		std::cout << x << " " << y << " " << heading;
		if (death) std::cout << " LOST";
		std::cout << std::endl;
	}
}