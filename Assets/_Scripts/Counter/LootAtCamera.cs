using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootAtCamera : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        if (mainCamera == null) return;
        transform.LookAt(mainCamera.transform);
    }
}
