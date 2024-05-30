using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeSelf) ClosePause();
            else OpenPause();
        }
    }

    private void OpenPause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePause()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SaveManager.Instance.DeleteAllKeys();
        
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        SaveManager.Instance.Save();
        
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ExitNoSave()
    {
        SaveManager.Instance.DeleteAllKeys();
        
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void OnApplicationQuit()
    {
        SaveManager.Instance.Save();
    }
}
