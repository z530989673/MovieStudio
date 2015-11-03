using UnityEngine;
using System.Collections;

public enum DOOR_DIR
{
    NONE = 0x0000,
    LEFT = 0x0001,
    RIGHT = 0x0010,
    UP = 0x0100,
    DOWN = 0x1000
}

public class Cell : SceneNode {
    public int roomID;
    public DOOR_DIR doorDir = DOOR_DIR.NONE;
    public bool walkable = true;
}
