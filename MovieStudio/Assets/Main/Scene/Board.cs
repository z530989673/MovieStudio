using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class TileOrder
{
    public int order = -1;
    public bool isSingle = true;
    public Pair refPos = new Pair(-1,-1);
    public Pair size = new Pair(1,1);
    public bool XRef = false;
    public bool YRef = false;

    public void Reset()
    {
        order = 0;
        isSingle = true;
        refPos.x = -1;
        refPos.y = -1;
        size.x = 1;
        size.y = 1;
        XRef = false;
        YRef = false;
    }

    public void clearData()
    {
        order = 0;
        XRef = false;
        YRef = false;
    }
}

public class Board {

    GameObject gameObject;

    private Cell[,] tiles; //record the room id of each cell on the board
    private TileOrder[,] tilesOrder;
    private Item[,] leftWalls;
    private Item[,] rightWalls; 
    private List<Item> effects;
    public Pair size;
    Stack<Pair> stack;


    /// <summary>
    /// init the board with size s
    /// </summary>
    /// <param name="s">size</param>
    public void Init(RoomData globalRoom, GameObject board)
    {
        effects = new List<Item>();
        stack = new Stack<Pair>();

        size = globalRoom.size;
        gameObject = board;

        tiles = new Cell[size.x, size.y];
        leftWalls = new Item[size.x + 1, size.y + 1];
        rightWalls = new Item[size.x + 1, size.y + 1];
        tilesOrder = new TileOrder[size.x + 1, size.y + 1];

        for(int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                tiles[i, j] = new Cell(gameObject);


        for (int i = 0; i <= size.x; i++)
            for (int j = 0; j <= size.y; j++)
            {
                leftWalls[i, j] = new Item(board);
                rightWalls[i, j] = new Item(board);
                tilesOrder[i, j] = new TileOrder();
            }

        int globalRoomLevel = GameManager.Instance.GetRoomLevel(0);
        LevelItemData levelItemData = GameManager.Instance.GetLevelItemData(globalRoom.wallID);

        if (levelItemData == null)
            return;

        int wallID = levelItemData.getItemID(globalRoomLevel);
        ItemData itemData = GameManager.Instance.GetItemData(wallID);

        for (int i = 0; i < size.y; i++)
        {
            leftWalls[size.x, i].ResetItem(new Pair(size.x - 1, i), itemData, (int)ITEM_ORDER.ITEM_ORDER_BACK, false);
        }

        for (int i = 0; i < size.x; i++)
            rightWalls[i, size.y].ResetItem(new Pair(i, size.y - 1), itemData, (int)ITEM_ORDER.ITEM_ORDER_BACK, true);
    }

    public void UpdateRoom(RoomData roomData, int level)
    {
        for(int i = 0; i < roomData.size.x; i++)
            for (int j = 0; j < roomData.size.y; j++)
            {
                int indexI = i + roomData.botRight.x;
                int indexJ = j + roomData.botRight.y;
                tiles[indexI, indexJ].ResetCell(roomData, new Pair(i, j) + roomData.botRight, (int)ITEM_ORDER.ITEM_ORDER_FLOOR);
            }

        for(int i = 0; i < roomData.doors.Count; i++)
            for(int j = 0; j < roomData.doors[i].length; j++)
            {
                int indexI = roomData.botRight.x;
                int indexJ = roomData.botRight.y;

                if (roomData.doors[i].dir == GameConstant.DOOR_DIR_TOPLEFT)
                    indexI += roomData.size.x - 1;
                else if (roomData.doors[i].dir == GameConstant.DOOR_DIR_TOPRIGHT)
                    indexJ += roomData.size.y - 1;


                if (roomData.doors[i].dir == GameConstant.DOOR_DIR_BOTRIGHT || roomData.doors[i].dir == GameConstant.DOOR_DIR_TOPLEFT)
                    indexJ += j + roomData.doors[i].startIndex;
                else if (roomData.doors[i].dir == GameConstant.DOOR_DIR_BOTLEFT || roomData.doors[i].dir == GameConstant.DOOR_DIR_TOPRIGHT)
                    indexI += j + roomData.doors[i].startIndex;

                tiles[indexI, indexJ].UpdateDoor(roomData.doors[i].dir);
            }
    }

    public void Update()
    {
        for(int i = 0; i < size.x; i++)
            for(int j = 0; j < size.y; j++)
                tiles[i, j].Update();

        foreach (Item effect in effects)
            effect.Update();

        for(int i = 0; i <= size.x; i++)
            for(int j = 0; j <= size.y; j++)
            {
                leftWalls[i, j].Update();
                rightWalls[i, j].Update();
            }
    }

    private void ClearItemTilesOrder(ItemData itemData, Pair refPos, bool isRevert)
    {
        Pair itemSize = new Pair(itemData.size);
        if (isRevert)
        {
            itemSize.x = itemData.size.y;
            itemSize.y = itemData.size.x;
        }

        if (itemSize.x == 1 && itemSize.y == 1)
            return;

        for (int i = 0; i < itemSize.x; i++)
            for (int j = 0; j < itemSize.y; j++)
                tilesOrder[refPos.x + i, refPos.y + j].Reset();
    }

    private void UpdateItemTilesOrder(ItemData itemData, Pair refPos, bool isRevert)
    {
        Pair itemSize = new Pair(itemData.size);
        if (isRevert)
        {
            itemSize.x = itemData.size.y;
            itemSize.y = itemData.size.x;
        }

        if (itemSize.x == 1 && itemSize.y == 1)
            return;

        for (int j = 0; j < itemSize.x; j++)
            for (int k = 0; k < itemSize.y; k++)
            {
                tilesOrder[refPos.x + j, refPos.y + k].isSingle = false;
                tilesOrder[refPos.x + j, refPos.y + k].size = itemSize;
            }

        tilesOrder[refPos.x, refPos.y + itemSize.y - 1].refPos = refPos + itemSize + new Pair(-1, -1);
        tilesOrder[refPos.x + itemSize.x - 1, refPos.y].refPos = refPos + itemSize + new Pair(-1, -1);
    }

    public void UpdateRoomTilesOrder(RoomData roomData, int level)
    {
        if (level != 0)
        {
            for(int i = 0; i < roomData.staticItems.Count; i++)
            {
                LevelItemData levelItemData = GameManager.Instance.GetLevelItemData(roomData.staticItems[i].levelItemID);
                ItemData itemData = GameManager.Instance.GetItemData(levelItemData.getItemID(level - 1));
                Pair refPos = roomData.staticItems[i].offset + roomData.botRight + new Pair(1, 1);
                ClearItemTilesOrder(itemData, refPos, roomData.staticItems[i].isRevert);
            }

            if (roomData.dynamicItems.Count > level - 1)
            {
                for (int i = 0; i < roomData.dynamicItems[level - 1].Count; i++)
                {
                    ItemData itemData = GameManager.Instance.GetItemData(roomData.dynamicItems[level - 1][i].itemID);
                    Pair refPos = roomData.dynamicItems[level - 1][i].offset + roomData.botRight + new Pair(1, 1);
                    ClearItemTilesOrder(itemData, refPos, roomData.dynamicItems[level - 1][i].isRevert);
                }
            }
        }

        for (int i = 0; i < roomData.staticItems.Count; i++)
        {
            LevelItemData levelItemData = GameManager.Instance.GetLevelItemData(roomData.staticItems[i].levelItemID);
            ItemData itemData = GameManager.Instance.GetItemData(levelItemData.getItemID(level));
            Pair refPos = roomData.staticItems[i].offset + roomData.botRight + new Pair(1, 1);
            UpdateItemTilesOrder(itemData, refPos, roomData.staticItems[i].isRevert);
        }

        if (roomData.dynamicItems.Count > level)
        {
            for (int i = 0; i < roomData.dynamicItems[level].Count; i++)
            {
                ItemData itemData = GameManager.Instance.GetItemData(roomData.dynamicItems[level][i].itemID);
                Pair refPos = roomData.dynamicItems[level][i].offset + roomData.botRight + new Pair(1, 1);
                UpdateItemTilesOrder(itemData, refPos, roomData.dynamicItems[level][i].isRevert);
            }
        }
    }

    private void checkTile(Pair dir, Pair top)
    {
        Pair refPos = new Pair(0,0);
        Pair checkPos = top + dir;

        if (checkPos.x <= size.x && checkPos.y <= size.y)
        {
            refPos = checkPos;
            if (tilesOrder[checkPos.x, checkPos.y].refPos.x != -1
                    && tilesOrder[checkPos.x, checkPos.y].refPos.y != -1)
                refPos = tilesOrder[checkPos.x, checkPos.y].refPos;

            if (dir.x == 1)
                tilesOrder[refPos.x, refPos.y].XRef = true;
            else if (dir.y == 1)
                tilesOrder[refPos.x, refPos.y].YRef = true;

            if (tilesOrder[refPos.x, refPos.y].order > tilesOrder[top.x, top.y].order - 1)
                tilesOrder[refPos.x, refPos.y].order = tilesOrder[top.x, top.y].order - 1;
        }

        if ((refPos.x != 0 || refPos.y != 0)
            && tilesOrder[refPos.x, refPos.y].XRef && tilesOrder[refPos.x, refPos.y].YRef)
        {
            if (tilesOrder[refPos.x, refPos.y].isSingle
                ||
                (tilesOrder[checkPos.x, checkPos.y].refPos.x != -1
                && tilesOrder[checkPos.x, checkPos.y].refPos.y != -1))
                stack.Push(refPos);
        }
    }

    public void CalculateTileOrder()
    {
        for (int i = 0; i <= size.x; i++)
            for (int j = 0; j <= size.y; j++)
                tilesOrder[i, j].clearData();

        for (int i = 0; i <= size.x; i++)
            tilesOrder[i, 0].YRef = true;
        for (int i = 0; i <= size.y; i++)
            tilesOrder[0, i].XRef = true;

        stack.Clear();
        stack.Push(new Pair(0, 0));

        while(stack.Count != 0)
        {
            Pair top = stack.Pop();
            if (tilesOrder[top.x, top.y].isSingle)
            {
                checkTile(new Pair(1, 0), top);
                checkTile(new Pair(0, 1), top);
            }
            else
            {
                Pair itemSize = tilesOrder[top.x, top.y].size;
                for (int i = 0; i < itemSize.x; i++)
                    for (int j = 0; j < itemSize.y; j++)
                        tilesOrder[top.x - i, top.y - j].order = tilesOrder[top.x, top.y].order;

                for (int i = itemSize.x - 1; i >= 0; i--)
                    checkTile(new Pair(0, 1), top - new Pair(i, 0));
                for (int i = itemSize.y - 1; i >= 0; i--)
                    checkTile(new Pair(1, 0), top - new Pair(0, i));
            }
        }

        for (int i = 1; i <= size.x; i++)
            for (int j = 1; j <= size.y; j++)
                tilesOrder[i, j].order -= tilesOrder[size.x, size.y].order;
    }

    public int GetTileOrder(Pair pos)
    {
        return tilesOrder[pos.x + 1, pos.y + 1].order;
    }

    /// <summary>
    /// should run after all rooms has been updated
    /// </summary>
    public void  UpdateWall(List<RoomData> rooms)
    {
        for (int i = 1; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
            {
                if (tiles[i, j].roomID != tiles[i - 1, j].roomID &&
                    (tiles[i, j].doorDir & GameConstant.DOOR_DIR_BOTRIGHT) == 0 &&
                    (tiles[i - 1, j].doorDir & GameConstant.DOOR_DIR_TOPLEFT) == 0)
                {
                    int roomID = tiles[i - 1, j].roomID;
                    if (roomID == 0)
                        roomID = tiles[i, j].roomID;

                    int roomLevel = GameManager.Instance.GetRoomLevel(roomID);
                    LevelItemData levelItemData = GameManager.Instance.GetLevelItemData(rooms[roomID].wallID);

                    if (levelItemData == null)
                        continue;

                    int wallID = levelItemData.getItemID(roomLevel);
                    ItemData itemData = GameManager.Instance.GetItemData(wallID);

                    leftWalls[i, j].ResetItem(new Pair(i - 1, j), itemData, (int)ITEM_ORDER.ITEM_ORDER_BACK, false);
                }
            }

        for (int i = 0; i < size.x; i++)
            for (int j = 1; j < size.y; j++)
            {
                if (tiles[i, j].roomID != tiles[i, j - 1].roomID &&
                    (tiles[i, j].doorDir & GameConstant.DOOR_DIR_BOTLEFT) == 0 &&
                    (tiles[i, j - 1].doorDir & GameConstant.DOOR_DIR_TOPRIGHT) == 0)
                {
                    int roomID = tiles[i, j - 1].roomID;
                    if (roomID == 0)
                        roomID = tiles[i, j].roomID;

                    int roomLevel = GameManager.Instance.GetRoomLevel(roomID);
                    LevelItemData levelItemData = GameManager.Instance.GetLevelItemData(rooms[roomID].wallID);

                    if (levelItemData == null)
                        continue;

                    int wallID = levelItemData.getItemID(roomLevel);
                    ItemData itemData = GameManager.Instance.GetItemData(wallID);

                    rightWalls[i, j].ResetItem(new Pair(i, j - 1), itemData, (int)ITEM_ORDER.ITEM_ORDER_BACK, true);
                }
            }
    }
}
