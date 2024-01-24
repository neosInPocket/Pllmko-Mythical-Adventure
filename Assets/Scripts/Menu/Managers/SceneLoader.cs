using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("SwordGameScene");
    }
}
