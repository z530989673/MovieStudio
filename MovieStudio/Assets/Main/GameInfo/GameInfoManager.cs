using UnityEngine;
using System.Collections;

public class GameInfoManager : MonoBehaviour {

    private GameInfoManager m_instance;
    private GameInfoManager() { }

    public GameInfoManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new GameInfoManager();
            return m_instance;
        }
    }

    private GameInfo gameInfo;
    private PlayerInfo playerInfo;

    public GameInfo getGameInfo() { return gameInfo; }
    public PlayerInfo getPlayerInfo() { return playerInfo; }

	// Use this for initialization
	void Start () {
        gameInfo = new GameInfo();
        playerInfo = new PlayerInfo();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
