using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinsManager : MonoBehaviour
{
	[SerializeField] private string[] names;
	[SerializeField] private Material[] materials;
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private Image swordHolder;
	[SerializeField] private TMP_Text swordName;
	[SerializeField] private Button leftButton;
	[SerializeField] private Button rightButton;
	[SerializeField] private Button middleButton;
	[SerializeField] private TMP_Text buttonStatus;
	[SerializeField] private TMP_Text swordStatus;
	[SerializeField] private PlayerDataReader dataReader;
	[SerializeField] private StoreManager storeManager;
	[SerializeField] private GameObject costContainer;
	[SerializeField] private TMP_Text costText;
	private int currentSwordIndex;
	private int swordCost => currentSwordIndex * 20;
	private SwordStatus currentSwordStatus;

	private void Start()
	{
		SelectSword(PlayerDataReader.CustomData.m_swordIndex);
	}

	public void Next()
	{
		SelectSword(currentSwordIndex + 1);
	}

	public void Prev()
	{
		SelectSword(currentSwordIndex - 1);
	}

	public void RefreshControls()
	{
		leftButton.interactable = true;
		rightButton.interactable = false;

		if (currentSwordIndex <= 0)
		{
			leftButton.interactable = false;
		}
		else
		{
			leftButton.interactable = true;
		}

		if (currentSwordIndex >= 4)
		{
			rightButton.interactable = false;
		}
		else
		{
			rightButton.interactable = true;
		}

		middleButton.interactable = true;
		costContainer.gameObject.SetActive(false);
		buttonStatus.gameObject.SetActive(true);

		if (PlayerDataReader.CustomData.m_swordPurchaseHistory[currentSwordIndex])
		{
			if (PlayerDataReader.CustomData.m_swordIndex == currentSwordIndex)
			{
				swordStatus.text = "SELECTED";
				swordStatus.color = Color.white;
				buttonStatus.text = "SELECT";
				middleButton.interactable = false;
			}
			else
			{
				swordStatus.text = "PURCHASED";
				swordStatus.color = Color.green;
				buttonStatus.text = "SELECT";
				middleButton.interactable = true;
				currentSwordStatus = SwordStatus.Purchased;
			}
		}
		else
		{
			if (PlayerDataReader.CustomData.m_playerCoins < swordCost)
			{
				swordStatus.text = "NOT ENOUGH GOLD ORE";
				swordStatus.color = Color.red;
				costContainer.gameObject.SetActive(true);
				buttonStatus.gameObject.SetActive(false);
				costText.text = swordCost.ToString();
				middleButton.interactable = false;
			}
			else
			{
				swordStatus.text = "AVALIABLE";
				swordStatus.color = Color.white;
				costContainer.gameObject.SetActive(true);
				buttonStatus.gameObject.SetActive(false);
				costText.text = swordCost.ToString();
				middleButton.interactable = true;
				currentSwordStatus = SwordStatus.ReadyToBuy;
			}

		}
	}

	public void OnBuyButtonClick()
	{
		if (currentSwordStatus == SwordStatus.ReadyToBuy)
		{
			PlayerDataReader.CustomData.m_playerCoins -= swordCost;
			PlayerDataReader.CustomData.m_swordPurchaseHistory[currentSwordIndex] = true;
			PlayerDataReader.CustomData.m_swordIndex = currentSwordIndex;
			dataReader.SaveData();
			storeManager.Refresh();
			RefreshControls();
		}
		else
		{
			PlayerDataReader.CustomData.m_swordIndex = currentSwordIndex;
			dataReader.SaveData();
			storeManager.Refresh();
			RefreshControls();
		}
	}

	private void SelectSword(int index)
	{
		currentSwordIndex = index;
		swordHolder.sprite = sprites[index];
		swordName.text = names[index];
		swordHolder.material = materials[index];
		RefreshControls();
	}
}

public enum SwordStatus
{
	ReadyToBuy,
	Purchased
}
