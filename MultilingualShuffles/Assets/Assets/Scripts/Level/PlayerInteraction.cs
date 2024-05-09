using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private WordsManager wordsManager;
    [SerializeField] private LivesManager livesManager;

    private Word word = null;
    
    private bool canGrabWord = false;
    private bool canPutGrabbedWord = false;

    private void Start()
    {
        textMeshPro.text = "";
    }

    private void Update()
    {
        if (canGrabWord && word != null && textMeshPro.text == "" && Input.GetKeyDown(KeyCode.E))
        {
            textMeshPro.text = word.CurrentWord;
            word.CurrentWord = null;
        }

        if (canPutGrabbedWord && textMeshPro.text != "" && Input.GetKeyDown(KeyCode.E))
        {
            wordsManager.CollectedWords.Add(textMeshPro.text);
            textMeshPro.text = "";
            canPutGrabbedWord = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.name == "Interaction")
        {
            if (collision.CompareTag("Word"))
            {
                word = collision.GetComponent<Word>();
                canGrabWord = true;
            }

            if (collision.CompareTag("Target"))
            {
                canPutGrabbedWord = true;
            }

            if (collision.CompareTag("Drop"))
            {
                livesManager.Lives -= 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.name == "Interaction")
        {
            if (collision.CompareTag("Word"))
            {
                word = null;
                canGrabWord = false;
            }

            if (collision.CompareTag("Target"))
            {
                canPutGrabbedWord = false;
            }
        }
    }
}
