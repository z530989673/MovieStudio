using UnityEngine;
using System.Collections;

public class Pair
{
    public int x = -1;
    public int y = -1;
}

public enum DOOR_DIR
{
    NONE = 0x0000,
    LEFT = 0x0001,
    RIGHT = 0x0010,
    UP = 0x0100,
    DOWN = 0x1000
}