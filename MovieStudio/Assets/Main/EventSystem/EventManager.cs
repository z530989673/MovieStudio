using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void Handler(Event evt);

public class EventManager : MonoBehaviour {

    
    private static EventManager m_instance;
    private EventManager() { }

    public static EventManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = GameObject.FindObjectOfType(typeof(EventManager)) as EventManager;
            return m_instance;
        }
    }

    private Queue m_eventsQueue;

    private Dictionary<EVT_TYPE, Handler> m_callBacks;

    public void BindEvent(EVT_TYPE t, Handler hdr)
    {
        m_callBacks[t] = hdr;
    }

    public void UnbindEvent(EVT_TYPE t)
    {
        if (m_callBacks.ContainsKey(t))
            m_callBacks.Remove(t);
    }

    public void SendEvent(Event evt)
    {
        m_eventsQueue.Enqueue(evt);
    }

    public void SendEvent(EVT_TYPE t)
    {
        Event evt = new Event(t);
        m_eventsQueue.Enqueue(evt);
    }

    void Awake()
    {
        m_eventsQueue = new Queue(100);
        m_callBacks = new Dictionary<EVT_TYPE, Handler>(200);

        BindEvent(EVT_TYPE.EVT_TYPE_DEFAULT, new Handler(DefaultEventHandler.Handle));
        BindEvent(EVT_TYPE.EVT_TYPE_ENTER_GAME, new Handler(EnterGameEventHandler.Handle));
		BindEvent(EVT_TYPE.EVT_TYPE_MAKEMOVIE, new Handler(DefaultEventHandler.MakeMovie));
    }

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	    while(m_eventsQueue.Count != 0)
        {
            Event evt = m_eventsQueue.Dequeue() as Event;
            if (evt.type < EVT_TYPE.EVT_TYPE_MAX)
            {
                Handler hdr = m_callBacks[evt.type] as Handler;
                hdr(evt);
            }
            else
                Debug.LogError("Wrong Event Type!");
        }
	}
}
