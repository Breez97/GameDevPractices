using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DialogManager dialogManager;

    private Rigidbody2D rb;
    private readonly float moveSpeed = 10.0f;
    private bool isMoving = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (!isMoving && !dialogManager.IsDialogStarted)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !CheckCollision(Vector2.up))
            {
                StartCoroutine(MoveToNewPosition(Vector2.up));
            }
            else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !CheckCollision(Vector2.down))
            {
                StartCoroutine(MoveToNewPosition(Vector2.down));
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !CheckCollision(Vector2.left))
            {
                StartCoroutine(MoveToNewPosition(Vector2.left));
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !CheckCollision(Vector2.right))
            {
                StartCoroutine(MoveToNewPosition(Vector2.right));
            }
        }
    }

    IEnumerator MoveToNewPosition(Vector2 direction)
    {
        isMoving = true;
        Vector2 currentPosition = rb.position;
        Vector2 targetPosition = rb.position + direction;
        float distance = Vector2.Distance(currentPosition, targetPosition);
        float elapsedTime = 0f;
        while (elapsedTime < (distance / moveSpeed))
        {
            rb.MovePosition(Vector2.Lerp(currentPosition, targetPosition, (elapsedTime / (distance / moveSpeed))));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rb.MovePosition(targetPosition);
        targetPosition = new Vector2(Mathf.Round(targetPosition.x * 2f) / 2f, Mathf.Round(targetPosition.y * 2f) / 2f);
        rb.MovePosition(targetPosition);
        isMoving = false;
    }

    public bool CheckCollision(Vector2 direction)
    {
        Vector2 targetPosition = rb.position + direction;
        Collider2D[] colliders = Physics2D.OverlapPointAll(targetPosition);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Character"))
            {
                return true;
            }
        }
        return false;
    }

}
