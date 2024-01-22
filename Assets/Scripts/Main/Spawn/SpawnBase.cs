using UnityEngine;

public abstract class SpawnBase : MonoBehaviour
{
	public bool Avaliable => gameObject.activeSelf;

	public void Pull()
	{
		InitializePull();
		gameObject.SetActive(true);
	}

	public void Destroy()
	{
		InitializePop();
		gameObject.SetActive(false);
	}

	protected abstract void InitializePull();
	protected abstract void InitializePop();
}
