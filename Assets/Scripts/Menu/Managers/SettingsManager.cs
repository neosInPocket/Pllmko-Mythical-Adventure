using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider sfxSlider;
	[SerializeField] private VolumeDataReader volumeDataReader;
	[SerializeField] private Image musicOnImage;
	[SerializeField] private Image sfxOnImage;
	private SoundManager mainController;

	private void Start()
	{
		mainController = FindObjectsOfType<SoundManager>().FirstOrDefault();
		RefershSliders();
	}

	private void RefershSliders()
	{
		musicSlider.value = VolumeDataReader.CustomData.m_musicVolume;
		sfxSlider.value = VolumeDataReader.CustomData.m_sfxVolume;
		musicOnImage.enabled = musicSlider.value != 0;
		sfxOnImage.enabled = sfxSlider.value != 0;
	}

	public void SetMusicVolume(float value)
	{
		VolumeDataReader.CustomData.m_musicVolume = value;
		mainController.SetMusicValue(value);
		musicOnImage.enabled = value != 0;
	}

	public void SetSFXVolume(float value)
	{
		VolumeDataReader.CustomData.m_sfxVolume = value;
		sfxOnImage.enabled = value != 0;
	}

	public void SaveVolume()
	{
		volumeDataReader.SaveData();
	}

	public void DisableMusic()
	{
		musicSlider.value = 0;
	}

	public void DisableSfx()
	{
		sfxSlider.value = 0;
	}
}
