using UniRx;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public ReactiveProperty<bool> isMusicEnabled = new ReactiveProperty<bool>(true);
    public ReactiveProperty<bool> areSoundEffectsEnabled = new ReactiveProperty<bool>(true);
    public CompositeDisposable disposable = new CompositeDisposable();

    private void OnDestroy()
    {
        disposable.Dispose();
    }
}
