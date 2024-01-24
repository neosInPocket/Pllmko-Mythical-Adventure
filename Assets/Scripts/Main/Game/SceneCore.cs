using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCore : MonoBehaviour
{
	[SerializeField] private SwordsContainer swordsContainer;
	[SerializeField] private ObjectsSpawnHandler objectsSpawnHandler;
	[SerializeField] private GrabInput grabInput;
	[SerializeField] private UIRefresher uIRefresher;
	[SerializeField] private CountPanel countPanel;
	[SerializeField] private EndPanel endPanel;
	[SerializeField] private int additivePoints;
	[SerializeField] private PlayerDataReader playerDataReader;
	private Sword sword;
	private int currentPoints;
	private int maxPoints;
	private int maxRewards;
	private int currentLevel;

	private void Start()
	{
		currentLevel = PlayerDataReader.CustomData.m_levelProgress;
		currentPoints = 0;
		maxPoints = MaxScore();
		maxRewards = Reward();

		sword = swordsContainer.Initialize();
		sword.RewardCollected += OnRewardCollected;
		sword.Spike += OnDamaged;
		countPanel.Count(OnCountCompleted);
	}

	private void OnCountCompleted()
	{
		grabInput.Active = true;
		sword.Enable();
		objectsSpawnHandler.isEnabled = true;
	}

	private void OnRewardCollected()
	{
		currentPoints += additivePoints;
		if (currentPoints >= maxPoints)
		{
			currentPoints = maxPoints;

			grabInput.Active = false;
			sword.Disable();
			objectsSpawnHandler.isEnabled = false;
			endPanel.Play(maxRewards);
			sword.RewardCollected -= OnRewardCollected;
			sword.Spike -= OnDamaged;

			PlayerDataReader.CustomData.m_playerCoins += maxRewards;
			PlayerDataReader.CustomData.m_levelProgress++;
			playerDataReader.SaveData();
		}

		uIRefresher.Fill((float)currentPoints / (float)maxPoints);
	}

	private void OnDamaged(int healthLeft)
	{
		if (healthLeft <= 0)
		{
			grabInput.Active = false;
			sword.Disable();
			objectsSpawnHandler.isEnabled = false;
			endPanel.Play(0);
			sword.RewardCollected -= OnRewardCollected;
			sword.Spike -= OnDamaged;
		}

		uIRefresher.RefreshLifes(healthLeft);
	}

	public void Replay()
	{
		SceneManager.LoadScene("SwordGameScene");
	}

	public void LoadFirstScene()
	{

		SceneManager.LoadScene("MenuScreen");
	}

	private void OnDestroy()
	{
		sword.RewardCollected += OnRewardCollected;
		sword.Spike += OnDamaged;
	}
	private int MaxScore() => (int)(8 * Mathf.Log(Mathf.Sqrt(currentLevel) + 2));
	private int Reward() => (int)(13 * Mathf.Log(Mathf.Pow(currentLevel, 2) + 2) + 25);
}
