using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsUI : MonoBehaviour
{
    [SerializeField] private Button soundEffectsButton;

    [SerializeField] private Button musicButton;

    [SerializeField] private TextMeshProUGUI soundEffectsVolumeText;

    [SerializeField] private TextMeshProUGUI musicVolumeText;
    private void Start()
    {
        soundEffectsButton.onClick.AddListener(OnClickSoundEffects);
        musicButton.onClick.AddListener(OnClickMusic);

        soundEffectsVolumeText.text = $"Sound effects: {SoundManager.Instance.GetVolume()}";
        musicVolumeText.text = $"Music: {MusicManager.Instance.GetVolume()}";
    }
    private void OnClickSoundEffects()
    {
        soundEffectsVolumeText.text = $"Sound effects: {(float)Math.Round(SoundManager.Instance.AddVolume(), 1)}";
    }
    private void OnClickMusic()
    {
        musicVolumeText.text = $"Music: {(float)Math.Round(MusicManager.Instance.AddVolume(), 1)}";
    }
}
