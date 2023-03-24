using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] AudioSource musicAudioSource;
    private void Awake()
    {
        Instance = this;
        musicAudioSource.volume = .5f;
    }
    public float AddVolume()
    {
        musicAudioSource.volume += .1f;
        if (musicAudioSource.volume >= 1f)
            musicAudioSource.volume = .1f;
        
        return musicAudioSource.volume;
    }
    public float GetVolume()
    {
        return musicAudioSource.volume;
    }
}
