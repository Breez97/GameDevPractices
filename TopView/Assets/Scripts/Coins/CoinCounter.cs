using UniRx;
using UnityEngine;
using TMPro;
using UnityEditor;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCountText;
    [SerializeField] private GameObject menuWin;
    [SerializeField] private int needToCollectCoins = 7;
    private GameState gameState;

    private void Awake()
    {
        menuWin.SetActive(false);
        gameState = GetComponent<GameState>();
    }

    private void Start()
    {
        gameState.CoinCount.Subscribe(count => coinCountText.SetText($"Coins: {count}")).AddTo(this);
    }

    private void Update()
    {
        gameState.CoinCount.Subscribe(coinCount => {
            if (coinCount == needToCollectCoins)
            {
                WinGame();
            }
        }).AddTo(this);
    }

    private void WinGame()
    {
        menuWin.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
