using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    private SceneManager m_instance;
    private SceneManager() { }

    public SceneManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = GameObject.FindObjectOfType(typeof(SceneManager)) as SceneManager;
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
