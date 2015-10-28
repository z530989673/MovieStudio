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
        EventManager.Instance.BindEvent(EVT_TYPE.EVT_TYPE_PRELOAD_PARTIAL_FINISH, new Handler(PartialFinshed));
        EventManager.Instance.BindEvent(EVT_TYPE.EVT_TYPE_PRELOAD_TOTAL_FINISH, new Handler(TotalFinshed));

        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/Canvas");
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/MainScreen");
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/TopBar");
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/WelcomeScreen");

        ResourceManager.Instance.StartPreLoad();
    }

	// Use this for initialization
	void Start () {
        startPreLoad();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
