using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootAtCamera : MonoBehaviour
{

    [SerializeField] Camera mainCamera;
    private void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
        Debug.Log("looking camera");
    }
}
