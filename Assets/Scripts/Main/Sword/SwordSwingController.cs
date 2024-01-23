using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class SwordSwingController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private HingeJoint2D hingeJoint2D;
	[SerializeField] private float maxThrowMagnitude;
	private Vector3 currentDifference;
	private Vector3 prevFingerPosition;
	private Vector3 currentFingerPosition;
	private Vector3 lastShootingVelocity;
	private Vector3 currentFingerPositionDifference;
	private Finger currentFinger;
	private Vector2 screenSize;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		screenSize = Vector2.one.GetSize();
		hingeJoint2D.enabled = false;
	}

	public void Enable()
	{
		rigid.constraints = RigidbodyConstraints2D.None;
	}

	public void Disable()
	{
		rigid.constraints = RigidbodyConstraints2D.FreezeAll;
		rigid.velocity = Vector2.zero;
	}

	private void Update()
	{
		if (currentFinger == null) return;

		currentFingerPositionDifference = currentFingerPosition - prevFingerPosition;
		prevFingerPosition = currentFingerPosition;
		currentFingerPosition = currentFinger.screenPosition.ScreenToWorldPoint();
		lastShootingVelocity = currentFingerPositionDifference / Time.deltaTime;
	}

	public void StartSwing(Finger finger)
	{
		currentFinger = finger;
		currentFingerPosition = currentFinger.screenPosition.ScreenToWorldPoint();
		prevFingerPosition = currentFingerPosition;

		var fingerPosition = finger.screenPosition.ScreenToWorldPoint();
		hingeJoint2D.anchor = transform.InverseTransformPoint(fingerPosition);
		hingeJoint2D.enabled = true;
		Touch.onFingerMove += OnFingerMoveHandler;
		Touch.onFingerUp += OnFingerUpHandler;
	}

	private void OnFingerMoveHandler(Finger finger)
	{
		Vector3 fingerPosition = finger.screenPosition.ScreenToWorldPoint();
		currentDifference = transform.position - currentFingerPosition;
		transform.position = fingerPosition + currentDifference;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.transform.position.y < -screenSize.y)
		{
			Release();
		}
	}

	private void OnFingerUpHandler(Finger finger)
	{
		ReleaseWithSpeed();
	}

	private void Release()
	{
		hingeJoint2D.enabled = false;
		Touch.onFingerMove -= OnFingerMoveHandler;
		Touch.onFingerUp -= OnFingerUpHandler;
	}

	private void ReleaseWithSpeed()
	{
		Release();

		if (lastShootingVelocity.magnitude > maxThrowMagnitude)
		{
			var factor = lastShootingVelocity.magnitude / maxThrowMagnitude;
			rigid.velocity = lastShootingVelocity / factor;
		}
		else
		{
			rigid.velocity = lastShootingVelocity;
		}
	}

	private void OnDestroy()
	{
		Touch.onFingerMove -= OnFingerMoveHandler;
		Touch.onFingerUp -= OnFingerUpHandler;
	}
}
