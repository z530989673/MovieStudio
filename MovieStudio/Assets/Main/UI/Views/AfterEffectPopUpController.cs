using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AfterEffectPopUpController : ViewController {

	public Text m_techNumber;
	public Button m_start;
	public GameObject m_techButton;
	public GameObject m_techGroup;

	private int m_techLimit = 1;
	private int m_techTotal = 3;

	private HashSet<string> m_selectedTeches;

	void Awake()
	{
		m_selectedTeches = new HashSet<string>();
		
		m_start.onClick.AddListener( delegate {
			if(m_selectedTeches.Count == m_techLimit)
			{
				SendEvent(EVT_TYPE.EVT_TYPE_AE_WORKER_CHOOSEN);
			}
		});
	}

	void OnEnable()
	{
		InitTechPanel();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void InitTechPanel()
	{
		m_selectedTeches.Clear();
		for(int i = 0;i<m_techGroup.transform.childCount;i++)
		{
			Destroy(m_techGroup.transform.GetChild(i).gameObject);
		}

		for(int i = 0;i<m_techTotal;i++)
		{
			GameObject go = Instantiate(m_techButton);
			go.name = "Tech " + i;
			go.transform.SetParent(m_techGroup.transform, false);
			ItemButton item = go.GetComponent<ItemButton>();
			item.SetSwitchable(true);
			item.SetText(go.name);
			
			item.AddOnClick( () => {
				if(!ItemSelected(item.name))
				{
					item.Deselect();
				}
			});
		}
		m_start.interactable = m_selectedTeches.Count >= m_techLimit;
		RefreshTechNumber();
	}

	private void RefreshTechNumber()
	{
		m_techNumber.text = "Number " + m_selectedTeches.Count + "/" + m_techLimit;
	}

	private bool ItemSelected(string item)
	{
		if(m_selectedTeches.Contains(item))
		{
			m_selectedTeches.Remove(item);
		}
		else if(m_selectedTeches.Count < m_techLimit)
		{
			m_selectedTeches.Add(item);
		}
		else // Already up to limit, cannot select item
		{
			return false;
		}
		m_start.interactable = m_selectedTeches.Count >= m_techLimit;

		RefreshTechNumber();
		
		return true;
	}

}
