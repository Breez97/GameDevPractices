using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[Serializable]
public class Phrase
{
    public List<string> words;
    public int amountOfWordsToCollect;
}

public class WordsManager : MonoBehaviour
{
    [Header("Text Mesh Pro")]
    [SerializeField] private TextMeshProUGUI needToCollectText;
    [SerializeField] private TextMeshProUGUI collectingText;

    [Header("Phrases")]
    [SerializeField] private List<string> phrasesToCollect;

    [Space]

    [Header("Words on Scene Settings")]
    [SerializeField] private List<Phrase> collectingPhrases;
    [SerializeField] private List<GameObject> pointsToSpawn;
    [SerializeField] private GameObject wordPrefab;
    [SerializeField] private Sprite[] sprites;

    [Space]

    [Header("Target positions")]
    [SerializeField] private List<GameObject> pointsOfTarget;

    [Space]
    [Header("Game Manager")]
    [SerializeField] private WinLoseMenu winLoseMenu;
    [SerializeField] private LivesManager livesManager;

    [Space]
    [Header("Drop Settings")]
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private List<GameObject> pointsOfDrops;
    
    private Phrase phrase;
    private List<string> collectedWords = new List<string>();
    private List<GameObject> spawnedWords = new List<GameObject>();
    private int countCollectedWords = 0;
    private int indexCurrentCollectingPhrase = 0;

    public List<string> CollectedWords
    {
        get { return collectedWords; }
        set { collectedWords = value; }
    }

    private void Start()
    {
        MoveTarget();
        SpawnWords();
    }

    private void Update()
    {
        AddingNewWord();

        if (collectedWords.Count == phrase.amountOfWordsToCollect)
        {
            if (indexCurrentCollectingPhrase < collectingPhrases.Count - 1)
            {
                GameObject[] words = GameObject.FindGameObjectsWithTag("Word");
                foreach (GameObject word in words)
                {
                    Destroy(word);
                }

                indexCurrentCollectingPhrase += 1;

                collectedWords.Clear();
                countCollectedWords = 0;

                SpawnWords();
            }
            else
            {
                winLoseMenu.IsWin = true;
            }
        }
    }

    private void SpawnWords()
    {
        ShufflePoints(pointsToSpawn);

        SpawnDrops();

        collectingText.text = "";
        needToCollectText.text = phrasesToCollect[indexCurrentCollectingPhrase];

        if (collectingPhrases.Count > 0)
        {
            phrase = collectingPhrases[indexCurrentCollectingPhrase];
            for (int i = 0; i < phrase.words.Count; i++)
            {
                GameObject newWord = Instantiate(wordPrefab, pointsToSpawn[i].transform.position, Quaternion.identity);
                ChangeSprite(newWord);
                if (newWord != null)
                {
                    Word newWordComponent = newWord.GetComponent<Word>();
                    if (newWordComponent != null)
                    {
                        newWordComponent.CurrentWord = phrase.words[i];
                    }
                }
            }
        }
    }

    private void ShufflePoints(List<GameObject> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            GameObject temp = points[i];
            int randomIndex = UnityEngine.Random.Range(i, points.Count);
            points[i] = points[randomIndex];
            points[randomIndex] = temp;
        }
    }

    private void ShuffleSpawnedWords()
    {
        ShufflePoints(pointsToSpawn);

        SpawnDrops();

        for (int i = 0; i < spawnedWords.Count; i++)
        {
            spawnedWords[i].transform.position = new Vector2(pointsToSpawn[i].transform.position.x, pointsToSpawn[i].transform.position.y);
            ChangeSprite(spawnedWords[i]);
        }

        spawnedWords.Clear();
    }

    private void AddingNewWord()
    {
        if (collectedWords.Count != countCollectedWords)
        {
            GameObject[] words = GameObject.FindGameObjectsWithTag("Word");

            if (collectedWords[countCollectedWords].Equals(collectingPhrases[indexCurrentCollectingPhrase].words[countCollectedWords]))
            {
                ShufflePoints(pointsOfTarget);
                MoveTarget();

                collectingText.text = string.Join(" ", collectedWords);
                countCollectedWords = collectedWords.Count;

                spawnedWords.AddRange(words);

                ShuffleSpawnedWords();
            }
            else
            {
                livesManager.Lives -= 1;

                collectedWords.Clear();
                countCollectedWords = 0;

                foreach (GameObject word in words)
                {
                    Destroy(word);
                }

                SpawnWords();
            }
        }
    }

    private void MoveTarget()
    {
        int index = UnityEngine.Random.Range(0, pointsOfTarget.Count);
        gameObject.transform.position = new Vector2(pointsOfTarget[index].transform.position.x, pointsOfTarget[index].transform.position.y);
    }

    private void ChangeSprite(GameObject objectToChange)
    {
        SpriteRenderer renderer = objectToChange.GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
    }

    private void SpawnDrops()
    {
        GameObject[] currentDrops = GameObject.FindGameObjectsWithTag("Drop");
        if (currentDrops.Length > 0)
        {
            foreach (GameObject currentDrop in currentDrops)
            {
                Destroy(currentDrop);
            }
        }

        ShufflePoints(pointsOfDrops);
        int amountOfDrops = UnityEngine.Random.Range(1, pointsOfDrops.Count);
        for (int i = 0; i < amountOfDrops; i++)
        {
            Instantiate(dropPrefab, pointsOfDrops[i].transform.position, Quaternion.identity);
        }
    }
}
