using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCore : MonoBehaviour
{
	[SerializeField] private SwordsContainer swordsContainer;
	[SerializeField] private ObjectsSpawnHandler objectsSpawnHandler;
	[SerializeField] private GrabInput grabInput;
	[SerializeField] private UIRefresher uIRefresher;
	[SerializeField] private CountPanel countPanel;
	private Sword sword;

	private void Start()
	{
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

	}

	private void OnDamaged(int healthLeft)
	{

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
}
