using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopBarController : ViewController {

	private GameObject m_Menu;
	private GameObject m_Money;
	private GameObject m_OverallInfo;
	private GameObject m_Themes;
	private GameObject m_Actors;
	private GameObject m_Competers;

	private GameObject m_DropMenu;
	private GameObject m_MakeMovie;
	private GameObject m_MovieSite;
	private GameObject m_InternalPlan;
	private GameObject m_ExternalPlan;
	
	void Awake()
	{
		Init();
	}

	void Init()
	{
		Transform ButtonGroup = transform.FindChild("Bar").FindChild("ButtonGroup");
		m_Menu 		= ButtonGroup.FindChild("Menu").gameObject;
		m_Money 	= ButtonGroup.FindChild("Money").gameObject;
		m_OverallInfo = ButtonGroup.FindChild("Overall Info").gameObject;
		m_Themes 	= ButtonGroup.FindChild("Themes").gameObject;
		m_Actors 	= ButtonGroup.FindChild("Actors").gameObject;
		m_Competers = ButtonGroup.FindChild("Competers").gameObject;
		m_DropMenu = transform.FindChild("DropMenu").gameObject;

		if(m_DropMenu)
		{
			Transform menuGroup = m_DropMenu.transform.FindChild("MenuGroup");
			m_MakeMovie = menuGroup.FindChild("MakeMovie").gameObject;
			m_MovieSite = menuGroup.FindChild("MovieSite").gameObject;
			m_InternalPlan = menuGroup.FindChild("InternalPlan").gameObject;
			m_ExternalPlan = menuGroup.FindChild("ExternalPlan").gameObject;


			m_MakeMovie.GetComponent<Button>().onClick.AddListener( delegate { Debug.Log("set!"); SendEvent(EVT_TYPE.EVT_TYPE_MAKEMOVIE); } );


			m_DropMenu.SetActive(false);
		}
		
		m_Menu.GetComponent<Button>().onClick.AddListener( delegate { m_DropMenu.SetActive(!m_DropMenu.activeInHierarchy); });
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
