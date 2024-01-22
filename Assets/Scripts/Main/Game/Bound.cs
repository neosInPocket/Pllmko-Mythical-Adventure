using UnityEngine;

public class Bound : MonoBehaviour
{
	[SerializeField] private BoxCollider2D boxCollider2D;
	public Vector2 Size
	{
		get => boxCollider2D.size;
		set => boxCollider2D.size = value;
	}

}
