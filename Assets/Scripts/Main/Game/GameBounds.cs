using UnityEngine;

public class GameBounds : MonoBehaviour
{
	[SerializeField] private Bound left;
	[SerializeField] private Bound right;
	[SerializeField] private Bound down;
	[SerializeField] private Bound up;
	[SerializeField] private float width;

	private void Start()
	{
		var screenSize = Vector2.zero.GetSize();
		left.transform.position = new Vector2(-screenSize.x - width / 2, 0);
		right.transform.position = new Vector2(screenSize.x + width / 2, 0);
		up.transform.position = new Vector2(0, screenSize.y + width / 2);
		down.transform.position = new Vector2(0, -screenSize.y - width / 2);

		left.Size = new Vector2(width, screenSize.y * 2);
		right.Size = new Vector2(width, screenSize.y * 2);
		up.Size = new Vector2(screenSize.x * 2, width);
		down.Size = new Vector2(screenSize.x * 2, width);
	}
}
