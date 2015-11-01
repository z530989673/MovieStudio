﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WelcomeScreen : ViewController {

    // Use this for initialization
	void Awake () {
        Button button = GetComponentInChildren<Button>();
        button.onClick.AddListener(delegate { EnterGameEvent(); });
	}
	
    void EnterGameEvent()
    {
        SendEvent(EVT_TYPE.EVT_TYPE_ENTER_GAME);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
