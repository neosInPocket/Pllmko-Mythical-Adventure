using System;
using UnityEngine;

public class CountPanel : MonoBehaviour
{
	private Action m_onEnd;

	public void Count(Action onEnd)
	{
		m_onEnd = onEnd;
		gameObject.SetActive(true);
	}

	public void OnEnd()
	{
		m_onEnd();
		gameObject.SetActive(false);
	}
}
