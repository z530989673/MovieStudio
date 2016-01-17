using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {
    private RoomData roomData;
    public List<Item> items;
    public int level;

    private GameObject roomGO;

    public Room(GameObject Rooms)
    {
        roomGO = new GameObject();
        roomGO.transform.parent = Rooms.transform;

        items = new List<Item>();
    }

    public void ResetRoom(RoomData roomData, int level)
    {
        List<ItemData> itemData = GameManager.Instance.GetItemData();
        for(int i = 0; i < roomData.items[level].Count; i++)
        {
            Item item = new Item(roomGO);
            RoomItem roomItem = roomData.items[level][i];

            Pair pos = roomData.botRight + roomItem.offset;
            item.ResetItem(pos, 2, roomItem.isRevert, roomItem.ID, roomItem.color);

            items.Add(item);
        }
    }
}
