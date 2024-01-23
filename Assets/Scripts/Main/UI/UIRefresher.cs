using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRefresher : MonoBehaviour
{
	[SerializeField] private TMP_Text level;
	[SerializeField] private Image fill;
	[SerializeField] private List<Image> lifes;

	private void Start()
	{
		level.text = $"LEVEL {PlayerDataReader.CustomData.m_levelProgress}";
		Fill(0f);
		RefreshLifes(PlayerDataReader.CustomData.m_healthUpgrade);
	}

	public void Fill(float value)
	{
		fill.fillAmount = value;
	}

	public void RefreshLifes(int value)
	{
		lifes.ForEach(x => x.enabled = false);
		for (int i = 0; i < value; i++)
		{
			lifes[i].enabled = true;
		}
	}
}
