using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TutorCore : MonoBehaviour
{
	[SerializeField] private Sword sword;
	[SerializeField] private GrabInput grabInput;
	[SerializeField] private CountPanel countPanel;
	[SerializeField] private TMP_Text characterText;
	[SerializeField] private GameObject tutorialWindow;
	[SerializeField] private List<string> strings;
	[SerializeField] private UIRefresher refresher;
	[SerializeField] private ObjectsSpawnHandler spawner;
	[SerializeField] private int tutorScore;
	private int currentScore;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerDown += Quote1;
	}

	private void Quote1(Finger finger)
	{
		Touch.onFingerDown -= Quote1;
		Touch.onFingerDown += Quote2;

		characterText.text = strings[0];
	}

	private void Quote2(Finger finger)
	{
		Touch.onFingerDown -= Quote2;
		Touch.onFingerDown += Quote3;

		characterText.text = strings[1];
	}

	private void Quote3(Finger finger)
	{
		Touch.onFingerDown -= Quote3;
		refresher.Fill(0f);
		currentScore = 0;

		tutorialWindow.gameObject.SetActive(false);
		spawner.Reset();
		countPanel.Count(OnCountEnd);
		sword.Reset();
	}

	private void OnCountEnd()
	{
		sword.Enable();
		grabInput.Active = true;
		spawner.isEnabled = true;

		sword.RewardCollected += OnPLayerCoinCollected;
		sword.Spike += OnPlayerSpike;
	}

	private void OnPLayerCoinCollected()
	{
		currentScore++;
		if (currentScore >= tutorScore)
		{
			sword.Disable();
			grabInput.Active = false;
			spawner.Reset();
			Quote4();
		}

		refresher.Fill((float)currentScore / (float)tutorScore);
	}

	private void Quote4()
	{
		Touch.onFingerDown += Quote5;

		tutorialWindow.SetActive(true);
		characterText.text = strings[2];
	}

	private void Quote5(Finger finger)
	{
		Touch.onFingerDown -= Quote5;
		Touch.onFingerDown += Quote6;

		characterText.text = strings[3];
	}

	private void Quote6(Finger finger)
	{
		Touch.onFingerDown -= Quote6;

		LoadGame();
	}

	private void OnPlayerSpike(int lifes)
	{
		sword.RewardCollected -= OnPLayerCoinCollected;
		sword.Spike -= OnPlayerSpike;
		sword.Disable();
		grabInput.Active = false;
		spawner.Reset();

		tutorialWindow.SetActive(true);
		characterText.text = "LET'S TRY ONE MORE TIME!";
		Touch.onFingerDown += Quote3;
	}

	public void LoadGame()
	{
		SceneManager.LoadScene("SwordGameScene");
	}
}
