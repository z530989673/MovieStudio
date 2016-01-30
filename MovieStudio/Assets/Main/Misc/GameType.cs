using UnityEngine;
using System.Collections;

public class Pair
{
    public int x = -1;
    public int y = -1;
    public Pair(int intX, int intY)
    {
        this.x = intX;
        this.y = intY;
    }

    public static Pair operator +(Pair value1, Pair value2)
    {
        return new Pair(value1.x + value2.x, value1.y + value2.y);
    }
}