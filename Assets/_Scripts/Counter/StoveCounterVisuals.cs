using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisuals : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnGameObject;

    [SerializeField] private GameObject particlesOnGameObject;

    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedArgs e)
    {
        stoveOnGameObject.SetActive(e.status);
        particlesOnGameObject.SetActive(e.status);
    }
}
