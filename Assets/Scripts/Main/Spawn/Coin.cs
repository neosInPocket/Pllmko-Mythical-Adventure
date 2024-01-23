using System.Collections;
using UnityEngine;

public class Coin : SpawnBase
{
	[SerializeField] private ParticleSystem popEffect;
	[SerializeField] private ParticleSystem ps;
	[SerializeField] private CircleCollider2D circleCollider;

	protected override void InitializePull()
	{
		ps.Play();
		circleCollider.enabled = true;
	}

	public override void Destroy()
	{
		StartCoroutine(PopEnumerator());
	}

	private IEnumerator PopEnumerator()
	{
		ps.Stop();
		circleCollider.enabled = false;
		popEffect.gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		popEffect.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}
}
