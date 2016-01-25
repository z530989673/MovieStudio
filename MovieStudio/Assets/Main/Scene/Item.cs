using UnityEngine;
using System.Collections;

public class Item : SceneNode {

    public Item(GameObject parent) : base(parent) { }
    protected ItemData item;

    public void ResetItem(Pair pos, int ord = (int)ITEM_ITEM_ORDER.ITEM_ORDER_BACK, bool revert = false, int itemID = 0, Color? color = null)
    {
        item = GameManager.Instance.GetItemData(itemID);
        Reset(pos, ord, revert, item.spritePath, color);
    }
}
