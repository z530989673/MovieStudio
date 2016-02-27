using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExternalPlanPopUpController : MonoBehaviour {

	public RectTransform m_planSteps;
	public Button m_backButton;
	[Range(0,1)]
	public float m_switchTime = 0.5f;

	private float m_panelSize = 433;
	private float m_moveDistance = 0;
	private bool m_moving = false;

	// Use this for initialization
	void Start () {
		m_backButton.onClick.AddListener( MoveBack );
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.RightArrow) && !m_moving)
		{
			//MoveToNext();
			StartCoroutine(MoveTo(1));
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow) && !m_moving)
		{
			StartCoroutine(MoveTo(-1));
		}
	}

	void MoveToNext()
	{
		Vector3 newpos = m_planSteps.localPosition;
		newpos.x -= m_panelSize;
		m_planSteps.localPosition = newpos;
	}

	IEnumerator MoveTo(int direction)
	{
		m_moving = true;
		Vector3 newpos = m_planSteps.localPosition;
		float speed = m_panelSize/m_switchTime * direction;
		while(m_moveDistance < m_panelSize)
		{
			newpos.x -= Time.deltaTime*speed;
			m_moveDistance += Mathf.Abs( Time.deltaTime*speed );
			m_planSteps.localPosition = newpos;
			yield return null;
		}
		newpos.x += (m_moveDistance - m_panelSize) * direction;
		m_planSteps.localPosition = newpos;
		m_moveDistance = 0;
		m_moving = false;
	}

	void MoveBack()
	{
		Vector3 newpos = m_planSteps.localPosition;
		newpos.x += m_panelSize;
		m_planSteps.localPosition = newpos;
	}
}
