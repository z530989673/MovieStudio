﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : Screen {

    public Text percentText;

	// Use this for initialization
	void Start () {
        EventManager.Instance.BindEvent(EVT_TYPE.EVT_TYPE_PRELOAD_PARTIAL_FINISH, new Handler(UpdatePercent));
        EventManager.Instance.BindEvent(EVT_TYPE.EVT_TYPE_PRELOAD_TOTAL_FINISH, new Handler(FinishLoad));
	}
	
    private void UpdatePercent(Event evt)
    {
        percentText.text = (float)evt.evt_obj[1] * 100 + "%";
        Debug.Log((float)evt.evt_obj[1] * 100 + "%");
    }

    private void FinishLoad(Event evt)
    {
        Event e = new Event(EVT_TYPE.EVT_TYPE_CHANGE_SCREEN);
        e.evt_obj.Add("WelcomeScreen");
        EventManager.Instance.SendEvent(e);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
