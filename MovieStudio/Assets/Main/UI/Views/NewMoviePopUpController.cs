using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewMoviePopUpController : ViewController {

	void Awake()
	{
		for(int i = 0;i<transform.childCount;i++)
		{
			GameObject go = transform.GetChild(i).gameObject;
			go.SetActive(true);
			Button [] buttons = go.GetComponentsInChildren<Button>();
			for(int j = 0;j<buttons.Length;j++)
			{
				SetButton(buttons[j], i);
			}
		}
	}

	void OnEnable()
	{
		// close all except first one
		for(int i = 0;i<transform.childCount;i++)
		{
			transform.GetChild(i).gameObject.SetActive(i == 0);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void SetButton(Button button, int childIndex)
	{
		button.onClick.AddListener( ()=>{
			transform.GetChild(childIndex).gameObject.SetActive(false);
			if(childIndex+1 == transform.childCount)
			{
				// may send different event type in future: make movie done?
				SendEvent(EVT_TYPE.EVT_TYPE_MAKEMOVIE);
				Debug.Log("Movie Make!");
			}
			else
			{
				transform.GetChild(childIndex+1).gameObject.SetActive(true);
			}
		});
	}
}
