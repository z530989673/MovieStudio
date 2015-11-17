using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovieMakingController : ViewController {

	public enum MovieStatus
	{
		None = 0,
		Making = 1,
		AfterEffect = 2,
		Done = 3
	}

	public float m_progress;
	private float m_speed = 30;
	public Text m_progressText;
	private MovieStatus m_movieStatus;
	public MovieStatus movieStatus{ get { return m_movieStatus;} set{ m_movieStatus = value;}}

	void OnEnable()
	{
		m_progress = 0;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_progress += m_speed * Time.deltaTime;
		if(m_progress >= 100)
		{
			// complete movie making
			switch(m_movieStatus)
			{
			case MovieStatus.Making:
				SendEvent(EVT_TYPE.EVT_TYPE_AE_START);
				break;
			case MovieStatus.AfterEffect:
				SendEvent(EVT_TYPE.EVT_TYPE_MOVIE_DONE);
				break;
			default:
				break;
			}
		}
		SetProgress();
	}



	public void SetProgress()
	{
		switch(m_movieStatus)
		{
		case MovieStatus.None:
			m_progressText.text = "";
			break;
		case MovieStatus.Making:
			m_progressText.text = "Making Movie..." + (int)m_progress + "%";
			break;
		case MovieStatus.AfterEffect:
			m_progressText.text = "After Effect..." + (int)m_progress + "%";
			break;
		case MovieStatus.Done:
			m_progressText.text = "Movie Done!";
			break;
		}
	}
}
