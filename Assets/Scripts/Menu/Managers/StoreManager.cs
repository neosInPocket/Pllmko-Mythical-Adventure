using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
	[SerializeField] private List<Image> healthPoints;
	[SerializeField] private List<Image> sizePoints;
	[SerializeField] private TMP_Text healthStatus;
	[SerializeField] private TMP_Text sizeStatus;
	[SerializeField] private Button healthButton;
	[SerializeField] private Button sizeButton;
	[SerializeField] private PlayerDataReader playerDataReader;
	[SerializeField] private int healthCost;
	[SerializeField] private int sizeCost;
	[SerializeField] private UIMoneyManager uIMoneyManager;
	[SerializeField] private SkinsManager skinsManager;
	private Dictionary<PurchaseType, string> strings;
	private Dictionary<PurchaseType, Color> colors;

	private void Start()
	{
		strings = new Dictionary<PurchaseType, string>
		{
			{ PurchaseType.Purchased, "PURCHASED" },
			{ PurchaseType.Avalible, "AVALIABLE" },
			{ PurchaseType.NoMoney, "NOT ENOUGH GOLD ORE" }
		};

		colors = new Dictionary<PurchaseType, Color>
		{
			{ PurchaseType.Purchased, Color.yellow },
			{ PurchaseType.Avalible, Color.green },
			{ PurchaseType.NoMoney, Color.red }
		};

		Refresh();
	}

	public void Refresh()
	{
		uIMoneyManager.Refresh();
		healthPoints.ForEach(x => x.enabled = false);
		sizePoints.ForEach(x => x.enabled = false);

		for (int i = 0; i < PlayerDataReader.CustomData.m_healthUpgrade; i++)
		{
			healthPoints[i].enabled = true;
		}

		for (int i = 0; i < PlayerDataReader.CustomData.m_swordSize; i++)
		{
			sizePoints[i].enabled = true;
		}

		bool sizeButtonEnabled = PlayerDataReader.CustomData.m_swordSize < 3 && PlayerDataReader.CustomData.m_playerCoins >= sizeCost;
		bool healthButtonEnabled = PlayerDataReader.CustomData.m_healthUpgrade < 3 && PlayerDataReader.CustomData.m_playerCoins >= healthCost;
		sizeButton.interactable = sizeButtonEnabled;
		healthButton.interactable = healthButtonEnabled;

		if (!sizeButtonEnabled)
		{
			if (PlayerDataReader.CustomData.m_swordSize >= 3)
			{
				sizeStatus.text = strings[PurchaseType.Purchased];
				sizeStatus.color = colors[PurchaseType.Purchased];
			}
			else
			{
				if (PlayerDataReader.CustomData.m_playerCoins < sizeCost)
				{
					sizeStatus.text = strings[PurchaseType.NoMoney];
					sizeStatus.color = colors[PurchaseType.NoMoney];
				}
			}
		}
		else
		{
			sizeStatus.text = strings[PurchaseType.Avalible];
			sizeStatus.color = colors[PurchaseType.Avalible];
		}

		if (!healthButtonEnabled)
		{
			if (PlayerDataReader.CustomData.m_healthUpgrade >= 3)
			{
				healthStatus.text = strings[PurchaseType.Purchased];
				healthStatus.color = colors[PurchaseType.Purchased];
			}
			else
			{
				if (PlayerDataReader.CustomData.m_playerCoins < healthCost)
				{
					healthStatus.text = strings[PurchaseType.NoMoney];
					healthStatus.color = colors[PurchaseType.NoMoney];
				}
			}
		}
		else
		{
			healthStatus.text = strings[PurchaseType.Avalible];
			healthStatus.color = colors[PurchaseType.Avalible];
		}

		skinsManager.RefreshControls();
	}

	public void BuyHealthUpgrade()
	{
		PlayerDataReader.CustomData.m_healthUpgrade++;
		PlayerDataReader.CustomData.m_playerCoins -= healthCost;
		playerDataReader.SaveData();
		Refresh();
	}

	public void BuySizeUpgrade()
	{
		PlayerDataReader.CustomData.m_swordSize++;
		PlayerDataReader.CustomData.m_playerCoins -= sizeCost;
		playerDataReader.SaveData();
		Refresh();
	}
}

public enum PurchaseType
{
	Purchased,
	NoMoney,
	Avalible
}
