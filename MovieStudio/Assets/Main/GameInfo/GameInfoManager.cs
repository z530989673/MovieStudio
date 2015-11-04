using UnityEngine;
using System.Collections;

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

	public class TestJsonClass
	{
		public int mId;
		public string mTitle;
		public int mTime;
		public float mFloat;
	}

	// Use this for initialization
	void Start () {
        gameInfo = new GameInfo();
        playerInfo = new PlayerInfo();

		// For Basic Json parser functions :)
		TestJsonClass test = new TestJsonClass();
		test.mId = 2;
		test.mTime = 100;
		test.mTitle = "title";
		test.mFloat = 0.5f;
		Debug.Log(Util.Serialize( typeof(TestJsonClass), test));

		TestJsonClass test2 = Util.Deserialize<TestJsonClass>(Resources.Load<TextAsset>("Data/test").ToString());
		Debug.Log("mid = " + test2.mId);
		Debug.Log("mTitle = " + test2.mTitle);
		Debug.Log("mTime = " + test2.mTime);
		Debug.Log("mFloat = " + test2.mFloat);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
