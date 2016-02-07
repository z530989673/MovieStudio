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

    public void ResetRoom(RoomData roomData)
    {
        int level = GameManager.Instance.GetRoomLevel(roomData.ID);

        for (int i = 0; i < roomData.doors.Count; i++)
        {
            LevelItemData levelItemData = GameManager.Instance.GetLevelItemData(roomData.doors[i].levelItemID);
            if (levelItemData == null)
                continue;
             
            int itemID = levelItemData.getItemID(level);
            ItemData itemData = GameManager.Instance.GetItemData(itemID);

            if (itemData != null)
            {
                Item item = new Item(roomGO);
                Pair pos = new Pair(roomData.botRight);
                if (roomData.doors[i].dir == GameConstant.DOOR_DIR_TOPLEFT)
                    pos.x += roomData.size.x - 1;
                else if (roomData.doors[i].dir == GameConstant.DOOR_DIR_TOPRIGHT)
                    pos.y += roomData.size.y - 1;
                if (roomData.doors[i].dir == GameConstant.DOOR_DIR_BOTLEFT || roomData.doors[i].dir == GameConstant.DOOR_DIR_TOPRIGHT)
                    pos.x += roomData.doors[i].startIndex;
                else if (roomData.doors[i].dir == GameConstant.DOOR_DIR_BOTRIGHT || roomData.doors[i].dir == GameConstant.DOOR_DIR_TOPLEFT)
                    pos.y += roomData.doors[i].startIndex;

                bool isRevert = false;
                if (roomData.doors[i].dir == GameConstant.DOOR_DIR_BOTLEFT || roomData.doors[i].dir == GameConstant.DOOR_DIR_TOPRIGHT)
                    isRevert = true;

                if (roomData.doors[i].dir == GameConstant.DOOR_DIR_BOTRIGHT)
                    pos.x--;
                else if (roomData.doors[i].dir == GameConstant.DOOR_DIR_BOTLEFT)
                    pos.y--;

                item.ResetItem(pos, itemData, (int)ITEM_ORDER.ITEM_ORDER_BACK, isRevert);

                items.Add(item);
            }
        }

        for (int i = 0; i < roomData.staticItems.Count; i++)
        {
            StaticLevelItem staticLevelItem = roomData.staticItems[i];
            LevelItemData levelItemData = GameManager.Instance.GetLevelItemData(roomData.staticItems[i].levelItemID);

            if (levelItemData == null)
                continue;

            int itemID = levelItemData.getItemID(level);
            ItemData itemData = GameManager.Instance.GetItemData(itemID);

            if (itemData != null)
            {
                Item item = new Item(roomGO);
                Pair pos = roomData.botRight + staticLevelItem.offset;
                item.ResetItem(pos, itemData, staticLevelItem.order, staticLevelItem.isRevert);

                items.Add(item);
            }
        }

        if (roomData.dynamicItems.Count > level)
        {
            for (int i = 0; i < roomData.dynamicItems[level].Count; i++)
            {
                DynamicLevelItem levelItem = roomData.dynamicItems[level][i];
                ItemData itemData = GameManager.Instance.GetItemData(levelItem.itemID);

                if (itemData != null)
                {
                    Item item = new Item(roomGO);
                    Pair pos = roomData.botRight + levelItem.offset;
                    item.ResetItem(pos, itemData, levelItem.order, levelItem.isRevert);

                    items.Add(item);
                }
            }
        }
    }
}
