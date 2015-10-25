using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    static private UIManager m_instance;
    private UIManager() { }

    private GameObject m_canvas;
    private CanvasList m_canvasList;

    static public UIManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
            return m_instance;
        }
    }

	// Use this for initialization
	void Start () {
        if (!m_canvas)
        {
            GameObject canvas = Instantiate(ResourceManager.Instance.GetResourceObject("UI/Prefabs/Canvas"));
            m_canvas = canvas;
            m_canvasList = m_canvas.GetComponent<CanvasList>();
            m_canvasList.OpenScreen("MainScreen");

            m_canvasList.LoadOverlay("TopBar");

            m_canvasList.OpenScreen("WelcomeScreen");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EnterGame()
    {
        m_canvasList.OpenScreen("MainScreen");
        m_canvasList.SetOverlayEnable("TopBar", true);
    }
}
