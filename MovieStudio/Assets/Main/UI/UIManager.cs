using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    private UIManager m_instance;
    private UIManager() { }

    public UIManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new UIManager();
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
