using UniRx;
using UnityEngine;
using TMPro;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    private GameState gameState;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
        gameState.Health.Subscribe(health => healthText.SetText($"Health: {health}")).AddTo(this);
    }
}
