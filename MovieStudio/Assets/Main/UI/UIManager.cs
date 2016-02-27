﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UIManager : MonoBehaviour {

    static private UIManager m_instance;
    private UIManager() { }

    private GameObject m_screen;
    private GameObject m_overlay;
    private GameObject m_popup;
    private GameObject m_debug;

    private GameObject m_curScreen;

    Dictionary<string, GameObject> m_screens;
    Dictionary<string, GameObject> m_overlays;
    Dictionary<string, GameObject> m_popups;

    static public UIManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
            return m_instance;
        }
    }

    public void PreInit()
    {
        OpenScreen("LoadingScreen");
    }

    public void Init()
    {
        LoadOverlay("TopBar");
		LoadOverlay("MovieMaking");
		LoadDebug("DebugUI");
    }
    public void ChangeScreen(Event evt)
    {
        OpenScreen((string)evt.evt_obj[0]);
    }

    // Use this for initialization
    void Awake() {
        m_screen = transform.FindChild("Screens").gameObject;
        m_overlay = transform.FindChild("Overlays").gameObject;
        m_popup = transform.FindChild("PopUps").gameObject;
        m_debug = transform.FindChild("Debugs").gameObject;

        m_screens = new Dictionary<string, GameObject>();
        m_overlays = new Dictionary<string, GameObject>();
		m_popups = new Dictionary<string, GameObject>();

		// Set backgound of pop up to click to close all popups
		Transform background = m_popup.transform.GetChild(0);
		background.GetComponent<Button>().onClick.AddListener( ()=>{CloseAllPopUps(); background.gameObject.SetActive(false);});

    }

	// Use this for initialization
	void Start () {
        GameManager.Instance.BindEvent(EVT_TYPE.EVT_TYPE_CHANGE_SCREEN, new Handler(ChangeScreen));
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.D))
			SetDebugUIEnable(true);
	}

    public GameObject OpenScreen(string name)
    {
        CloseCurrentScreen();
        if (m_screens.ContainsKey(name))
        {
            m_curScreen = m_screens[name];
        }
        else
        {
            GameObject screen = GameManager.Instance.GetResourceObject("UI/Prefabs/" + name);
            if (screen)
            {
                screen = Instantiate(screen);
                m_screens.Add(name, screen);
                screen.transform.SetParent(m_screen.transform, false);
                m_curScreen = screen;
            }
        }
        m_curScreen.SetActive(true);

        return m_curScreen;
    }

    public void CloseCurrentScreen()
    {
        if (m_curScreen)
        {
            m_curScreen.SetActive(false);
        }
    }

    public GameObject LoadOverlay(string name)
    {
        if(m_overlays.ContainsKey(name))
        {
            return m_overlays[name];
        }

        GameObject overlay = GameManager.Instance.GetResourceObject("UI/Prefabs/" + name);
        overlay = Instantiate(overlay);
        overlay.transform.SetParent(m_overlay.transform, false);
        m_overlays.Add(name, overlay);
        overlay.SetActive(false);
        return overlay;
    }

    public void SetOverlayEnable(string name, bool enabled)
    {
        if (m_overlays.ContainsKey(name))
        {
            m_overlays[name].SetActive(enabled);
        }
    }

	public GameObject LoadPopUp(string name)
	{
		if(m_popups.ContainsKey(name))
		{
			return m_popups[name];
		}

		GameObject popup = Instantiate(GameManager.Instance.GetResourceObject("UI/Prefabs/" + name));
		popup.transform.SetParent(m_popup.transform, false);
		m_popups.Add(name, popup);
		popup.SetActive(false);
		return popup;
	}

	public void SetPopupEnable(string name, bool enabled)
	{
		if (m_popups.ContainsKey(name))
		{
			m_popup.transform.GetChild(0).gameObject.SetActive(enabled);
			m_popups[name].SetActive(enabled);
		}
		else
		{
			LoadPopUp(name);
			SetPopupEnable(name, enabled);
		}
	}

	public void CloseAllPopUps()
	{
		foreach( string key in m_popups.Keys)
		{
			m_popups[key].SetActive(false);
		}
	}

	public void SetPopupDebugEnable(string name, bool enabled)
	{
		m_debug.SetActive(enabled);
	}

	private void LoadDebug(string name)
	{
		GameObject debugUI = Instantiate(GameManager.Instance.GetResourceObject("UI/Prefabs/" + name));
		debugUI.transform.SetParent(m_debug.transform, false);
		debugUI.SetActive(false);
		m_debug = debugUI;
	}

	public void SetDebugUIEnable(bool enabled)
	{
		m_debug.SetActive(enabled);
	}


	public GameObject GetScreen(string name)
	{
		if(m_screens.ContainsKey(name))
		{
			return m_screens[name];
		}
		return null;
	}

	public GameObject GetOverlay(string name)
	{
		if(m_overlays.ContainsKey(name))
		{
			return m_overlays[name];
		}
		return null;
	}

	public GameObject GetPopUp(string name)
	{
		if(m_popups.ContainsKey(name))
		{
			return m_popups[name];
		}
		return null;
	}

}
