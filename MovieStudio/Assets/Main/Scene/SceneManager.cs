using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour {

    private static SceneManager m_instance;
    private SceneManager() { }

    public static SceneManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = GameObject.FindObjectOfType(typeof(SceneManager)) as SceneManager;
            return m_instance;
        }
    }

    public GameObject board;
    public GameObject Rooms;

    Board gameBoard;
    List<SceneCharactor> charactors;
    List<Room> rooms;

	// Use this for initialization
	void Start () {
        charactors = new List<SceneCharactor>(30);
        rooms = new List<Room>(20);
        gameBoard = new Board();
	}
	
    public void Init()
    {
        List<RoomData> roomData = GameManager.Instance.GetRoomData();

        gameBoard.Init(roomData[0].size, board);

        for (int i = 0; i < roomData.Count; i++)
        {
            Room room = new Room(Rooms);
            room.ResetRoom(roomData[i], 1);
            rooms.Add(room);
        }

        //rooms[0].ResetRoom(roomData[0], 0);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public Vector2 GetActualPos(Pair coord)
    {
        Vector2 result;
        result.x = - coord.x * 0.5f + coord.y * 0.5f;
        result.y = coord.x * 0.25f + coord.y * 0.25f;
        return result;
    }
}
