using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	public AudioSource Audio => audioSource;

	private void Awake()
	{
		var avaliableControllers = GameObject.FindObjectsOfType<SoundManager>();
		var living = avaliableControllers.FirstOrDefault(x => x.gameObject.scene.name == "DontDestroyOnLoad");

		if (living != null && living != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		audioSource.volume = VolumeDataReader.CustomData.m_musicVolume;
	}

	public void SetMusicValue(float volume)
	{
		audioSource.volume = volume;
	}
}
