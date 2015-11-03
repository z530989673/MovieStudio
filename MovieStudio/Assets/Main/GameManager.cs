using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager m_instance;
    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
            return m_instance;
        }
    }

    public GameObject GetResourceObject(string str)
    {
        return ResourceManager.Instance.GetResourceObject(str);
    }

    /// <summary>
    /// CAUTION: each event can only bind one callback function!!!
    /// </summary>
    /// <param name="t">event type</param>
    /// <param name="hdr">call back(handler)</param>
    public void BindEvent(EVT_TYPE t, Handler hdr)
    {
        EventManager.Instance.BindEvent(t, hdr);
    }

    public void UnbindEvent(EVT_TYPE t)
    {
        EventManager.Instance.UnbindEvent(t);
    }

    public void SendEvent(EVT_TYPE t)
    {
        EventManager.Instance.SendEvent(t);
    }

    public void SendEvent(Event evt)
    {
        EventManager.Instance.SendEvent(evt);
    }

    void startPreLoad()
    {
        for (int i = 0; i < 25; i++)//temp
        {
            ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/MainScreen");
            ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/TopBar");
            ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/WelcomeScreen");
        }

        StartCoroutine(ResourceManager.Instance.StartPreLoad());
    }

	// Use this for initialization
	void Start () {
        UIManager.Instance.Init();
        startPreLoad();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
