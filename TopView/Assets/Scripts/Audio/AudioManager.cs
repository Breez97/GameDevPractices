using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sourceMusic;
    [SerializeField] private TextMeshProUGUI buttonMusic;
    [SerializeField] private Button buttonMusicColor;

    [SerializeField] private TextMeshProUGUI buttonSoundEffects;
    [SerializeField] private Button buttonSoundEffectsColor;

    private AudioSettings audioSettings;

    private void Awake()
    {
        audioSettings = GetComponent<AudioSettings>();

        audioSettings.isMusicEnabled.Subscribe(isEnabled => {
            if (isEnabled)
            {
                StartMusic();
                buttonMusic.SetText("MUSIC: ON");
                buttonMusicColor.image.color = Color.green;
            }
            else
            {
                PauseMusic();
                buttonMusic.SetText("MUSIC: OFF");
                buttonMusicColor.image.color = Color.red;
            }
        }).AddTo(this);

        audioSettings.areSoundEffectsEnabled.Subscribe(isEnabled => {
            if (isEnabled)
            {
                buttonSoundEffects.SetText("SOUNDS: ON");
                buttonSoundEffectsColor.image.color = Color.green;
            }
            else
            {
                buttonSoundEffects.SetText("SOUNDS: OFF");
                buttonSoundEffectsColor.image.color = Color.red;
            }
        }).AddTo(this);
    }

    private void PauseMusic()
    {
        sourceMusic.Pause();
    }

    private void StartMusic()
    {
        sourceMusic.Play();
    }

    public void ToggleMusic()
    {
        audioSettings.isMusicEnabled.Value = !audioSettings.isMusicEnabled.Value;
    }

    public void ToggleSoundEffects()
    {
        audioSettings.areSoundEffectsEnabled.Value = !audioSettings.areSoundEffectsEnabled.Value;
    }
}
