using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{
    [SerializeField] private string currentWord;
    [SerializeField] private TextMeshPro tmp;

    public string CurrentWord
    {
        get { return currentWord; }
        set { currentWord = value; }
    }

    private void Start()
    {
        tmp.text = currentWord;
    }

    private void Update()
    {
        if (currentWord == null)
        {
            tmp.text = "";
            Destroy(gameObject);
        }
    }
}
