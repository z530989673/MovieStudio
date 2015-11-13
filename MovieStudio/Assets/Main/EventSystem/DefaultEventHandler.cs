using UnityEngine;
using System.Collections;

public class DefaultEventHandler {
    public static void Handle(Event evt)
    {
        Debug.Log("event Handled in handler!");
        //evt.HandleCallBacks(evt);
    }

	public static void MakeMovie(Event evt)
	{
		UIManager.Instance.MakeMovie();
	}

	public static void MakingMovie(Event evt)
	{
		UIManager.Instance.MakingMovie();
	}

	public static void AfterMakingMovie(Event evt)
	{
		UIManager.Instance.AfterMakingMovie();
	}

	public static void FinishMakingMovie(Event evt)
	{
		UIManager.Instance.FinishMakingMovie();
	}
}
