using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct RoomItem
{
    public int ID;
    public Pair offset;
    public bool isRevert;
    public Color color;
}

public class RoomData {
    public int ID = -1;
    public string name = "";
    public Pair botRight;
    public Pair size;
    public Pair handlePosOffset;
    public Pair doorPosOffset;
    public DOOR_DIR doorDir = DOOR_DIR.NONE;
    public bool hasWall = true;
    public int boardID;
    public Color boardColor;
    public List<List<RoomItem>> items;
    public List<int> unlockCost;
}
