using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanvasList : MonoBehaviour {

    private GameObject m_screen;
    private GameObject m_overlay;
    private GameObject m_popup;
    private GameObject m_debug;

    private GameObject m_curScreen;

    Dictionary<string, GameObject> m_screens;
    Dictionary<string, GameObject> m_overlays;

    // Use this for initialization
    void Awake() {
        m_screen = transform.FindChild("Screens").gameObject;
        m_overlay = transform.FindChild("Overlays").gameObject;
        m_popup = transform.FindChild("PopUps").gameObject;
        m_debug = transform.FindChild("Debugs").gameObject;

        m_screens = new Dictionary<string, GameObject>();
        m_overlays = new Dictionary<string, GameObject>();
        Debug.Log(m_screens == null);
    }

    // Update is called once per frame
    void Update() {

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
            GameObject screen = Resources.Load<GameObject>("UI/Prefabs/" + name);
            if (screen)
            {
                screen = Instantiate(screen);
                m_screens.Add(name, screen);
                screen.transform.parent = m_screen.transform;
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

        GameObject overlay = Resources.Load<GameObject>("UI/Prefabs/" + name);
        overlay = Instantiate(overlay);
        overlay.transform.SetParent(m_overlay.transform);
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

}
