using System.Collections;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmp;

    private float fadeSpeed = 0.3f;
    private bool showPopUp = false;
    private Coroutine fadeInCoroutine;
    private Coroutine fadeOutCoroutine;
    private bool isCharacter = false;

    public bool IsCharacter => isCharacter;

    private void Start()
    {
        tmp.alpha = 0f;
    }

    private void Update()
    {
        if (showPopUp && fadeInCoroutine == null)
        {
            fadeInCoroutine = StartCoroutine(FadeText(1f, fadeSpeed));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character") || collision.CompareTag("Player"))
        {
            isCharacter = true;
            showPopUp = true;
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
                fadeOutCoroutine = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character") || collision.CompareTag("Player"))
        {
            isCharacter = false;
            showPopUp = false;
            if (gameObject.activeSelf)
            {
                if (fadeInCoroutine != null)
                {
                    StopCoroutine(fadeInCoroutine);
                    fadeInCoroutine = null;
                }
                fadeOutCoroutine = StartCoroutine(FadeText(0f, fadeSpeed));
            }
        }
    }

    IEnumerator FadeText(float targetAlpha, float duration)
    {
        float startAlpha = tmp.alpha;
        float startTime = Time.time;
        float endTime = startTime + duration;
        while (Time.time < endTime)
        {
            float normalizedTime = (Time.time - startTime) / duration;
            tmp.alpha = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            yield return null;
        }
        tmp.alpha = targetAlpha;
        if (targetAlpha == 0f) fadeOutCoroutine = null;
        else fadeInCoroutine = null;
    }
}
