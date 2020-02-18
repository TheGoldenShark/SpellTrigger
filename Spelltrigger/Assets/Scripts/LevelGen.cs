using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGen : MonoBehaviour
{
	//Constructor, with various objects being initialised
	// Reference to the room prefabs shown above
	public List<GameObject> roomPrefabs;
	public GameObject enemyPrefab;
	public int levelSize;
	public List<GameObject> bossPrefabs;
	public List<GameObject> itemPrefabs;

	// The scale with which coordinates are multiplied by to be correctly positioned in the level
	GameManager gameManager;
	float placeScale = 5.4f;
	float offset = 0.8f;
	Dictionary<string, GameObject> roomPrefabsDict = new Dictionary<string, GameObject> { };
	List<List<string>> levelGrid;
	static System.Random rnd = new System.Random();
	//List of the possible rooms that can be placed in a given direction.
	//Rooms are stored as a string in the format NESW, where '0' represents a wall and '1' represents a door
	List<string> NRooms = new List<string> { "1000", "1001", "1010", "1011", "1100", "1101", "1110" };
	List<string> SRooms = new List<string> { "0010", "0011", "0110", "0111", "1010", "1011", "1110" };
	List<string> WRooms = new List<string> { "0001", "0011", "0101", "0111", "1001", "1011", "1101" };
	List<string> ERooms = new List<string> { "0100", "0101", "0110", "0111", "1100", "1101", "1110" };
	List<List<int>> roomStack = new List<List<int>>();
	// A list of rooms that have not already had an object placed in them
	private List<List<int>> regularRoomList = new List<List<int>>();
	private bool placed = false;
	private Vector2 startPoint;
	private Transform playerTransform;
	// Start is called before the first frame update
	void Start()
	//Runs when the game starts. Generates the level, and then places the corresponding rooms.
	{
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level"));
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		playerTransform = GameObject.Find("Player").transform;
		startPoint = new Vector2((levelSize - 1) / 2, (levelSize - 1) / 2) * placeScale;
		generate();
		createroomPrefabsDict();
		closeLevel();
		levelPlace();
		regularRoomList.RemoveAt(0);
		addSpecialRoom(bossPrefabs);
		addSpecialRoom(itemPrefabs);
		enemyPlace();
		playerTransform.position = startPoint;
	}

	// Update is called once per frame
	void Update()
	{

	}

	// Adds a random room from the list it is given to the furthest possible room from the start distance that has not already been used for something
	void addSpecialRoom(List<GameObject> prefabs)
	{
		// Uses maxDist to get the furthest location in the regularRoomList
		List<int> location = maxDist(regularRoomList);
		// Finds the corresponding vector2 and places a random prefab from the given list at that coordinate
		Vector2 newCoord = new Vector2(location[1] * placeScale, ((levelSize - 1) - location[0]) * placeScale);
		Instantiate(prefabs[rnd.Next(prefabs.Count)], newCoord, Quaternion.identity);

	}

	// Finds the coordinate with the furthest distance from the start in the list of coordinates given, removes and returns that coordinate
	List<int> maxDist(List<List<int>> rooms)
	{
		// Various variables that are needed in this function
		List<int> maxPos = new List<int> { 0, 0 };
		int maxMagnitude = 0;
		int magnitude = 0;
		// Iterates through each item in the rooms list, calculating the magnitude of each one and comparing it to the maximum magnitude found so far
		// If one with a greater magnitude is found the max is overwritten and location is recorded
		// This is repeated until the whole list has been iterated through
		foreach (List<int> location in rooms)
		{
			magnitude = (location[0] - (levelSize - 1) / 2) * (location[0] - (levelSize - 1) / 2) + (location[1] - (levelSize - 1) / 2) * (location[1] - (levelSize - 1) / 2);
			if (maxMagnitude < magnitude)
			{
				maxMagnitude = magnitude;
				maxPos = location;
			}
		}
		// Removes the location discovered from the original list, so it won't be found the next time maxDist is used
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
	// For all locations in the regularRoomList it places an enemy
	{
		foreach (var i in regularRoomList)
		{
			for (int j = 0; j < gameManager.difficulty; j++)
			{
				Vector2 offsetVector = new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2 * offset, 2 * offset));
				Vector2 newCoord = new Vector2(i[1] * placeScale, ((levelSize - 1) - i[0]) * placeScale);
				Instantiate(enemyPrefab, newCoord + offsetVector, Quaternion.identity);
			}
		}
	}

	void levelPlace()
	//Loops through the final level grid, placing the room prefabs correspondingly.
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
		// Ensures the player cannot escape the level
	{
		for (int i = 0; i < levelSize-1; i++)
		{
			// West side
			// Uses stringbuilder to turn the room string into a temporary list variable, so each character can be edited
			StringBuilder sb = new StringBuilder(levelGrid[i][0]);
			// Closes the relevant door that allows the player to escape the level
			sb[3] = '0';
			// Turns it back to a string, and overwrites the original room string that was copied
			levelGrid[i][0] = sb.ToString();
			// East side
			sb = new StringBuilder(levelGrid[i][levelGrid.Count - 1]);
			sb[1] = '0';
			levelGrid[i][levelGrid.Count - 1] = sb.ToString();
			// North side
			sb = new StringBuilder(levelGrid[0][i]);
			sb[0] = '0';
			levelGrid[0][i] = sb.ToString();
			// West side
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