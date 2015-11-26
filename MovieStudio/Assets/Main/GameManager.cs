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

    public void SendEvent(EVT_TYPE t, object obj = null)
    {
        EventManager.Instance.SendEvent(t, obj);
    }

    public void SendEvent(EVT_TYPE t, ArrayList list)
    {
        EventManager.Instance.SendEvent(t, list);
    }

    public void SendEvent(Event evt)
    {
        EventManager.Instance.SendEvent(evt);
    }

    void startPreLoad()
    {
        //basic prefabs
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/ItemButton");//really need to load this?
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/SceneItem");

        //screen
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/MainScreen");
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/WelcomeScreen");

        //overlay
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/TopBar");
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/MovieMaking");

        //popup
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/AfterEffectPopUp");
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/MovieDonePopUp");
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_PREFAB, "UI/Prefabs/NewMoviePopUp");

        //texture
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_TEXTURE, "UI/Textures/board");
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_TEXTURE, "UI/Textures/chair");
        ResourceManager.Instance.AddPreLoadResource(RESOURCE_TYPE.RESOURCE_TEXTURE, "UI/Textures/wall");

        //TODO: data

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
