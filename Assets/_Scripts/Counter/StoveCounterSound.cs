using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        stoveCounter.OnProgressEvent += StoveCounter_OnProgressEvent;
    }
    private void StoveCounter_OnProgressEvent(object sender, ICounterProgressUI.OnProgressEventArgs e)
    {
        if (e.status)
            audioSource.Play();
        else
            audioSource.Stop();
    }
}
