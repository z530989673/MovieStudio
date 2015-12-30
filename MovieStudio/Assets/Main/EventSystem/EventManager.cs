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

    public void SendEvent(EVT_TYPE t, object obj)
    {
        Event evt = new Event(t, obj);
        m_eventsQueue.Enqueue(evt);
    }

    public void SendEvent(EVT_TYPE t, ArrayList list)
    {
        Event evt = new Event(t, list);
        m_eventsQueue.Enqueue(evt);
    }

    void Awake()
    {
        m_eventsQueue = new Queue((int)EVT_TYPE.EVT_TYPE_MAX);
        m_callBacks = new Dictionary<EVT_TYPE, Handler>((int)EVT_TYPE.EVT_TYPE_MAX);

        BindEvent(EVT_TYPE.EVT_TYPE_DEFAULT, new Handler(DefaultEventHandler.Handle));

        BindEvent(EVT_TYPE.EVT_TYPE_ENTER_GAME, new Handler(GameLoopEventHandler.EnterGame));
        BindEvent(EVT_TYPE.EVT_TYPE_MAKE_MOVIE, new Handler(GameLoopEventHandler.MakeMovie));
        BindEvent(EVT_TYPE.EVT_TYPE_MAKING_MOVIE, new Handler(GameLoopEventHandler.MakingMovie));
        BindEvent(EVT_TYPE.EVT_TYPE_AE_START, new Handler(GameLoopEventHandler.AfterEffectStart));
        BindEvent(EVT_TYPE.EVT_TYPE_AE_WORKER_CHOOSEN, new Handler(GameLoopEventHandler.AfterEffectWorkerChoosen));
        BindEvent(EVT_TYPE.EVT_TYPE_MOVIE_DONE, new Handler(GameLoopEventHandler.MovieDone));
        BindEvent(EVT_TYPE.EVT_TYPE_MOVIE_PUBLISH, new Handler(GameLoopEventHandler.PublishMovie));
        BindEvent(EVT_TYPE.EVT_TYPE_ADD_ADVERTISEMENT, new Handler(GameLoopEventHandler.AddAdvertisement));

		//Movie Site
		BindEvent(EVT_TYPE.EVT_TYPE_MOVIESITE, new Handler(GameLoopEventHandler.MovieSite));
		BindEvent(EVT_TYPE.EVT_TYPE_INTERNALPLAN, new Handler(GameLoopEventHandler.InternalPlan));

        BindEvent(EVT_TYPE.EVT_TYPE_LOAD_FAILED, new Handler(LoadEventHandler.LoadFailed));
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
