using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InternalPlanPopUpController : MonoBehaviour {
	
	public GameObject m_sitesButton;
	public Button m_developButton;
	public Button m_recruitButton;
	public Button m_backButton;
	public Button m_confirmButton;
	
	public GameObject m_plansGroup;
	public GameObject m_planOverview;
	
	private Text m_planName;
	private Text m_planCost;
	
	private int m_siteTotal = 4;
	
	private GameObject m_selectedPlan = null;
	
	// Use this for initialization
	void Start () {
		
		m_developButton.onClick.AddListener(()=>{
			if(m_selectedPlan != null)
				return;
			m_plansGroup.SetActive(true);
			GetDevelopmentPlanList();
			m_recruitButton.interactable = true;
			m_developButton.interactable = false;
		});
		
		m_recruitButton.onClick.AddListener(()=>{			
			if(m_selectedPlan != null)
				return;
			m_plansGroup.SetActive(true);
			GetRecruitPlanList();
			m_developButton.interactable = true;
			m_recruitButton.interactable = false;
		});
		
		if(m_planOverview == null)
		{
			Debug.LogError("compare GO not set!");
			return;
		}
		
		m_planName = m_planOverview.transform.FindChild("PlanName").GetComponent<Text>();
		m_planCost = m_planOverview.transform.FindChild("Cost").GetComponent<Text>();
		

		m_confirmButton.onClick.AddListener(()=>{
			Debug.Log("Plan : " + m_selectedPlan.name + " is confirmed!");
			UIManager.Instance.SetPopupEnable("InternalPlanPopUp", false);
		});
		
		m_backButton.onClick.AddListener(()=>{
			if(m_planOverview.activeInHierarchy)
			{
				m_planOverview.SetActive(false);
				m_selectedPlan = null;
			}
			else
			{
				UIManager.Instance.SetPopupEnable("InternalPlanPopUp", false);
			}
		});
		
		
	}
	
	void OnEnable()
	{
		m_developButton.interactable = true;
		m_recruitButton.interactable = true;
		m_confirmButton.interactable = false;
		m_plansGroup.SetActive(false);
		m_planOverview.SetActive(false);
		m_selectedPlan = null;
	}

	void GetDevelopmentPlanList()
	{
		ClearGroup();

		for(int i = 0;i<m_siteTotal;i++)
		{
			GameObject go = Instantiate(m_sitesButton);
			go.transform.SetParent(m_plansGroup.transform, false);			
			
			go.name = "Plan "+i;
			ItemButton item = go.GetComponent<ItemButton>();
			item.SetText(go.name);
			item.AddOnClick(()=>{
				m_planOverview.SetActive(true);
				m_planName.text = "Plan : " + go.name;
				m_planCost.text = "Cost: 100";
				m_confirmButton.interactable = true;
				m_selectedPlan = go;
			});
		}

	}

	void GetRecruitPlanList()
	{
		ClearGroup();
		
		for(int i = 0;i<m_siteTotal;i++)
		{
			GameObject go = Instantiate(m_sitesButton);
			go.transform.SetParent(m_plansGroup.transform, false);			
			
			go.name = "Recruit "+i;
			ItemButton item = go.GetComponent<ItemButton>();
			item.SetText(go.name);
			item.AddOnClick(()=>{
				m_planOverview.SetActive(true);
				m_planName.text = "Plan : " + go.name;
				m_planCost.text = "Cost: 200";
				m_confirmButton.interactable = true;
				m_selectedPlan = go;
			});
		}
		
	}

	void ClearGroup()
	{
		for(int i = m_plansGroup.transform.childCount-1; i>=0 ; i--)
		{
			Destroy( m_plansGroup.transform.GetChild(i).gameObject );
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
