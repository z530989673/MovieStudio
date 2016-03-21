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
    List<SceneCharacter> characters;
    List<Room> rooms;

    bool needUpdateOrder = false;

	// Use this for initialization
	void Start () {
        characters = new List<SceneCharacter>(30);
        rooms = new List<Room>(20);
        gameBoard = new Board();
	}
	
    public void Init()
    {
        needUpdateOrder = true;
        List<RoomData> roomData = GameManager.Instance.GetRoomData();

        gameBoard.Init(roomData[0], board);

        for (int i = 0; i < roomData.Count; i++)
        {
            Room room = new Room(Rooms);
            room.ResetRoom(roomData[i]);
            rooms.Add(room);

            gameBoard.UpdateRoom(roomData[i], GameManager.Instance.GetRoomLevel(i));
            gameBoard.UpdateRoomTilesOrder(roomData[i], GameManager.Instance.GetRoomLevel(i));
        }

        gameBoard.CalculateTileOrder();

        gameBoard.UpdateWall(roomData);
    }

	// Update is called once per frame
	void Update () {

        if (needUpdateOrder)
        {
            foreach (SceneCharacter sceneCharacter in characters)
                sceneCharacter.Update();
            foreach (Room room in rooms)
                room.update();
            gameBoard.Update();

            needUpdateOrder = false;
        }
	}

    static public Vector2 GetActualPos(Pair coord)
    {
        Vector2 result;
        result.x = - coord.x * 0.5f + coord.y * 0.5f;
        result.y = coord.x * 0.25f + coord.y * 0.25f;
        return result;
    }

    public int GetTileOrder(Pair pos)
    {
        if (pos.x < 0 || pos.y < 0)
            return gameBoard.GetTileOrder(new Pair(0, 0));
        else if (pos.x >= gameBoard.size.x || pos.y >= gameBoard.size.y)
            return gameBoard.GetTileOrder(new Pair(gameBoard.size.x - 1, gameBoard.size.y - 1));
        else
            return gameBoard.GetTileOrder(pos);
    }
}
