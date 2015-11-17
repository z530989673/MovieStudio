using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovieDonePopUpController : ViewController {

	public Button m_publishBtn;
	public Button m_advertisementBtn;
	public Button m_backBtn;
	public Button m_confirmBtn;

	private bool m_initialized = false;

	void OnEnable()
	{
		OpenNum(0);
		if(!m_initialized)
		{
			m_publishBtn.interactable = false;
			m_publishBtn.onClick.AddListener(delegate {
				SendEvent(EVT_TYPE.EVT_TYPE_MOVIE_PUBLISH);
			});

			m_advertisementBtn.onClick.AddListener(delegate {
				OpenNum(1);
			});

			m_backBtn.onClick.AddListener(delegate {
				OpenNum(0);
			});

			m_confirmBtn.onClick.AddListener(delegate {
				SendEvent(EVT_TYPE.EVT_TYPE_ADD_ADVERTISEMENT);
				SendEvent(EVT_TYPE.EVT_TYPE_MOVIE_PUBLISH);
			});
			m_initialized = true;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OpenNum(int childId)
	{
		for(int i = 0;i<transform.childCount;i++)
		{
			transform.GetChild(i).gameObject.SetActive(i == childId);
		}
	}
}
