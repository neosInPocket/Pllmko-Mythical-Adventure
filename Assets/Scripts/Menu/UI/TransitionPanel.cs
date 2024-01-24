using UnityEngine;

public class TransitionPanel : MonoBehaviour
{
	[SerializeField] private Transform startScreen;
	[SerializeField] private Animator animator;
	private Transform actualScreen;
	private Transform switchScreen;

	private void Start()
	{
		actualScreen = startScreen;
	}

	public void Transit(Transform target)
	{
		if (target == actualScreen) return;
		switchScreen = target;
		animator.SetTrigger("fadein");
	}

	public void OnTransitEnd()
	{
		switchScreen.gameObject.SetActive(true);
		actualScreen.gameObject.SetActive(false);
		actualScreen = switchScreen;
	}
}

