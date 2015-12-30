using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MovieSitePopUpController : MonoBehaviour {

	public GameObject m_itemButton;
	public Button m_updateButton;
	public Button m_expandButton;
	public Button m_backButton;
	public Button m_confirmButton;
	
	public GameObject m_roomsGroup;
	public GameObject m_compare;

	public GameObject m_sitesButton;

	private Text m_compareSiteName;
	private Text m_compareCost;

	private int m_siteTotal = 4;

	private GameObject m_selectedSite = null;

	// Use this for initialization
	void Start () {

		m_updateButton.onClick.AddListener(()=>{
			if(m_selectedSite != null)
				return;
			m_roomsGroup.SetActive(true);
			GetUpdateList();
			m_expandButton.interactable = true;
			m_updateButton.interactable = false;
		});

		m_expandButton.onClick.AddListener(()=>{			
			if(m_selectedSite != null)
				return;
			GetExpandList();
			m_roomsGroup.SetActive(true);
			m_updateButton.interactable = true;
			m_expandButton.interactable = false;
		});

		if(m_compare == null)
		{
			Debug.LogError("compare GO not set!");
			return;
		}

		m_compareSiteName = m_compare.transform.FindChild("SiteName").GetComponent<Text>();
		m_compareCost = m_compare.transform.FindChild("Cost").GetComponent<Text>();



		m_confirmButton.onClick.AddListener(()=>{
			Debug.Log("Site : " + m_selectedSite.name + " is confirmed!");
			UIManager.Instance.SetPopupEnable("MovieSitePopUp", false);
		});

		m_backButton.onClick.AddListener(()=>{
			if(m_compare.activeInHierarchy)
			{
				m_compare.SetActive(false);
				m_selectedSite = null;
			}
			else
			{
				UIManager.Instance.SetPopupEnable("MovieSitePopUp", false);
			}
		});


	}

	void OnEnable()
	{
		m_updateButton.interactable = true;
		m_expandButton.interactable = true;
		m_confirmButton.interactable = false;
		m_roomsGroup.SetActive(false);
		m_compare.SetActive(false);
		m_selectedSite = null;
	}

	void GetExpandList()
	{
		ClearGroup ();
		for(int i = 0;i<m_siteTotal;i++)
		{
			GameObject go = Instantiate(m_sitesButton);
			go.transform.SetParent(m_roomsGroup.transform, false);			
			
			go.name = "Expand Site "+i;
			ItemButton item = go.GetComponent<ItemButton>();
			item.SetText(go.name);
			item.AddOnClick(()=>{
				m_compare.SetActive(true);
				m_compareSiteName.text = "MovieSite: \n" + go.name;
				m_compareCost.text = "Cost: 100";
				m_confirmButton.interactable = true;
				m_selectedSite = go;
			});
		}
	}

	void GetUpdateList()
	{
		ClearGroup();
		for(int i = 0;i<m_siteTotal;i++)
		{
			GameObject go = Instantiate(m_sitesButton);
			go.transform.SetParent(m_roomsGroup.transform, false);			
			
			go.name = "Update Site "+i;
			ItemButton item = go.GetComponent<ItemButton>();
			item.SetText(go.name);
			item.AddOnClick(()=>{
				m_compare.SetActive(true);
				m_compareSiteName.text = "MovieSite: \n" + go.name;
				m_compareCost.text = "Cost: 200";
				m_confirmButton.interactable = true;
				m_selectedSite = go;
			});
		}
	}

	void ClearGroup()
	{
		for(int i = m_roomsGroup.transform.childCount-1; i>=0 ; i--)
		{
			Destroy( m_roomsGroup.transform.GetChild(i).gameObject );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
