﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board {

    GameObject gameObject;

    private Cell[,] ground; //record the room id of each cell on the board
    private Item[,] leftWalls;
    private Item[,] rightWalls; 
    private List<Item> effects;
    public Pair size;

    /// <summary>
    /// init the board with size s
    /// </summary>
    /// <param name="s">size</param>
    public void Init(RoomData globalRoom, GameObject board)
    {
        effects = new List<Item>();

        size = globalRoom.size;
        gameObject = board;

        ground = new Cell[size.x, size.y];
        leftWalls = new Item[size.x + 1, size.y + 1];
        rightWalls = new Item[size.x + 1, size.y + 1];


        for(int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                ground[i, j] = new Cell(gameObject);


        for (int i = 0; i <= size.x; i++)
            for (int j = 0; j <= size.y; j++)
            {
                leftWalls[i, j] = new Item(board);
                rightWalls[i, j] = new Item(board);
            }

        for (int i = 0; i < size.y; i++)
            leftWalls[size.x, i].ResetItem(new Pair(size.x - 1, i), (int)ITEM_ITEM_ORDER.ITEM_ORDER_BACK, false, globalRoom.wallID, globalRoom.wallColor);

        for (int i = 0; i < size.x; i++)
            rightWalls[i, size.y].ResetItem(new Pair(i, size.y - 1), (int)ITEM_ITEM_ORDER.ITEM_ORDER_BACK, true, globalRoom.wallID, globalRoom.wallColor);
    }

    public void UpdateRoom(RoomData roomData, int level)
    {
        for(int i = 0; i < roomData.size.x; i++)
            for (int j = 0; j < roomData.size.y; j++)
            {
                int indexI = i + roomData.botRight.x;
                int indexJ = j + roomData.botRight.y;
                ground[indexI, indexJ].ResetCell(roomData, new Pair(i, j) + roomData.botRight, (int)ITEM_ITEM_ORDER.ITEM_ORDER_GROUND);
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

                ground[indexI, indexJ].UpdateDoorDir(roomData.doors[i].dir);
            }
    }

    /// <summary>
    /// should run after all rooms has been updated
    /// </summary>
    public void UpdateWall(List<RoomData> rooms)
    {
        for (int i = 1; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
            {
                if (ground[i, j].roomID != ground[i - 1, j].roomID &&
                    (ground[i, j].doorDir & GameConstant.DOOR_DIR_BOTRIGHT) == 0 &&
                    (ground[i - 1, j].doorDir & GameConstant.DOOR_DIR_TOPLEFT) == 0)
                {
                    int roomID = ground[i - 1, j].roomID;
                    if (roomID == 0)
                        roomID = ground[i, j].roomID;

                    leftWalls[i, j].ResetItem(new Pair(i - 1, j), (int)ITEM_ITEM_ORDER.ITEM_ORDER_BACK, false, rooms[roomID].wallID, rooms[roomID].wallColor);
                }
            }

        for (int i = 0; i < size.x; i++)
            for (int j = 1; j < size.y; j++)
            {
                if (ground[i, j].roomID != ground[i, j - 1].roomID &&
                    (ground[i, j].doorDir & GameConstant.DOOR_DIR_BOTLEFT) == 0 &&
                    (ground[i, j - 1].doorDir & GameConstant.DOOR_DIR_TOPRIGHT) == 0)
                {
                    int roomID = ground[i, j - 1].roomID;
                    if (roomID == 0)
                        roomID = ground[i, j].roomID;
                    rightWalls[i, j].ResetItem(new Pair(i, j - 1), (int)ITEM_ITEM_ORDER.ITEM_ORDER_BACK, true, rooms[roomID].wallID, rooms[roomID].wallColor);
                }
            }
    }
}
