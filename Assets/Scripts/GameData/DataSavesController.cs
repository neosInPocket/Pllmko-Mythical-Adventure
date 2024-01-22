using UnityEngine;

public class DataSavesController : MonoBehaviour
{
	[SerializeField] private PlayerDataReader playerDataReader;
	[SerializeField] private VolumeDataReader volumeDataReader;
	[SerializeField] private bool refreshSettings;

	private void Awake()
	{
		playerDataReader.LoadOnAwake(refreshSettings);
		volumeDataReader.LoadOnAwake(refreshSettings);
	}
}
