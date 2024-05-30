using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject menuDeath;

    public ReactiveProperty<int> CoinCount { get; private set; } = new ReactiveProperty<int>(0);
    public ReactiveCommand IncrementCoinCountCommand { get; private set; }
    public ReactiveProperty<int> Health { get; private set; } = new ReactiveProperty<int>(3);

    private void Awake()
    {
        Time.timeScale = 1.0f;
        menuDeath.SetActive(false);
        IncrementCoinCountCommand = new ReactiveCommand();
        IncrementCoinCountCommand.Subscribe(_ => CoinCount.Value++);
    }

    public void DecreaseHealth()
    {
        if (Health.Value > 0)
        {
            Health.Value--;
            if (Health.Value == 0)
            {
                OnPlayerDeath();
            }
        }
    }

    public void OnPlayerDeath()
    {
        menuDeath.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
