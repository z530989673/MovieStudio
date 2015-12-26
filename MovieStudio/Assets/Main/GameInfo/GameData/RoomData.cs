using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public List<List<int>> itemIDs;
    public List<List<Pair>> itemsOffset;
    public List<List<bool>> itemsRevert;
    public List<int> unlockCost;

}
