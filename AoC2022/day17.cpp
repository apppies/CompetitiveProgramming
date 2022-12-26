#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <string> 
#include <algorithm>

using namespace std;

struct block {
	vector<vector<int>> map;

	int h() { return map.size(); }
	int w() { return map[0].size(); }
};

struct block2 {
	vector<int> map;
	int h;
	int w;
};

bool isemptyline(vector<int>& line) {
	for (auto const& i : line) {
		if (i > 0)
			return false;
	}
	return true;
}


void drawmap(vector<vector<int>>& map) {

	for (int i = 0; i < map.size(); i++)
	{
		cout << '|';
		for (int j = 0; j < 7; j++)
		{
			if (map[map.size() - i - 1][j] > 0)
				cout << '#';
			else
				cout << '.';
		}
		cout << '|' << endl;
	}
	cout << endl;
}

int dropblock(vector<vector<int>>& map, block& b, string& line, int& action) {
	int freespace = 3;
	int newlines = 0;
	bool falling = true;
	int left = 2;
	int top = 0;
	while (falling) {
		int toadd = 0;
		for (int i = 0; i < freespace; i++)
		{
			if (!isemptyline(map[map.size() - i - 1])) {
				toadd = freespace - i;
				break;
			}
		}
		for (int i = 0; i < toadd; i++)
		{
			map.push_back(vector<int>(7, 0));
		}
		newlines += toadd;

		// Move side
		int prevleft = left;
		if (line[action] == '>')
			left = min(left + 1, 7 - b.w());
		else
			left = max(left - 1, 0);

		// Check if side is valid when moving
		if (left != prevleft) {
			for (int i = 0; i < min(top - freespace, b.h()); i++)
			{
				int mapy = map.size() - top + i;
				if (mapy >= map.size())
					continue;

				for (int x = 0; x < b.w(); x++)
				{
					if (b.map[b.h() - i - 1][x] > 0 && // part of block
						map[mapy][x + left] > 0) { // and occupied
						left = prevleft;
						break;
					}
				}
				if (left == prevleft)
					break;
			}
		}

		// go down
		if (top >= freespace) {
			for (int i = 0; i < b.h(); i++)
			{
				int mapy = map.size() - top + i - 1;
				if (mapy >= map.size())
					continue;

				for (int x = 0; x < b.w(); x++)
				{
					if (b.map[b.h() - i - 1][x] > 0 && // part of block
						map[mapy][x + left] > 0) { // and occupied
						falling = false;
						break;
					}
				}
				if (!falling)
					break;
			}
		}

		if (map.size() - top == 0)
			falling = false;

		if (falling)
			top++;

		// next
		action++;
		action = action % line.size();
	}

	// merge block
	for (int i = 0; i < b.h(); i++)
	{
		int mapy = map.size() - top + i;
		if (mapy >= map.size()) {
			map.push_back(vector<int>(7, 0));
			newlines++;
		}
		for (int x = 0; x < b.w(); x++)
		{
			if (b.map[b.h() - i - 1][x] > 0) {// part of block
				map[mapy][x + left] = 1;
			}
		}
	}

	return newlines;
}

int dropblock2(vector<int>& map, block2& b, string& line, int& action) {
	int freespace = 3;
	int newlines = 0;
	bool falling = true;
	int left = 2;
	int top = 0;
	while (falling) {
		int toadd = 0;
		for (int i = 0; i < freespace; i++)
		{
			if (map[map.size() - i - 1] > 0) {
				toadd = freespace - i;
				break;
			}
		}
		for (int i = 0; i < toadd; i++)
		{
			map.push_back(0);
		}
		newlines += toadd;

		// Move side
		int prevleft = left;
		if (line[action] == '>')
			left = min(left + 1, 7 - b.w);
		else
			left = max(left - 1, 0);

		// Check if side is valid when moving
		if (left != prevleft) {
			for (int i = 0; i < min(top - freespace, b.h); i++)
			{
				int mapy = map.size() - top + i;
				if (mapy >= map.size())
					continue;

				if (((b.map[b.h - i - 1] >> left) & map[mapy]) > 0) {
					left = prevleft;
					break;
				}

				if (left == prevleft)
					break;
			}
		}

		// go down
		if (top >= freespace) {
			for (int i = 0; i < b.h; i++)
			{
				int mapy = map.size() - top + i - 1;
				if (mapy >= map.size())
					continue;

				if (((b.map[b.h - i - 1] >> left) & map[mapy]) > 0) {
					falling = false;
					break;
				}

				if (!falling)
					break;
			}
		}

		if (map.size() - top == 0)
			falling = false;

		if (falling)
			top++;

		// next
		action++;
		action = action % line.size();
	}

	// merge block
	for (int i = 0; i < b.h; i++)
	{
		int mapy = map.size() - top + i;
		if (mapy >= map.size()) {
			map.push_back(0);
			newlines++;
		}
		for (int x = 0; x < b.w; x++)
		{
			map[mapy] |= (b.map[b.h - i - 1] >> left);

		}
	}

	return newlines;
}


bool islineequal(vector<int>line1, vector<int>line2) {
	for (int i = 0; i < line1.size(); i++)
	{
		if (line1[i] != line2[i])
			return false;
	}
	return true;
}

