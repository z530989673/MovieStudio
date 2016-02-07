using UnityEngine;
using System.Collections;

public class Cell : Item {

    public Cell(GameObject parent) : base(parent){}

    public void ResetCell(RoomData roomData, Pair pos, int ord = 0)
    {
        room = roomData;
        roomID = room.ID;

        int roomLevel = GameManager.Instance.GetRoomLevel(roomID);
        LevelItemData levelItemData = GameManager.Instance.GetLevelItemData(roomData.boardID);

        if (levelItemData == null)
            return;

        int boardID = levelItemData.getItemID(roomLevel);
        ItemData itemData = GameManager.Instance.GetItemData(boardID);

        ResetItem(pos, itemData, ord, false);
    }

    public void UpdateDoor(int dir)
    {
        doorDir |= (int)dir;
    }

    public int roomID;
    public int doorDir = GameConstant.DOOR_DIR_NONE;
    public bool walkable = true;

    protected RoomData room;
}
