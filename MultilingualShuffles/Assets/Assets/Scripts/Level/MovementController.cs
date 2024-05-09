using System;
using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected readonly float moveSpeed = 10.0f;
    protected bool isMoving = false;

    private bool canMoveUp = true;
    private bool canMoveDown = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    public bool CanMoveUp
    {
        get { return canMoveUp; }
        set { canMoveUp = value; }
    }

    public bool CanMoveDown
    {
        get { return canMoveDown; }
        set { canMoveDown = value; }
    }

    public bool CanMoveLeft
    {
        get { return canMoveLeft; }
        set { canMoveLeft = value; }
    }

    public bool CanMoveRight
    {
        get { return canMoveRight; }
        set { canMoveRight = value; }
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        if (!isMoving)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && canMoveUp)
            {
                StartCoroutine(MoveToPosition(Vector2.up));
            }
            else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && canMoveDown)
            {
                StartCoroutine(MoveToPosition(Vector2.down));
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && canMoveLeft)
            {
                StartCoroutine(MoveToPosition(Vector2.left));
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && canMoveRight)
            {
                StartCoroutine(MoveToPosition(Vector2.right));
            }
        }
    }

    protected IEnumerator MoveToPosition(Vector2 direction)
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
}
