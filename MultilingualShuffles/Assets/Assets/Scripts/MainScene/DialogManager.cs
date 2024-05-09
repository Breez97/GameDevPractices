using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [SerializeField] private float typingSpeed = 0.2f;
    [SerializeField] private Image characterIcon;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI dialogueArea;
    [SerializeField] private Animator dialogBoxAnimator;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private TextMeshProUGUI nextLevelButtonText;

    private bool linePrinted = false;
    private string previousLanguage;

    private DialogLine currentLine;

    private Queue<DialogLine> lines;

    private bool isDialogStarted = false;
    public bool IsDialogStarted => isDialogStarted;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        lines = new Queue<DialogLine>();
    }

    private void Start()
    {
        continueButton.SetActive(true);
        nextLevelButton.SetActive(false);
    }

    private void Update()
    {
        ChangeDialogText();
    }

    public void StartDialog(Dialog dialogue)
    {
        isDialogStarted = true;
        dialogBoxAnimator.Play("DialogBox");
        lines.Clear();
        foreach (DialogLine dialogLine in dialogue.dialogLines)
        {
            lines.Enqueue(dialogLine);
        }
        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 1)
        {
            continueButton.SetActive(false);
            nextLevelButton.SetActive(true);

            switch(LocalizationManager.CurrentLanguage)
            {
                case "English":
                    nextLevelButtonText.text = "Let's go!";
                    break;
                case "Russian":
                    nextLevelButtonText.text = "Начнем!";
                    break;
                case "German":
                    nextLevelButtonText.text = "Lass uns gehen!";
                    break;
                default:
                    break;
            }
            return;
        }
        currentLine = lines.Dequeue();
        characterIcon.sprite = currentLine.character.icon;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    public void StartNextLevel()
    {
        SceneManager.LoadScene("Level");
    }

    IEnumerator TypeSentence(DialogLine dialogueLine)
    {
        linePrinted = false;
        dialogueArea.text = "";
        char[] letters = { };
        string initialLanguage = LocalizationManager.CurrentLanguage;
        switch (initialLanguage)
        {
            case "English":
                letters = dialogueLine.line[0].ToCharArray();
                break;
            case "Russian":
                letters = dialogueLine.line[1].ToCharArray();
                break;
            case "German":
                letters = dialogueLine.line[2].ToCharArray();
                break;
            default:
                break;
        }
        foreach (char letter in letters)
        {
            if (initialLanguage != LocalizationManager.CurrentLanguage)
            {
                StopAllCoroutines();
                StartCoroutine(TypeSentence(dialogueLine));
                break;
            }
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        linePrinted = true;
    }

    private void ChangeDialogText()
    {
        if (currentLine != null && currentLine.character != null && currentLine.character.name != null)
        {
            switch (LocalizationManager.CurrentLanguage)
            {
                case "English":
                    buttonText.text = "Continue...";
                    characterName.text = currentLine.character.name[0];
                    break;
                case "Russian":
                    buttonText.text = "Продолжить...";
                    characterName.text = currentLine.character.name[1];
                    break;
                case "German":
                    buttonText.text = "Weiterhin...";
                    characterName.text = currentLine.character.name[0];
                    break;
                default:
                    break;
            }
        }

        string currentLanguage = LocalizationManager.CurrentLanguage;
        if (currentLanguage != previousLanguage)
        {
            previousLanguage = currentLanguage;
            if (linePrinted)
            {
                switch (currentLanguage)
                {
                    case "English":
                        dialogueArea.text = currentLine.line[0];
                        break;
                    case "Russian":
                        dialogueArea.text = currentLine.line[1];
                        break;
                    case "German":
                        dialogueArea.text = currentLine.line[2];
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void CloseDialog()
    {
        StopAllCoroutines();
        dialogBoxAnimator.Play("DialogBoxHide");
        isDialogStarted = false;
    }
}
