using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] private Vector2 speeds;
	private float speed;

	private void Start()
	{
		speed = Random.Range(speeds.x, speeds.y);
	}

	private void Update()
	{
		var euler = transform.eulerAngles;
		euler.z += speed * Time.deltaTime;
		transform.eulerAngles = euler;
	}
}
