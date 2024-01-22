using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{
	[SerializeField] private SpawnBase prefab;
	[SerializeField] private List<SpawnBase> startObjects;
	private List<SpawnBase> currentObjects;

	private void Start()
	{
		currentObjects = startObjects;
	}

	public SpawnBase Pull()
	{
		var avaliable = currentObjects.FirstOrDefault(x => x.Avaliable);

		if (avaliable != null)
		{
			avaliable.Pull();
			return avaliable;
		}
		else
		{
			var newObject = Instantiate(prefab, transform);
			newObject.Pull();
			currentObjects.Add(newObject);
			return newObject;
		}
	}
}
