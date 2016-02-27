using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MovieDonePopUpController : ViewController {

	public Button m_publishBtn;
	public Button m_advertisementBtn;
	public Button m_backBtn;
	public Button m_confirmBtn;

	public GameObject m_ADButton;
	public GameObject m_ADGroup;

	private int m_adNumber = 4;

	private ItemButton m_selectedButton;

	void OnEnable()
	{
		OpenNum(0);
		InitAdsPanel();
	}

	void Start()
	{
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

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void InitAdsPanel()
	{

		for(int i = 0;i<m_ADGroup.transform.childCount;i++)
		{
			Destroy(m_ADGroup.transform.GetChild(i).gameObject);
		}

		for(int i = 0;i<m_adNumber;i++)
		{
			GameObject go = Instantiate(m_ADButton);
			go.name = "Advertisement " + i;
			go.transform.SetParent(m_ADGroup.transform, false);
			ItemButton button = go.GetComponent<ItemButton>();
			button.SetSwitchable(true);
			button.SetText(go.name);
			
			button.AddOnClick(()=>{
				
				if(m_selectedButton != null)
				{
					m_selectedButton.Deselect();
				}
				m_selectedButton = m_selectedButton == button ? null : button;
				m_confirmBtn.interactable = m_selectedButton != null;
				
			});
		}
		m_confirmBtn.interactable = m_selectedButton != null;
	}

	private void OpenNum(int childId)
	{
		for(int i = 0;i<transform.childCount;i++)
		{
			transform.GetChild(i).gameObject.SetActive(i == childId);
		}
	}

}
