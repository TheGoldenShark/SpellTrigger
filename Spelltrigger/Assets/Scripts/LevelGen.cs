using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class LevelGen : MonoBehaviour
{

    public List<GameObject> roomPrefabs;
    public GameObject enemyPrefab;
    public int levelSize;
    public Transform playerTransform;

    float placeScale = 5.4f;
    Dictionary<string, GameObject> roomPrefabsDict = new Dictionary<string, GameObject> { };
    List<List<string>> levelGrid;
    static System.Random rnd = new System.Random();
    List<string> NRooms = new List<string> { "1000", "1001", "1010", "1011", "1100", "1101", "1110" };
    List<string> SRooms = new List<string> { "0010", "0011", "0110", "0111", "1010", "1011", "1110" };
    List<string> WRooms = new List<string> { "0001", "0011", "0101", "0111", "1001", "1011", "1101" };
    List<string> ERooms = new List<string> { "0100", "0101", "0110", "0111", "1100", "1101", "1110" };
    List<List<int>> roomStack = new List<List<int>>();
    bool placed = false;
    Vector2 startPoint;
    Vector2 newCoord;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = new Vector2((levelSize - 1) / 2, (levelSize - 1) / 2) * placeScale;
        generate();
        createroomPrefabsDict();
        closeLevel();
        enemyPlace();
        levelPlace();
        playerTransform.Translate(startPoint);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void addRoom(List<int> location, List<string> doors)
    {
        levelGrid[location[0]][location[1]] = doors[rnd.Next(doors.Count)];
        roomStack.Add(location);
    }

    void enemyPlace()
    {
        for (int i = 0; i < levelGrid.Count; i++)
        {
            for (int j = 0; j < levelGrid[i].Count; j++)
            {
                if (i!=7 && j!=7)
                {
                    Vector2 newCoord = new Vector2(j * placeScale, ((levelSize - 1) - i) * placeScale);
                    if (newCoord != startPoint)
                    {
                        Instantiate(enemyPrefab, newCoord, Quaternion.identity);
                    }
                }
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
        levelGrid = (Enumerable.Range(0, levelSize).Select(i => (Enumerable.Range(0, levelSize).Select(j => "0000")).ToList())).ToList();
        List<int> start = new List<int> { (levelSize - 1) / 2, (levelSize - 1) / 2 };
        addRoom(start, new List<string> { "1111" });

        while (true)
        {
            placed = false;
            //North
            if (roomStack.Last()[0] - 1 >= 0 && placed == false)
            {
                if (levelGrid[roomStack.Last()[0] - 1][roomStack.Last()[1]] == "0000" && (levelGrid[roomStack.Last()[0]][roomStack.Last()[1]])[0] == '1')
                {
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
            if (placed == false)
            {
                if (roomStack.Last() != start)
                {
                    roomStack.RemoveAt(roomStack.Count - 1);
                }
                else break;
            }
        }
    }
}
