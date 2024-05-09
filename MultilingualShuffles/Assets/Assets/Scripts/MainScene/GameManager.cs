using I2.Loc;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject volume;
    [SerializeField] private Image startImage;
    [SerializeField] private GameObject startOfScene;
    
    private float fadeDuration = 1.5f;
    private bool isPaused = false;
    private bool canPause = true;

    public bool CanPause
    {
        set { canPause = value; }
        get { return canPause; }
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        volume.SetActive(false);
        startOfScene.SetActive(true);
        StartCoroutine(FadeImage());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        volume.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame() 
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        volume.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeImage()
    {
        Color startColor = startImage.color;
        float alpha;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            startImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        startImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        startOfScene.SetActive(false);
    }
}
