﻿using UnityEngine;
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

    public void Init()
    {
        if (!m_canvas)
        {
            GameObject canvas = Instantiate(GameManager.Instance.GetResourceObject("UI/Prefabs/Canvas"));
            m_canvas = canvas;
            m_canvasList = m_canvas.GetComponent<CanvasList>();
            m_canvasList.OpenScreen("LoadingScreen");
			
            // should be after resource are loaded.....
            m_canvasList.LoadOverlay("TopBar");
			m_canvasList.LoadOverlay("MovieMaking");
        }
    }

    public void ChangeScreen(Event evt)
    {
        m_canvasList.OpenScreen((string)evt.evt_obj[0]);
    }

	// Use this for initialization
	void Start () {
        GameManager.Instance.BindEvent(EVT_TYPE.EVT_TYPE_CHANGE_SCREEN, new Handler(ChangeScreen));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EnterGame()
    {
        m_canvasList.OpenScreen("MainScreen");
        m_canvasList.SetOverlayEnable("TopBar", true);
    }

	public void MakeMovie()
	{
		GameObject newMovie = m_canvasList.LoadPopUp("NewMoviePopUp");
		m_canvasList.SetPopupEnable("NewMoviePopUp", !newMovie.activeInHierarchy);
	}

	public void MakingMovie()
	{
		m_canvasList.LoadPopUp("NewMoviePopUp");
		m_canvasList.SetPopupEnable("NewMoviePopUp", false);

		m_canvasList.SetOverlayEnable("MovieMaking", true);
		MovieMakingController mmc = m_canvasList.GetOverlay("MovieMaking").GetComponent<MovieMakingController>();
		mmc.movieStatus = MovieMakingController.MovieStatus.Making;
	}

	public void AfterEffectStart()
	{
		m_canvasList.SetOverlayEnable("MovieMaking", false);
		
		m_canvasList.LoadPopUp("AfterEffectPopUp");
		m_canvasList.SetPopupEnable("AfterEffectPopUp", true);
	}

	public void AfterEffectWorkerChoosen()
	{
		m_canvasList.SetPopupEnable("AfterEffectPopUp", false);
		m_canvasList.SetOverlayEnable("MovieMaking", true);
		MovieMakingController mmc = m_canvasList.GetOverlay("MovieMaking").GetComponent<MovieMakingController>();
		mmc.movieStatus = MovieMakingController.MovieStatus.AfterEffect;
	}

	public void MovieDone()
	{
		m_canvasList.SetOverlayEnable("MovieMaking", false);
		m_canvasList.LoadPopUp("MovieDonePopUp");
		m_canvasList.SetPopupEnable("MovieDonePopUp", true);
	}

	public void PublishMovie()
	{
		m_canvasList.SetPopupEnable("MovieDonePopUp", false);
	}

	public void AddAdvertisement()
	{
		Debug.Log("doing nothing right now...");
	}

}
