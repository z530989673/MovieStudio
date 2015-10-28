using UnityEngine;
using System.Collections;

public class Event {
    public Event(EVT_TYPE t) { type = t; evt_obj = new ArrayList(); }

    //public delegate void CallBack(Event evt);
    public ArrayList evt_obj;
    public EVT_TYPE type;
    //public event CallBack callBacks;

    //public void HandleCallBacks(Event evt)
    //{
    //    if (callBacks != null)
    //        callBacks.Invoke(evt);
    //}
}
