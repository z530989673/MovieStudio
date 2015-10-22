using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WelcomeScreenControl : MonoBehaviour {

    // Use this for initialization
	void Awake () {
        Button button = GetComponentInChildren<Button>();
        button.onClick.AddListener(delegate { UIManager.Instance.EnterGame(); } );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
