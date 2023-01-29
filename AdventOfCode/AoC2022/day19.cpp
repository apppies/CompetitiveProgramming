#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <string> 
#include <algorithm>
#include <queue>

using namespace std;

class blueprint {
public:
	int id, ore_ore, clay_ore, obs_ore, obs_clay, geo_ore, geo_obs;

	int max_ore() {
		return max(ore_ore, max(clay_ore, max(obs_ore, geo_ore)));
	}
};

const int NONE = -1;
const int ORE = 0;
const int CLAY = 1;
const int OBSIDIAN = 2;
const int GEODE = 3;

vector<int> max_geodes_at_t;
void run_blueprint(blueprint& b, int time, int ore, int clay, int obsidian, int geode, int ore_bot, int clay_bot, int obsidian_bot, int geode_bot, int new_bot) {
	if (geode > max_geodes_at_t[time]) {
		max_geodes_at_t[time] = geode;
	}
	if (time == 0) {
		return;
	}
	if (geode < max_geodes_at_t[time]) {
		return;
	}

	int newore = ore + ore_bot;
	int newclay = clay + clay_bot;
	int newobsidian = obsidian + obsidian_bot;
	int newgeode = geode + geode_bot;
	if (new_bot == ORE) ore_bot++;
	if (new_bot == CLAY) clay_bot++;
	if (new_bot == OBSIDIAN) obsidian_bot++;
	if (new_bot == GEODE) geode_bot++;
	time--;

	if (ore_bot < b.max_ore() && (ore < b.ore_ore || new_bot != NONE) && newore >= b.ore_ore) {
		run_blueprint(b, time, newore - b.ore_ore, newclay, newobsidian, newgeode, ore_bot, clay_bot, obsidian_bot, geode_bot, ORE);
	}
	if (clay_bot < b.obs_clay && (ore < b.clay_ore || new_bot != NONE) && newore >= b.clay_ore) {
		run_blueprint(b, time, newore - b.clay_ore, newclay, newobsidian, newgeode, ore_bot, clay_bot, obsidian_bot, geode_bot, CLAY);
	}
	if (obsidian_bot < b.geo_obs && (ore < b.obs_ore || clay < b.obs_clay || new_bot != NONE) && newore >= b.obs_ore && newclay >= b.obs_clay) {
		run_blueprint(b, time, newore - b.obs_ore, newclay - b.obs_clay, newobsidian, newgeode, ore_bot, clay_bot, obsidian_bot, geode_bot, OBSIDIAN);
	}
	if (newore >= b.geo_ore && newobsidian >= b.geo_obs) {
		run_blueprint(b, time, newore - b.geo_ore, newclay, newobsidian - b.geo_obs, newgeode, ore_bot, clay_bot, obsidian_bot, geode_bot, GEODE);
	}
	run_blueprint(b, time, newore, newclay, newobsidian, newgeode, ore_bot, clay_bot, obsidian_bot, geode_bot, NONE);
}

int main()
{
	ifstream file("day19.txt");
	string line;
	string blueprint_string = "Blueprint %d: Each ore robot costs %d ore. Each clay robot costs %d ore. Each obsidian robot costs %d ore and %d clay. Each geode robot costs %d ore and %d obsidian.";
	vector<blueprint> blueprints;
	while (file.good()) {
		getline(file, line);

		if (line.length() == 0)
			continue;

		int id, ore_ore, clay_ore, obs_ore, obs_clay, geo_ore, geo_obs;
		sscanf(line.c_str(), blueprint_string.c_str(), &id, &ore_ore, &clay_ore, &obs_ore, &obs_clay, &geo_ore, &geo_obs);
		blueprint b = { id, ore_ore, clay_ore, obs_ore, obs_clay, geo_ore, geo_obs };
		blueprints.push_back(b);
	}
	file.close();
	
	int quality_sum = 0;
	int endtime = 24;
	for (auto& b : blueprints) {
		max_geodes_at_t = vector<int>(endtime + 1, 0);
		run_blueprint(b, endtime, 0, 0, 0, 0, 1, 0, 0, 0, NONE);
		int quality = max_geodes_at_t[0] * b.id;
		quality_sum += quality;
	}

	cout << "Part 1: " << quality_sum << endl;

	int geode_multi = 1;
	endtime = 32;
	for (int i = 0; i < 3; i++)
	{
		max_geodes_at_t = vector<int>(endtime + 1, 0);
		run_blueprint(blueprints[i], endtime, 0, 0, 0, 0, 1, 0, 0, 0, NONE);
		geode_multi *= max_geodes_at_t[0];
	}

	cout << "Part 2: " << geode_multi << endl;
}
