using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    
    private void Awake()
    {
        Application.targetFrameRate = -1;

        continueButton.SetActive(SaveManager.Instance.HasSaves());
    }

    public void NewGame()
    {
        SaveManager.Instance.DeleteAllKeys();
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
