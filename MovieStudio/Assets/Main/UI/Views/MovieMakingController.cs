using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovieMakingController : ViewController {

	public float m_Progress;
	public float m_Speed;
	public Text m_progressText;

	void OnEnable()
	{
		m_Progress = 0;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_Progress += m_Speed * Time.deltaTime;
		if(m_Progress >= 100)
		{
			// complete movie making
			SendEvent(EVT_TYPE.EVT_TYPE_AFTERMAKING_MOVIE);
		}
		SetProgress();
	}

	public void SetProgress()
	{
		m_progressText.text = "Making Movie..." + (int)m_Progress + "%";
	}
}
