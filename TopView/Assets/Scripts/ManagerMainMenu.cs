using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMainMenu : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
