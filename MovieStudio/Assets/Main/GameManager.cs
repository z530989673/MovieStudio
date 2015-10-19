using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameManager m_instance;
    private GameManager() { }

    public GameManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new GameManager();
            return m_instance;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
