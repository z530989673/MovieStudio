using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfoManager : MonoBehaviour {

    private static GameInfoManager m_instance;
    private GameInfoManager() { }

    public static GameInfoManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = GameObject.FindObjectOfType(typeof(GameInfoManager)) as GameInfoManager;
            return m_instance;
        }
    }

    private GameInfo gameInfo;
    private PlayerInfo playerInfo;
    private GameData gameData;

    public GameInfo getGameInfo() { return gameInfo; }
    public PlayerInfo getPlayerInfo() { return playerInfo; }
    public GameData getGameData() { return gameData; }

    public List<RoomData> GetRoomData() { return gameData.roomData; }
    public List<LevelItemData> GetLevelItemData() { return gameData.levelItemData; }
    public List<ItemData> GetItemData() { return gameData.itemData; }

    public RoomData GetRoomData(int dataID)
    {
        if (dataID < 0 || dataID >= gameData.roomData.Count)
            return null;
        return gameData.roomData[dataID]; 
    }
    public LevelItemData GetLevelItemData(int dataID)
    {
        if (dataID < 0 || dataID >= gameData.levelItemData.Count)
            return null;
        return gameData.levelItemData[dataID]; 
    }
    public ItemData GetItemData(int dataID)
    {
        if (dataID < 0 || dataID >= gameData.itemData.Count)
            return null;
        return gameData.itemData[dataID]; 
    }

    public int GetRoomLevel(int roomID)
    {
        if (gameInfo.roomLevel.Length < roomID)
            Debug.LogError("wrong roomID");
        else
            return gameInfo.roomLevel[roomID];

        return -1;
    }

	// Use this for initialization
	void Start () {
        gameInfo = new GameInfo();
        playerInfo = new PlayerInfo();
        gameData = new GameData();

        //// For Basic Json parser functions :)
        //TestJsonClass test = new TestJsonClass();
        //test.mId = 2;
        //test.mTime = 100;
        //test.mTitle = "title";
        //test.mFloat = 0.5f;
        //Debug.Log(Util.Serialize( typeof(TestJsonClass), test));

        //TestJsonClasses test2 = Util.Deserialize<TestJsonClasses>(ResourceManager.Instance.GetResourceTextAsset("Data/test").ToString());

	}
	
    public void Init()
    {
        gameData.roomData = Util.Deserialize<List<RoomData>>(GameManager.Instance.GetResourceTextAsset("Data/GameData/Room").ToString());
        gameData.levelItemData = Util.Deserialize<List<LevelItemData>>(GameManager.Instance.GetResourceTextAsset("Data/GameData/LevelItem").ToString());
        gameData.itemData = Util.Deserialize<List<ItemData>>(GameManager.Instance.GetResourceTextAsset("Data/GameData/Item").ToString());
    }

	// Update is called once per frame
	void Update () {
	
	}
}