int main()
{
	ifstream file("day17.txt");
	string line = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";

	getline(file, line);
	file.close();


	vector<block> blocks;
	vector<block2> blocks2;
	block b;
	b.map.push_back({ 1,1,1,1 });
	blocks.push_back(b);
	blocks2.push_back({ { 0b11110000 }, 1, 4 });

	b = block();
	b.map.push_back({ 0,1,0 });
	b.map.push_back({ 1,1,1 });
	b.map.push_back({ 0,1,0 });
	blocks.push_back(b);
	blocks2.push_back({ { 0b01000000, 0b11100000, 0b01000000 }, 3, 3 });

	b = block();
	b.map.push_back({ 0,0,1 });
	b.map.push_back({ 0,0,1 });
	b.map.push_back({ 1,1,1 });
	blocks.push_back(b);
	blocks2.push_back({ { 0b00100000, 0b00100000, 0b11100000 }, 3, 3 });

	b = block();
	b.map.push_back({ 1 });
	b.map.push_back({ 1 });
	b.map.push_back({ 1 });
	b.map.push_back({ 1 });
	blocks.push_back(b);
	blocks2.push_back({ { 0b10000000 , 0b10000000 , 0b10000000 ,0b10000000 }, 4, 1 });

	b = block();
	b.map.push_back({ 1, 1 });
	b.map.push_back({ 1, 1 });
	blocks.push_back(b);
	blocks2.push_back({ { 0b11000000 , 0b11000000}, 2, 2 });

	vector<vector<int>> map;
	map.push_back(vector<int>(7, 0));
	map.push_back(vector<int>(7, 0));
	map.push_back(vector<int>(7, 0));

	vector<int>map2(3, 0);


	cout << "Line length: " << line.length() << endl;
	cout << "lxb: " << line.length() * blocks.size() << endl;

	int const freespace = 3;

	int action = 0, action2 = 0;
	for (int n = 0; n < 2022; n++)
	{
		dropblock(map, blocks[n % blocks.size()], line, action);
		dropblock2(map2, blocks2[n % blocks2.size()], line, action2);
	}

	// Get size
	int part1 = 0;
	for (int i = map.size() - 1; i >= 0; i--)
	{
		if (!isemptyline(map[i])) {
			part1 = i + 1;
			break;
		}
	}
	cout << "Part 1: " << part1 << endl;

	// Get size
	part1 = 0;
	for (int i = map2.size() - 1; i >= 0; i--)
	{
		if (map2[i] > 0) {
			part1 = i + 1;
			break;
		}
	}
	cout << "Part 1: " << part1 << endl;


	vector<int>map3(3, 0);

	uint64_t end = 1000000000000;
	
	vector<int> heights;

	action = 0;
	for (int n = 0; n < 400000; n++)
	{
		int newlines = dropblock2(map3, blocks2[n % blocks2.size()], line, action);
		int height = 0;
		for (int i = map3.size() - 1; i >= 0; i--)
		{
			if (map3[i] > 0) {
				height = i + 1;
				break;
			}
		}
		heights.push_back(height);

		if (map3.size() > 60000 ) {
			// try to find repeating pattern
			bool failed = false;

			for (int j = map3.size() - newlines - 100; j < map3.size() - 100; j++)
			{
				vector<int> matches;
				for (int k = 0; k < j; k++)
				{
					if (map3[j] == map3[k]) {
						int lefttocompare = min(10, (int)(map3.size() - j));
						for (int i = 1; i < lefttocompare; i++)
						{
							if (map3[k + i] != map3[j + i]) {
								failed = true;
								break;
							}
						}

						if (!failed) {
							matches.push_back(k);
							//cout << "Found equal lines at: row " << k << " and " << j << endl;
						}
					}
				}

				bool pattern = true;
				if (matches.size() > 2) {
					int delta = matches[1] - matches[0];
					for (int i = 2; i < matches.size(); i++)
					{
						if (matches[i] - matches[i - 1] != delta)
						{
							pattern = false;
							break;
						}
					}
					if (pattern) {
						//guess output
						int first = matches[0];
						int curheight = 0;
						for (int i = map3.size() - 1; i >= 0; i--)
						{
							if (map3[i] > 0) {
								curheight = i + 1;
								break;
							}
						}
						cout << "Blocks dropped: " <<  n << ". Current height: " << curheight << " Found pattern starting at " << first << " with period " << delta << endl;

						uint64_t n1 = -1;
						for (int h = 0; h < heights.size(); h++)
						{
							if (heights[h] == first) {
								cout << "height: " << first << " reached by stone " << h + 1 <<endl;
								n1 = h + 1;
								break;
							}
						}
						if (n1 == -1) {
							continue;
						}

						uint64_t n2 = -1;
						for (int h = 0; h < heights.size(); h++)
						{
							if (heights[h] == first + delta) {
								cout << "height: " << first + delta << " reached by stone " << h + 1 << endl;
								n2 = h + 1;
								break;
							}
						}
						if (n2 == -1) {
							continue;
						}

						uint64_t t_h = heights[n2 - 1] - heights[n1 - 1];
						uint64_t t_n = n2 - n1;

						// amount of periods from n1 to end
						uint64_t numT = end / t_n;
						// remaining blocks from n1 + numT*t_n to end
						uint64_t remBlocks = end - (n1 + numT * t_n);

						uint64_t totalHeight = heights[n1 + remBlocks - 1] + numT * t_h;
						cout << "Part 2: " << totalHeight << endl;
						return 0;

					}
				}
			}

		}

	}
		cout << "NED" << endl;
}
