using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	[SerializeField] private PlayerDataReader data;

	public void LoadLevel()
	{
		if (PlayerDataReader.CustomData.m_firstGamePlaying)
		{
			PlayerDataReader.CustomData.m_firstGamePlaying = false;
			data.SaveData();
			SceneManager.LoadScene("TutorScene");
		}
		else
		{
			SceneManager.LoadScene("SwordGameScene");
		}
	}
}
