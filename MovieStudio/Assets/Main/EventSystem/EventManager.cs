using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private delegate void Handler(Event evt);

    private Queue m_eventsQueue;

    private Dictionary<EVT_TYPE, Handler> m_eventsHandlerLink;

    public void SendEvent(Event evt)
    {
        m_eventsQueue.Enqueue(evt);
    }

    public void SendEvent(EVT_TYPE t)
    {
        Event evt = new Event(t);
        m_eventsQueue.Enqueue(evt);
    }

	// Use this for initialization
	void Start () {
        m_eventsQueue = new Queue(100);
        m_eventsHandlerLink = new Dictionary<EVT_TYPE, Handler>(200);

        m_eventsHandlerLink[EVT_TYPE.EVT_TYPE_DEFAULT] = new Handler(DefaultEventHandler.Handle);
        m_eventsHandlerLink[EVT_TYPE.EVT_TYPE_ENTER_GAME] = new Handler(EnterGameEventHandler.Handle);
	}

	// Update is called once per frame
	void Update () {
	    while(m_eventsQueue.Count != 0)
        {
            Event evt = m_eventsQueue.Dequeue() as Event;
            if (evt.type < EVT_TYPE.EVT_TYPE_MAX)
            {
                Handler hdr = m_eventsHandlerLink[(int)evt.type] as Handler;
                hdr(evt);
            }
            else
            {
                Debug.LogError("Wrong Event Type!");
            }
        }
	}
}
