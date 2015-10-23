using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour {

    private ResourceManager m_instance;
    private ResourceManager() { }

    public ResourceManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = GameObject.FindObjectOfType(typeof(ResourceManager)) as ResourceManager;
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
