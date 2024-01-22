using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GrabInput : MonoBehaviour
{
	public bool Active
	{
		get => active;
		set
		{
			if (value)
			{
				Touch.onFingerDown += OnFingerDownHandler;
			}
			else
			{
				Touch.onFingerDown -= OnFingerDownHandler;
			}
		}
	}
	private bool active;

	private void Start()
	{
		Active = true;
	}

	private void OnFingerDownHandler(Finger finger)
	{
		var fingerPosition = finger.screenPosition.ScreenToWorldPoint();
		RaycastHit2D raycast = Physics2D.Raycast(fingerPosition, Vector3.forward);

		if (raycast.collider != null)
		{
			if (raycast.collider.TryGetComponent<SwordSwingController>(out SwordSwingController sword))
			{
				sword.StartSwing(finger);
			}
		}
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnFingerDownHandler;
	}
}
