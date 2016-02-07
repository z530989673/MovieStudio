using UnityEngine;
using System.Collections;

public class Item : SceneNode {

    public Item(GameObject parent) : base(parent) { }
    protected ItemData item;

    public void ResetItem(Pair pos,ItemData itemData, int ord = (int)ITEM_ORDER.ITEM_ORDER_BACK, bool revert = false)
    {
        if (itemData != null)
        {
            item = itemData;
            Reset(pos, ord, revert, item.spritePath, item.color);
        }
    }
}
