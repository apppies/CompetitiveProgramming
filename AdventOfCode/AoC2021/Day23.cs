using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    internal class Day23
    {

        string start = "...........DCBCBDAA";
        string sample = "...........BACDBCDA";
        string target = "...........AABBCCDD";
        //string start = "...........D  CB  CB  DA  A";
        string start2 =  "...........DDDCBCBCBBADAACA";
        string sample2 = "...........BDDACCBDBBACDACA";
        string target2 = "...........AAAABBBBCCCCDDDD";
        Dictionary<char, int> costs = new();

        public void Solve()
        {
            //#############
            //#...........#
            //###D#B#B#A###
            //  #C#C#D#A#
            //  #########            
            costs.Add('A', 1);
            costs.Add('B', 10);
            costs.Add('C', 100);
            costs.Add('D', 1000);
            // start2 = sample2;
            var q = new Queue<string>();
            q.Enqueue(start);
            var allStates = new Dictionary<string, int>();
            allStates.Add(start, 0);

            while (q.Count > 0)
            {
                var state = q.Dequeue();
                var newStates = Move(state);
                foreach (var newState in newStates)
                {
                    if (allStates.ContainsKey(newState.State))
                    {
                        if (allStates[newState.State] > allStates[state] + newState.Cost)
                        {
                            allStates[newState.State] = allStates[state] + newState.Cost;
                            q.Enqueue(newState.State);
                        }
                    }
                    else
                    {
                        allStates.Add(newState.State, allStates[state] + newState.Cost);
                        q.Enqueue(newState.State);
                    }
                }
            }

            Console.WriteLine(allStates[target]);

            // start = sample;
            var q2 = new Queue<string>();
            q2.Enqueue(start2);
            var allStates2 = new Dictionary<string, int>();
            allStates2.Add(start2, 0);

            while (q2.Count > 0)
            {
                var state = q2.Dequeue();
                var newStates = Move2(state);
                foreach (var newState in newStates)
                {
                    if (allStates2.ContainsKey(newState.State))
                    {
                        if (allStates2[newState.State] > allStates2[state] + newState.Cost)
                        {
                            allStates2[newState.State] = allStates2[state] + newState.Cost;
                            q2.Enqueue(newState.State);
                        }
                    }
                    else
                    {
                        allStates2.Add(newState.State, allStates2[state] + newState.Cost);
                        q2.Enqueue(newState.State);
                    }
                }
            }

            Console.WriteLine(allStates2[target2]);
        }

        List<(string State, int Cost)> Move(string state)
        {
            var newStates = new List<(string, int)>();
            var validStops = new int[] { 0, 1, 3, 5, 7, 9, 10 };

            // Move top of room to bottom if in right room and possible
            foreach (var i in new int[] { 11, 13, 15, 17 })
            {
                if (state[i] == target[i] && state[i + 1] == '.')
                {
                    newStates.Add((Swap(state, i, i + 1), costs[state[i]]));
                }

                // move bottom of room to top if not in right room and possible
                if (state[i + 1] != '.' && state[i + 1] != target[i + 1] && state[i] == '.')
                {
                    newStates.Add((Swap(state, i, i + 1), costs[state[i + 1]]));
                }

                // move top to all possible hallway locations if not in right room
                if (state[i] != '.')
                {
                    var currentRoomEntry = i - 9;
                    // Walk to left
                    for (int j = currentRoomEntry - 1; j >= 0; j--)
                    {
                        if (state[j] != '.')
                            break;
                        if (state[j] == '.' && validStops.Contains(j))
                            newStates.Add((Swap(state, i, j), costs[state[i]] * (currentRoomEntry - j + 1)));
                    }
                    // Walk to right
                    for (int j = currentRoomEntry + 1; j <= 10; j++)
                    {
                        if (state[j] != '.')
                            break;
                        if (state[j] == '.' && validStops.Contains(j))
                            newStates.Add((Swap(state, i, j), costs[state[i]] * (j - currentRoomEntry + 1)));
                    }
                }
            }

            // Move hallway to right room if possible
            var targetRooms = new Dictionary<char, int>();
            targetRooms.Add('A', 11);
            targetRooms.Add('B', 13);
            targetRooms.Add('C', 15);
            targetRooms.Add('D', 17);
            foreach (var i in validStops)
            {
                if (state[i] != '.')
                {
                    // Check if target room has space at the top and does not contain incorrect items
                    if (state[targetRooms[state[i]]] == '.' && (state[targetRooms[state[i]] + 1] == state[i] || state[targetRooms[state[i]] + 1] == '.'))
                    {
                        // Check if there is a path
                        var targetRoomEntry = targetRooms[state[i]] - 9;
                        if (targetRoomEntry > i && state.Skip(i + 1).Take(targetRoomEntry - i - 1).All(c => c == '.'))
                            newStates.Add((Swap(state, i, targetRooms[state[i]]), costs[state[i]] * (targetRoomEntry - i + 1)));
                        if (targetRoomEntry < i && state.Skip(targetRoomEntry + 1).Take(i - targetRoomEntry - 1).All(c => c == '.'))
                            newStates.Add((Swap(state, i, targetRooms[state[i]]), costs[state[i]] * (i - targetRoomEntry + 1)));
                    }
                }
            }
            return newStates;
        }

        List<(string State, int Cost)> Move2(string state)
        {
            var newStates = new List<(string, int)>();
            var validStops = new int[] { 0, 1, 3, 5, 7, 9, 10 };
            var targetRooms = new Dictionary<char, int>();
            targetRooms.Add('A', 11);
            targetRooms.Add('B', 15);
            targetRooms.Add('C', 19);
            targetRooms.Add('D', 23);
            var roomEntries = new Dictionary<char, int>();
            roomEntries.Add('A', 2);
            roomEntries.Add('B', 4);
            roomEntries.Add('C', 6);
            roomEntries.Add('D', 8);

            foreach (var i in new int[] { 11, 15, 19, 23 })
            {
                // Move top of room towards bottom if in right room and possible
                if (state[i] == target2[i] && // Belongs in room
                    state.Substring(i + 1, 3).All(c => c == '.' || c == target2[i]))
                {
                    for (int j = 3; j > 0; j--)
                    {
                        if (state[i + j] == '.')
                        {
                            newStates.Add((Swap(state, i, i + j), costs[state[i]] * j));
                            break;
                        }
                    }
                }

                // move bottom of room towards top if some of the elements are not in the right room
                var roomOK = CheckRoom(state, i);
                if (!roomOK)
                {
                    if (state[i] == '.') // Top is empty
                    {
                        for (int j = 1; j < 4; j++) // Jump first one to top
                        {
                            if (state[i + j] != '.')
                            {
                                newStates.Add((Swap(state, i, i + j), costs[state[i + j]] * j));
                                break;
                            }
                        }
                    }
                }


                // move top to all possible hallway locations
                if (state[i] != '.')
                {
                    var currentRoomEntry = roomEntries[target2[i]];
                    // Walk to left
                    for (int j = currentRoomEntry - 1; j >= 0; j--)
                    {
                        if (state[j] != '.')
                            break;
                        if (state[j] == '.' && validStops.Contains(j))
                            newStates.Add((Swap(state, i, j), costs[state[i]] * (currentRoomEntry - j + 1)));
                    }
                    // Walk to right
                    for (int j = currentRoomEntry + 1; j <= 10; j++)
                    {
                        if (state[j] != '.')
                            break;
                        if (state[j] == '.' && validStops.Contains(j))
                            newStates.Add((Swap(state, i, j), costs[state[i]] * (j - currentRoomEntry + 1)));
                    }
                }
            }

            // Move hallway to right room if possible

            foreach (var i in validStops)
            {
                if (state[i] != '.')
                {
                    // Check if target room has space at the top and does not contain incorrect items
                    if (state[targetRooms[state[i]]] == '.' && CheckRoom(state, targetRooms[state[i]]))// (state[targetRooms[state[i]] + 1] == state[i] || state[targetRooms[state[i]] + 1] == '.'))
                    {
                        // Check if there is a path
                        var targetRoomEntry = roomEntries[state[i]];
                        if (targetRoomEntry > i && state.Skip(i + 1).Take(targetRoomEntry - i - 1).All(c => c == '.'))
                            newStates.Add((Swap(state, i, targetRooms[state[i]]), costs[state[i]] * (targetRoomEntry - i + 1)));
                        if (targetRoomEntry < i && state.Skip(targetRoomEntry + 1).Take(i - targetRoomEntry - 1).All(c => c == '.'))
                            newStates.Add((Swap(state, i, targetRooms[state[i]]), costs[state[i]] * (i - targetRoomEntry + 1)));
                    }
                }
            }
            return newStates;
        }

        bool CheckRoom(string state, int R)
        {
            return state.Substring(R, 4).All(c => c == '.' || c == target2[R]);
        }

        string Swap(string state, int i1, int i2)
        {
            if (state[i1] != '.' && state[i2] != '.')
            {
                Console.WriteLine("Invalid Swap");
            }
            var chars = state.ToCharArray();
            chars[i1] = state[i2];
            chars[i2] = state[i1];
            return new string(chars);
        }
    }
}
