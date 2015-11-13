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

	public static void AfterEffectStart(Event evt)
	{
		UIManager.Instance.AfterEffectStart();
	}

	public static void AfterEffectWorkerChoosen(Event evt)
	{
		UIManager.Instance.AfterEffectWorkerChoosen();
	}

	public static void MovieDone(Event evt)
	{
		UIManager.Instance.MovieDone();
	}
}
