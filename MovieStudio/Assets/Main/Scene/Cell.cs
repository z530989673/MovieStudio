using UnityEngine;
using System.Collections;

public class Cell : Item {

    public Cell(GameObject parent) : base(parent){}

    public void ResetCell(RoomData roomData, Pair pos, int ord = 0)
    {
        room = roomData;
        roomID = room.ID;
        ResetItem(pos, ord, false, room.boardID, room.boardColor);
    }

    public int roomID;
    public DOOR_DIR doorDir = DOOR_DIR.NONE;
    public bool walkable = true;

    protected RoomData room;
}
