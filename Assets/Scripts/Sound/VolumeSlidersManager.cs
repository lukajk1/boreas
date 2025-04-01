using UnityEngine;
using UnityEngine.UI;

public class VolumeSlidersManager : MonoBehaviour
{
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;

    private SoundMixerManager soundMixerManager;

    private void Start()
    {
        soundMixerManager = FindFirstObjectByType<SoundMixerManager>();

        masterVolume.onValueChanged.AddListener(value => soundMixerManager.SetMasterVolume(value));
        musicVolume.onValueChanged.AddListener(value => soundMixerManager.SetMusicVolume(value));
        sfxVolume.onValueChanged.AddListener(value => soundMixerManager.SetSFXVolume(value));

    }
}
