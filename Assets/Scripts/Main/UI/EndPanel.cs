using TMPro;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
	[SerializeField] private TMP_Text winOrLose;
	[SerializeField] private TMP_Text retryButton;
	[SerializeField] private TMP_Text amount;

	public void Play(int amountValue)
	{
		gameObject.SetActive(true);
		winOrLose.text = amountValue == 0 ? "YOU LOSE" : "YOU WIN!";
		retryButton.text = amountValue == 0 ? "RETRY" : "NEXT LEVEL";
		amount.text = "+" + amountValue.ToString();
	}
}
