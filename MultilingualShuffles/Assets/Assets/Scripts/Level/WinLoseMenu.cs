using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseMenu : MonoBehaviour
{
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timer = 60.0f;

    [Space]

    [SerializeField] private WordsManager wordsManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject volume;

    private bool isWin = false;
    private bool isLose = false;

    public bool IsWin
    {
        set { isWin = value; }
        get { return isWin; }
    }

    public bool IsLose
    {
        set { isLose = value; }
        get { return isLose; }
    }

    private void Start()
    {
        timerText.text = timer.ToString();
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
    }

    private void Update()
    {
        Timer();

        if (isLose)
        {
            SetWinLoseMenu(loseMenu);
        }

        if (isWin)
        {
            SetWinLoseMenu(winMenu);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void Timer()
    {
        timer -= Time.deltaTime;

        int secondsMinutes = Mathf.RoundToInt(timer);
        int seconds = secondsMinutes % 60;
        int minutes = secondsMinutes / 60;
        string resultTimerString = minutes.ToString() + ":";
        if (seconds < 10)
        {
            resultTimerString += "0";
        }
        resultTimerString += seconds.ToString();
        timerText.text = resultTimerString;

        if (timer <= 0)
        {
            isLose = true;
        }
    }

    private void SetWinLoseMenu(GameObject winLoseMenu)
    {
        winLoseMenu.SetActive(true);
        gameManager.CanPause = false;
        Time.timeScale = 0.0f;
        volume.SetActive(true);
    }
}
