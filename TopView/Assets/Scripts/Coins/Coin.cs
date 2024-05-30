using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameState gameState;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameState.IncrementCoinCountCommand.Execute();
            Destroy(gameObject);
        }
    }
}
