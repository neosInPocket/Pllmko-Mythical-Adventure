using System;
using System.Collections;
using System.ComponentModel;
using UnityEditor.Search;
using UnityEngine;

public class Sword : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private GameObject destroyEffect;
	[SerializeField] private new Rigidbody2D rigidbody2D;
	[SerializeField] private int blinkAmount;
	[SerializeField] private float blinkDelay;
	[SerializeField] private Vector2 spawnPosition;
	[SerializeField] private SwordSwingController swordSwing;
	[Range(0, 1f)]
	[SerializeField] private float spriteRendererOpacity;
	[SerializeField] private float[] sizeValues;
	public Action RewardCollected { get; set; }
	public Action<int> Spike { get; set; }
	private int currentHealth;
	private bool invincible;

	private void Start()
	{
		currentHealth = PlayerDataReader.CustomData.m_healthUpgrade;
		swordSwing.Disable();
		float sizeValue = sizeValues[PlayerDataReader.CustomData.m_swordSize];
		transform.localScale = new Vector3(sizeValue, sizeValue, sizeValue);
	}

	public void Enable()
	{
		swordSwing.Enable();
	}

	public void Disable()
	{
		swordSwing.Disable();
	}

	public void Initialize()
	{
		transform.position = spawnPosition;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Coin>(out Coin coin))
		{
			RewardCollected?.Invoke();
			coin.Destroy();
			return;
		}

		if (collider.TryGetComponent<Spike>(out Spike spike))
		{
			if (invincible) return;

			currentHealth--;
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				StartCoroutine(Lose());
			}
			else
			{
				invincible = true;
				StartCoroutine(Blink());
			}

			Spike?.Invoke(currentHealth);
		}
	}

	private IEnumerator Blink()
	{
		var color = spriteRenderer.color;

		for (int i = 0; i < blinkAmount; i++)
		{
			color.a = spriteRendererOpacity;
			spriteRenderer.color = color;
			yield return new WaitForSeconds(blinkDelay);

			color.a = 1f;
			spriteRenderer.color = color;
			yield return new WaitForSeconds(blinkDelay);
		}

		invincible = false;
	}

	private IEnumerator Lose()
	{
		rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
		spriteRenderer.enabled = false;
		destroyEffect.gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		destroyEffect.gameObject.SetActive(false);
	}
}
