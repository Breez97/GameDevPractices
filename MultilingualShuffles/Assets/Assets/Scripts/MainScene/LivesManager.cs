using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private WinLoseMenu winLoseMenu;

    private int lives = 3;

    public int Lives
    {
        set { lives = value; }
        get { return lives; }
    }

    private void Update()
    {
        SetSprites();

        if (lives == 0)
        {
            winLoseMenu.IsLose = true;
        }
    }

    private void SetSprites()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            SpriteRenderer renderer = hearts[i].GetComponent<SpriteRenderer>();
            if (i < lives)
            {
                renderer.sprite = fullHeart;
            } 
            else
            {
                renderer.sprite = emptyHeart;
            }
        }
    }
}
