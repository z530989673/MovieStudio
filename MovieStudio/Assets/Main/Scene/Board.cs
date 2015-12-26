using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board {

    GameObject gameObject;

    private Cell[,] ground; //record the room id of each cell on the board
    private List<Item> effects;
    private List<Item> walls;
    public Pair size;

    /// <summary>
    /// init the board with size s
    /// </summary>
    /// <param name="s">size</param>
    public void Init(Pair s, GameObject board)
    {
        effects = new List<Item>();
        walls = new List<Item>();

        gameObject = board;

        size = s;
        ground = new Cell[size.x,size.y];
        for(int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                ground[i, j] = new Cell(gameObject);

        for (int i = 0; i < size.x; i++)
        {
            Item wall = new Item(board);
            wall.ResetItem(new Pair(i, size.y - 1), 1, true, 2);
            walls.Add(wall);
        }

        for (int i = 0; i < size.y; i++)
        {
            Item wall = new Item(board);
            wall.ResetItem(new Pair(size.x - 1, i), 1, false, 2);
            walls.Add(wall);
        }
    }

    public void UpdateRoom(RoomData roomData, int level)
    {
        for(int i = 0; i < roomData.size.x; i++)
            for (int j = 0; j < roomData.size.y; j++)
            {
                int indexI = i + roomData.botRight.x;
                int indexJ = j + roomData.botRight.y;
                ground[indexI, indexJ].ResetItem(new Pair(i, j) + roomData.botRight, 1, false, roomData.boardID);
            }
    }
}
