using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;  

public class Door
{
    public int startIndex;
    public int length;
    public int dir;
    public int levelItemID = -1;
}

public struct StaticLevelItem
{
    public int levelItemID;
    public int order;
    public Pair offset;
    public bool isRevert;
}
public struct DynamicLevelItem
{
    public int itemID;
    public int order;
    public Pair offset;
    public bool isRevert;
}

public class RoomData
{
    public int ID = -1;
    public Pair botRight;
    public Pair size;
    public Pair handlePosOffset;
    public bool hasWall = true;
    public int boardID;
    public int wallID;
    public List<Door> doors;
    public List<StaticLevelItem> staticItems;
    public List<List<DynamicLevelItem>> dynamicItems;
    public List<int> unlockCost;
}

public class LevelItemData
{
    public int ID = -1;
    public int startLevel;
    public int[] itemIDs;

    public int getItemID(int currentLevel)
    {
        if (startLevel > currentLevel)
            return -1;
        else
        {
            int level = currentLevel - startLevel;
            if (level >= itemIDs.Length)
                level = itemIDs.Length - 1;

            return itemIDs[level];
        }
    }
}
