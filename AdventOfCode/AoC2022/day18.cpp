#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <string> 
#include <algorithm>

using namespace std;

struct point {
	int x;
	int y;
	int z;
};

vector<point> get_nei(point& p, point& p_max) {
	vector<point> ret;
	if (p.x > 0) ret.push_back({ p.x - 1, p.y, p.z });
	if (p.x < p_max.x ) ret.push_back({ p.x + 1, p.y, p.z });
	if (p.y > 0) ret.push_back({ p.x, p.y - 1, p.z });
	if (p.y < p_max.y ) ret.push_back({ p.x, p.y + 1, p.z });
	if (p.z > 0) ret.push_back({ p.x, p.y, p.z - 1 });
	if (p.z < p_max.z ) ret.push_back({ p.x, p.y, p.z + 1 });
	/*
	if (p.x > 0 && p.y > 0) ret.push_back({ p.x - 1, p.y - 1, p.z });
	if (p.x > 0 && p.z > 0) ret.push_back({ p.x - 1, p.y, p.z - 1 });
	if (p.y > 0 && p.z > 0) ret.push_back({ p.x, p.y - 1, p.z - 1 });*/

	return ret;
}

int main()
{
	ifstream file("day18.txt");
	string line;
	vector<point> points;

	point p_min = { INT32_MAX,INT32_MAX,INT32_MAX };
	point p_max = { INT32_MIN,INT32_MIN,INT32_MIN };

	while (file.good()) {
		getline(file, line);
		int x, y, z;
		sscanf(line.c_str(), "%d,%d,%d", &x, &y, &z);
		points.push_back({ x,y,z });

		p_min.x = min(x, p_min.x);
		p_min.y = min(y, p_min.y);
		p_min.z = min(z, p_min.z);
		p_max.x = max(x, p_max.x);
		p_max.y = max(y, p_max.y);
		p_max.z = max(z, p_max.z);
	}
	file.close();

	cout << p_min.x << "," << p_min.y << "," << p_min.z << endl;
	cout << p_max.x << "," << p_max.y << "," << p_max.z << endl;

	vector<vector<vector<int>>> matrix;
	for (int z = p_min.z; z <= p_max.z; z++)
	{
		vector<vector<int>> plane;
		for (int y = p_min.y; y <= p_max.y; y++)
		{
			vector<int> row;
			for (int x = p_min.x; x <= p_max.x; x++)
			{
				row.push_back(0);
			}
			plane.push_back(row);
		}
		matrix.push_back(plane);
	}

	for (auto const& p : points) {
		matrix[p.z - p_min.z][p.y - p_min.y][p.x - p_min.x] = 1;
	}

	int freesides = 0;
	point p_max2 = { p_max.x - p_min.x, p_max.y - p_min.y, p_max.z - p_min.z };
	for (int z = 0; z < matrix.size(); z++)
	{
		for (int y = 0; y < matrix[0].size(); y++)
		{
			for (int x = 0; x < matrix[0][0].size(); x++)
			{
				if (matrix[z][y][x] == 1) {
					point test = { x,y,z };
					vector<point> nei = get_nei(test, p_max2);
					freesides += 6 - nei.size();
					for (auto const& n : nei) {
						if (matrix[n.z][n.y][n.x] == 0) {
							freesides++;
						}
					}
				}
			}
		}
	}

	cout << "Part 1: " << freesides << endl;

	// flood fill outside
	for (int z = 0; z < matrix.size(); z++)
	{
		for (int y = 0; y < matrix[0].size(); y++)
		{
			for (int x = 0; x < matrix[0][0].size(); x++)
			{
				if (matrix[z][y][x] == 0) {
					point test = { x,y,z };
					vector<point> nei = get_nei(test, p_max2);
					if (nei.size() < 6) {
						matrix[z][y][x] = 2;
					}
					else {
						for (auto const& n : nei) {
							if (matrix[n.z][n.y][n.x] == 2) {
								matrix[z][y][x] = 2;
								break;
							}
						}
					}
				}
			}
		}
	}
	for (int z = matrix.size() - 1; z>= 0; z--)
	{
		for (int y = matrix[0].size() - 1; y >= 0; y--)
		{
			for (int x = matrix[0][0].size() - 1; x >=0 ; x--)
			{
				if (matrix[z][y][x] == 0) {
					point test = { x,y,z };
					vector<point> nei = get_nei(test, p_max2);
					if (nei.size() < 6) {
						matrix[z][y][x] = 2;
					}
					else {
						for (auto const& n : nei) {
							if (matrix[n.z][n.y][n.x] == 2) {
								matrix[z][y][x] = 2;
								break;
							}
						}
					}
				}
			}
		}
	}

	int freesides2 = 0;
	for (int z = 0; z < matrix.size(); z++)
	{
		for (int y = 0; y < matrix[0].size(); y++)
		{
			for (int x = 0; x < matrix[0][0].size(); x++)
			{
				if (matrix[z][y][x] == 1) {
					point test = { x,y,z };
					vector<point> nei = get_nei(test, p_max2);
					freesides2 += 6 - nei.size();
					for (auto const& n : nei) {
						if (matrix[n.z][n.y][n.x] == 2) {
							freesides2++;
						}
					}
				}
			}
		}
	}

	cout << "Part 2: " << freesides2 << endl;
}
