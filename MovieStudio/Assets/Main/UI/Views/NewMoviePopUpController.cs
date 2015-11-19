using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NewMoviePopUpController : ViewController {



	// Actors panel
	public GameObject m_actorsGroup;
	public GameObject m_actorButton;
	private Button m_actorFinishButton;

	public Text m_actorNumber;
	private int m_actorTotal = 4;
	private int m_actorLimit = 3;

	private HashSet<string> m_selectedActors;

	void Awake()
	{
		m_selectedActors = new HashSet<string>();

		for(int i = 0;i<transform.childCount;i++)
		{
			GameObject go = transform.GetChild(i).gameObject;
			go.SetActive(true);
			Button [] buttons = go.GetComponentsInChildren<Button>();
			for(int j = 0;j<buttons.Length;j++)
			{
				SetButton(buttons[j], i);
			}
			if(i == (transform.childCount - 1))
			{
				m_actorFinishButton = buttons[0];
			}
		}
		
		m_actorFinishButton.interactable = m_selectedActors.Count >= m_actorLimit;
	}

	void OnEnable()
	{
		// close all except first one
		for(int i = 0;i<transform.childCount;i++)
		{
			transform.GetChild(i).gameObject.SetActive(i == 0);
		}
		InitActorsPanel();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void InitActorsPanel()
	{
		m_selectedActors.Clear();
		for(int i = 0;i<m_actorsGroup.transform.childCount;i++)
		{
			Destroy(m_actorsGroup.transform.GetChild(i).gameObject);
		}

		for(int i = 0;i<m_actorTotal;i++)
		{
			GameObject go = Instantiate(m_actorButton);
			go.transform.SetParent(m_actorsGroup.transform, false);
			go.name = "Actor "+i;
			ItemButton item = go.GetComponent<ItemButton>();
			item.SetSwitchable(true);
			item.SetText(go.name);
			item.AddOnClick(()=>{
				if(!ItemSelected(item.name))
				{
					item.Deselect();
				}
			});
		}
		RefreshActorNumber();
	}

	private void SetButton(Button button, int childIndex)
	{
		button.onClick.AddListener( ()=>{
			transform.GetChild(childIndex).gameObject.SetActive(false);
			if(childIndex+1 == transform.childCount)
			{
				// may send different event type in future: make movie done?
				SendEvent(EVT_TYPE.EVT_TYPE_MAKING_MOVIE);
				Debug.Log("Start to make Movie!");
			}
			else
			{
				transform.GetChild(childIndex+1).gameObject.SetActive(true);
			}
		});
	}

	private bool ItemSelected(string item)
	{
		if(m_selectedActors.Contains(item))
		{
			m_selectedActors.Remove(item);
		}
		else if(m_selectedActors.Count < m_actorLimit)
		{
			m_selectedActors.Add(item);
		}
		else // Already up to limit, cannot select item
		{
			return false;
		}
		RefreshActorNumber();
		m_actorFinishButton.interactable = m_selectedActors.Count >= m_actorLimit;

		return true;
	}

	private void RefreshActorNumber()
	{
		m_actorNumber.text = "Number " + m_selectedActors.Count + "/" + m_actorLimit;
	}
}
