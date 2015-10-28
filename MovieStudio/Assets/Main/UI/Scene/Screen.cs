using UnityEngine;
using System.Collections;

public class Screen : MonoBehaviour {

    protected void SendEvent(EVT_TYPE t)
    {
        EventManager.Instance.SendEvent(t);
    }

    protected void SendEvent(Event evt)
    {
        EventManager.Instance.SendEvent(evt);
    }

    virtual protected void Init(Event evt)
    {

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
