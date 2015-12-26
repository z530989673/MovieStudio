using UnityEngine;
using System.Collections;

public class Item : SceneNode {

    public Item(GameObject parent) : base(parent) { }
    private ItemData item;

    public void ResetItem(Pair pos, int ord = 0, bool revert = false, int itemID = 0)
    {
        item = GameManager.Instance.GetItemData(itemID);
        Reset(pos, ord, revert, item.spritePath);
    }
}
