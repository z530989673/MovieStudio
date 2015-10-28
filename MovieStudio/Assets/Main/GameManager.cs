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
                m_instance = new GameManager();
            return m_instance;
        }
    }

    void PartialFinshed(Event evt)
    {
        Debug.Log((float)evt.evt_obj[1]);
    }

    void TotalFinshed(Event evt)
    {
        Debug.Log("Total finished");
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
