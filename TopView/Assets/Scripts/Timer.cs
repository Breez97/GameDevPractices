using UniRx;
using UnityEngine;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float gameDuration = 61f;
    private CompositeDisposable disposable = new CompositeDisposable();
    private GameState gameState;

    private void Awake()
    {
        gameState = FindObjectOfType<GameState>();
    }

    private void Start()
    {
        StartGameTimer(gameDuration);
    }

    private void StartGameTimer(float duration)
    {
        var startTime = Time.time;
        var endTime = startTime + duration;

        Observable.Timer(TimeSpan.FromSeconds(duration))
            .Subscribe(_ => OnTimeUp())
            .AddTo(disposable);

        Observable.EveryUpdate()
            .Select(_ => endTime - Time.time)
            .TakeWhile(timeRemaining => timeRemaining > 0)
            .Sample(TimeSpan.FromSeconds(1.0f))
            .Subscribe(timeRemaining => {
                timerText.SetText($"Time left: {(int)timeRemaining}");
            })
            .AddTo(disposable);
    }

    private void OnTimeUp()
    {
        gameState.OnPlayerDeath();
    }

    private void OnDestroy()
    {
        disposable.Dispose();
    }
}
