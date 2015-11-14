using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AfterEffectPopUpController : ViewController {

	public Text m_peopleNumber;
	public Button m_start;
	public GameObject m_peopleButton;
	public GameObject m_group;

	void OnEnable()
	{
		for(int i = 0;i<m_group.transform.childCount;i++)
		{
			Destroy(m_group.transform.GetChild(i).gameObject);
		}

		for(int i = 0;i<3;i++)
		{
			GameObject go = Instantiate(m_peopleButton);
			go.transform.SetParent(m_group.transform, false);
		}
		m_start.onClick.AddListener( delegate {
			SendEvent(EVT_TYPE.EVT_TYPE_AE_WORKER_CHOOSEN);
		});
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
