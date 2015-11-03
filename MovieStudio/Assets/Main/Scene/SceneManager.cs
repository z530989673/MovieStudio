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

    Board gameBoard;
    List<SceneCharactor> charactors;
    List<Room> rooms;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
