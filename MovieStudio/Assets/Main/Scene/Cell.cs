using UnityEngine;
using System.Collections;

public class Cell : Item {

    public Cell(GameObject parent) : base(parent){}

    public int roomID;
    public DOOR_DIR doorDir = DOOR_DIR.NONE;
    public bool walkable = true;
}
