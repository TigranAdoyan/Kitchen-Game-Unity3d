using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipsRefs;

    private float volume = .5f;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Player.Instance.OnPickUp += Instance_OnPickUp;
        Player.Instance.OnDrop += Instance_OnDrop;
        DeliveryManager.Instance.OnResipeSuccess += DeliveryManager_OnResipeSuccess;
        DeliveryManager.Instance.OnResipeFail += DeliveryManager_OnResipeFail;
        CuttingCounter.OnAnyCuttingEvent += CuttingCounter_OnAnyCuttingEvent;
        TrashCounter.OnAnyObjectTrash += TrashCounter_OnAnyObjectTrash;
    }
    public float AddVolume()
    {
        volume += .1f;
        if (volume > 1f)
            volume = .1f;
        
        return volume;
    }
    public float GetVolume()
    {
        return volume;
    }
    private void TrashCounter_OnAnyObjectTrash(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipsRefs.trash, trashCounter.transform.position);
    }

    private void Instance_OnPickUp(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefs.objectPickup, Player.Instance.transform.position);
    }
    private void Instance_OnDrop(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefs.objectDrop, Player.Instance.transform.position);
    }
    private void CuttingCounter_OnAnyCuttingEvent(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySound(audioClipsRefs.chop, cuttingCounter.transform.position);
    }
    private void DeliveryManager_OnResipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefs.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }
    private void DeliveryManager_OnResipeFail(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefs.deliveryFail, DeliveryCounter.Instance.transform.position);
    }
    public void PlayPlayerFootSteps(Vector3 position)
    {
        PlaySound(audioClipsRefs.footStep, position);
    }
    private void PlaySound(AudioClip[] audioClips, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volume);   
    }
}
