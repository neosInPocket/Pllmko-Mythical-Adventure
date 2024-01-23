using System.Collections;
using UnityEngine;

public class ObjectsSpawnHandler : MonoBehaviour
{
	[SerializeField] private SpawnPool ballsPool;
	[SerializeField] private SpawnPool spikesPool;
	[SerializeField] private Vector2 delayRange;
	[SerializeField] private float spawnWidth;
	[Range(0, 1f)]
	[SerializeField] private float verticalSpawn;
	private bool isActing;
	public bool isEnabled { get; set; }

	private Vector2 screenSize;
	private void Start()
	{
		screenSize = Vector2.one.GetSize();
	}

	private void Update()
	{
		if (!isEnabled) return;
		if (isActing) return;

		StartCoroutine(Spawn());
	}

	public void Disable()
	{
		StopAllCoroutines();
		isEnabled = false;
		isActing = false;
	}

	private IEnumerator Spawn()
	{
		isActing = true;
		SpawnBase obj = default;
		Vector2 position;
		position.x = Random.Range(-screenSize.x + spawnWidth, screenSize.x - spawnWidth);
		position.y = 2 * screenSize.y * verticalSpawn - screenSize.y;

		if (Random.Range(0, 2) == 0)
		{
			obj = ballsPool.Pull();
		}
		else
		{
			obj = spikesPool.Pull();
		}

		obj.transform.position = position;
		yield return new WaitForSeconds(Random.Range(delayRange.x, delayRange.y));
		isActing = false;
	}
}
