using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timer;
    [HideInInspector] public float MainTime;

    private float msec;
    private float sec;
    private float min;

    private void Start()
    {
        if (timer != null) StartCoroutine(StopWatch());
    }

    private IEnumerator StopWatch()
    {
        while (true)
        {
            MainTime += Time.deltaTime;
            sec = (int)MainTime;

            timer.text = sec.ToString("00");

            yield return null;
        }
    }
}
