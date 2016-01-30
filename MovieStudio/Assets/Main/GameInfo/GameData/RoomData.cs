using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Door
{
    public Pair offset;
    public int length;
    public int dir; 
}

public struct RoomItem
{
    public int ID;
    public int order;
    public Pair offset;
    public bool isRevert;
    public Color color;
}

public class RoomData {
    public int ID = -1;
    public Pair botRight;
    public Pair size;
    public Pair handlePosOffset;
    public bool hasWall = true;
    public int wallID;
    public Color wallColor;
    public int boardID;
    public Color boardColor;
    public List<Door> doors;
    public List<List<RoomItem>> items;
    public List<int> unlockCost;
}
