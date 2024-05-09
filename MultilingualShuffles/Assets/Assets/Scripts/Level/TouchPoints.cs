using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPoints : MonoBehaviour
{
    [SerializeField] private MovementController movementController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Word"))
        {
            switch(gameObject.name)
            {
                case "up":
                    movementController.CanMoveUp = false;
                    break;
                case "down":
                    movementController.CanMoveDown = false;
                    break;
                case "left":
                    movementController.CanMoveLeft = false;
                    break;
                case "right":
                    movementController.CanMoveRight = false;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Word"))
        {
            switch (gameObject.name)
            {
                case "up":
                    movementController.CanMoveUp = true;
                    break;
                case "down":
                    movementController.CanMoveDown = true;
                    break;
                case "left":
                    movementController.CanMoveLeft = true;
                    break;
                case "right":
                    movementController.CanMoveRight = true;
                    break;
                default:
                    break;
            }
        }
    }
}
