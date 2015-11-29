using UnityEngine;
using System.Collections;

public class Item : SceneNode {

    public Item(GameObject parent) : base(parent) { }
    private ItemData item;
    public Pair offset;
    public int order = 1;
}
