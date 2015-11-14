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

    public GameInfo getGameInfo() { return gameInfo; }
    public PlayerInfo getPlayerInfo() { return playerInfo; }

    //public class TestJsonClasses
    //{
    //    public List<TestJsonClass> asd;
    //}

    //public class TestJsonClass
    //{
    //    public int mId;
    //    public string mTitle;
    //    public int mTime;
    //    public float mFloat;
    //}

	// Use this for initialization
	void Start () {
        gameInfo = new GameInfo();
        playerInfo = new PlayerInfo();

        //// For Basic Json parser functions :)
        //TestJsonClass test = new TestJsonClass();
        //test.mId = 2;
        //test.mTime = 100;
        //test.mTitle = "title";
        //test.mFloat = 0.5f;
        //Debug.Log(Util.Serialize( typeof(TestJsonClass), test));

        //TestJsonClasses test2 = Util.Deserialize<TestJsonClasses>(ResourceManager.Instance.GetResourceTextAsset("Data/test").ToString());
        //foreach (TestJsonClass test in test2.asd)
        //{
        //    Debug.Log("mid = " + test.mId);
        //    Debug.Log("mTitle = " + test.mTitle);
        //    Debug.Log("mTime = " + test.mTime);
        //    Debug.Log("mFloat = " + test.mFloat);
        //}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
