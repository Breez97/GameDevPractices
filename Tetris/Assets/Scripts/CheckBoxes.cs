using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckBoxes : MonoBehaviour
{
    [SerializeField] private GameObject finishScreen;
    [SerializeField] private TMP_Text timeText;

    private List<TetroDrag> here;
    
    private int startBoxesAmount;
    private int currentBoxesAmount;
    
    private void Awake()
    {
        here = new();
        startBoxesAmount = FindObjectsOfType<TetroDrag>().Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        TetroDrag drag = other.gameObject.GetComponentInParent<TetroDrag>();
        if (drag == null || here.Contains(drag)) return;

        here.Add(drag);
        currentBoxesAmount++;

        if (currentBoxesAmount >= startBoxesAmount)
        {
            finishScreen.SetActive(true);
            timeText.SetText($"Total time: {(int)FindObjectOfType<Timer>().MainTime}s");
            Time.timeScale = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TetroDrag drag = other.gameObject.GetComponentInParent<TetroDrag>();
        if (drag == null || !here.Contains(drag)) return;

        here.Remove(drag);
        currentBoxesAmount--;
    }
}
