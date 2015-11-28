using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board {

    private Cell[,] ground; //record the room id of each cell on the board
    private List<Item> effects;
    public Pair size;

    /// <summary>
    /// init the board with size s
    /// </summary>
    /// <param name="s">size</param>
    public void Init(Pair s)
    {
        size = s;
        ground = new Cell[size.x,size.y];
        for(int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
            {
                ground[i, j] = new Cell();
                ground[i, j].Reset(new Pair(i, j));
            }
    }

    public void Update(Room room)
    {

    }
}
