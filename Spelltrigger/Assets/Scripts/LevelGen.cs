using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class LevelGen : MonoBehaviour
{
	//Constructor, with various objects being initialised
	// Reference to the room prefabs shown above
	public List<GameObject> roomPrefabs;
	public GameObject enemyPrefab;
	public int levelSize;
	public Transform playerTransform;
	public List<GameObject> bossPrefabs;
	public List<GameObject> itemPrefabs;

	float placeScale = 5.4f;
	Dictionary<string, GameObject> roomPrefabsDict = new Dictionary<string, GameObject> { };
	List<List<string>> levelGrid;
	static System.Random rnd = new System.Random();
	//List of the possible rooms that can be placed in a given direction.
	List<string> NRooms = new List<string> { "1000", "1001", "1010", "1011", "1100", "1101", "1110" };
	List<string> SRooms = new List<string> { "0010", "0011", "0110", "0111", "1010", "1011", "1110" };
	List<string> WRooms = new List<string> { "0001", "0011", "0101", "0111", "1001", "1011", "1101" };
	List<string> ERooms = new List<string> { "0100", "0101", "0110", "0111", "1100", "1101", "1110" };
	List<List<int>> roomStack = new List<List<int>>();
	List<List<int>> regularRoomList = new List<List<int>>();
	bool placed = false;
	Vector2 startPoint;
	Vector2 newCoord;
	// Start is called before the first frame update
	void Start()
	//Runs when the game starts. Generates the level, and then places the corresponding rooms.
	{
		startPoint = new Vector2((levelSize - 1) / 2, (levelSize - 1) / 2) * placeScale;
		generate();
		createroomPrefabsDict();
		closeLevel();
		levelPlace();
		regularRoomList.Remove(new List<int> { levelSize, levelSize });
		addSpecialRoom(bossPrefabs);
		addSpecialRoom(itemPrefabs);
		enemyPlace();
		playerTransform.Translate(startPoint);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void addSpecialRoom(List<GameObject> prefabs)
	{
		List<int> location = maxDist(regularRoomList);
		Vector2 newCoord = new Vector2(location[1] * placeScale, ((levelSize - 1) - location[0]) * placeScale);
		Instantiate(prefabs[rnd.Next(prefabs.Count)], newCoord, Quaternion.identity);

	}

	List<int> maxDist(List<List<int>> rooms)
	{
		List<int> maxPos = new List<int> { 0, 0 };
		int maxMagnitude = 0;
		int magnitude = 0;
		foreach (List<int> location in rooms)
		{
			magnitude = (location[0] - (levelSize - 1) / 2) * (location[0] - (levelSize - 1) / 2) + (location[1] - (levelSize - 1) / 2) * (location[1] - (levelSize - 1) / 2);
			if (maxMagnitude < magnitude)
			{
				maxMagnitude = magnitude;
				maxPos = location;
			}
		}
		rooms.Remove(maxPos);
		return maxPos;
	}

	void addRoom(List<int> location, List<string> doors)
	//Takes a location and a list of possible rooms, then places a random room at that location. It also adds the location to the stack.
	{
		levelGrid[location[0]][location[1]] = doors[rnd.Next(doors.Count)];
		roomStack.Add(location);
		regularRoomList.Add(location);
	}

	void enemyPlace()
	//Loops through the final level grid, placing the room prefabs correspondingly.
	{
		foreach (var i in regularRoomList)
		{
			Vector2 newCoord = new Vector2(i[1] * placeScale, ((levelSize - 1) - i[0]) * placeScale);
			if (newCoord != startPoint && levelGrid[i[0]][i[1]] != "0000")
			{
				Instantiate(enemyPrefab, newCoord, Quaternion.identity);
			}
		}
	}

	void levelPlace()
	{
		for (int i=0; i < levelGrid.Count; i++)
		{
			for (int j=0; j < levelGrid[i].Count; j++)
			{
				Vector2 newCoord = new Vector2(j * placeScale, ((levelSize - 1) - i) * placeScale);
				Instantiate(roomPrefabsDict[levelGrid[i][j]], newCoord, Quaternion.identity);
			}
		}
	}

	void createroomPrefabsDict()
	//Creates a dictionary mapping the string of numbers representing a room to the corresponding room gameObject
	{
		for (int i=1; i<17; i++)
		{
			roomPrefabsDict.Add((Convert.ToString(i-1, 2)).PadLeft(4, '0'), roomPrefabs[i-1]);
		}
	}

	// Level generates with open walls due to not checking outside of list - This closes walls around the edge
	// Could possibly be written more neatly with object initialisers
	void closeLevel()
	{
		for (int i = 0; i < levelSize-1; i++)
		{
			StringBuilder sb = new StringBuilder(levelGrid[i][0]);
			sb[3] = '0';
			levelGrid[i][0] = sb.ToString();
			sb = new StringBuilder(levelGrid[i][levelGrid.Count - 1]);
			sb[1] = '0';
			levelGrid[i][levelGrid.Count - 1] = sb.ToString();
			sb = new StringBuilder(levelGrid[0][i]);
			sb[0] = '0';
			levelGrid[0][i] = sb.ToString();
			sb = new StringBuilder(levelGrid[levelGrid.Count - 1][i]);
			sb[2] = '0';
			levelGrid[levelGrid.Count - 1][i] = sb.ToString();
		}
	}
	void generate()
	{
		// Creates an empty levelgrid with an width and height of the levelsize variable
		levelGrid = (Enumerable.Range(0, levelSize).Select(i => (Enumerable.Range(0, levelSize).Select(j => "0000")).ToList())).ToList();
		List<int> start = new List<int> { (levelSize - 1) / 2, (levelSize - 1) / 2 };
		// Adds the starting room
		addRoom(start, new List<string> { "1111" });

		while (true)
		{
			placed = false;
			//North
			// Checks that this is a valid position in the current level size
			if (roomStack.Last()[0] - 1 >= 0 && placed == false)
			{
				// Checks that no other room has been placed in that location and if the last placed room has a door in this direction
				if (levelGrid[roomStack.Last()[0] - 1][roomStack.Last()[1]] == "0000" && (levelGrid[roomStack.Last()[0]][roomStack.Last()[1]])[0] == '1')
				{
					// Adds the room with the corresponding room list
					addRoom(new List<int> { roomStack.Last()[0] - 1, roomStack.Last()[1] }, SRooms);
					placed = true;
				}
			}
			//East
			if (roomStack.Last()[1] + 1 < levelSize - 1 && placed == false)
			{
				if (levelGrid[roomStack.Last()[0]][roomStack.Last()[1] + 1] == "0000" && (levelGrid[roomStack.Last()[0]][roomStack.Last()[1]])[1] == '1')
				{
					addRoom(new List<int> { roomStack.Last()[0], roomStack.Last()[1] + 1 }, WRooms);
					placed = true;
				}
			}
			//South
			if (roomStack.Last()[0] + 1 < levelSize - 1 && placed == false)
			{
				if (levelGrid[roomStack.Last()[0] + 1][roomStack.Last()[1]] == "0000" && (levelGrid[roomStack.Last()[0]][roomStack.Last()[1]])[2] == '1')
				{
					addRoom(new List<int> { roomStack.Last()[0] + 1, roomStack.Last()[1] }, NRooms);
					placed = true;
				}
			}
			//West
			if (roomStack.Last()[1] - 1 >= 0 && placed == false)
			{
				if (levelGrid[roomStack.Last()[0]][roomStack.Last()[1] - 1] == "0000" && (levelGrid[roomStack.Last()[0]][roomStack.Last()[1]])[3] == '1')
				{
					addRoom(new List<int> { roomStack.Last()[0], roomStack.Last()[1] - 1 }, ERooms);
					placed = true;
				}
			}
			// Runs if no room can be placed
			if (placed == false)
			{
				if (roomStack.Last() != start)
				{
					// Goes back one in the level stack
					roomStack.RemoveAt(roomStack.Count - 1);
				}
				// If the level generation has returned to the beginning, no more rooms can be placed and level generation is over
				else break;
			}
		}
	}
}
