using TMPro;
using UnityEngine;

public class UIMoneyManager : MonoBehaviour
{
	[SerializeField] private TMP_Text amount;

	private void Start()
	{
		Refresh();
	}

	public void Refresh()
	{
		amount.text = PlayerDataReader.CustomData.m_playerCoins.ToString();
	}
}
