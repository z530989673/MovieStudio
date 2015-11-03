using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Pair
{
    int x;
    int y;
}

public class Room {
    public Pair topLeft;
    public Pair size;
    public Pair HandlePosOffset;
    public int level;
    public List<Item> effects;
}
