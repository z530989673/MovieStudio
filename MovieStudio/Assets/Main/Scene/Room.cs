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
        for(int i = 0; i < roomData.itemIDs[level].Count; i++)
        {
            Item item = new Item(roomGO);

            Pair pos = roomData.botRight + roomData.itemsOffset[level][i];
            item.Reset(pos, 2, roomData.itemsRevert[level][i], itemData[roomData.itemIDs[level][i]].spritePath);

            items.Add(item);
        }
    }
}
