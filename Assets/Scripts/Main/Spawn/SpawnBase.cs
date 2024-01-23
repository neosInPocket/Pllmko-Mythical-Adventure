using UnityEngine;

public abstract class SpawnBase : MonoBehaviour
{
	[SerializeField] private Vector2 verticalSpeeds;
	[SerializeField] private new Rigidbody2D rigidbody2D;
	private float verticalSpeed;

	public bool Avaliable => gameObject.activeSelf;

	private void Update()
	{
		rigidbody2D.velocity = Vector2.down * verticalSpeed;
	}

	public void Pull()
	{
		InitializePull();

		verticalSpeed = Random.Range(verticalSpeeds.x, verticalSpeeds.y);
		gameObject.SetActive(true);
	}

	public abstract void Destroy();

	protected abstract void InitializePull();
}
