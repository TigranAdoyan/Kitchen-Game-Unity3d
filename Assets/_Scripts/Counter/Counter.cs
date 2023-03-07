using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Counter : MonoBehaviour, IKitchenObjectParent, ICounter
{   
    [SerializeField] private Material[] materialsNonActive;

    [SerializeField] private Material[] materialsActive;

    [SerializeField] private GameObject counterTopPoint;

    [SerializeField] private KitchenObject kitchenObject;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private Counter secondCounter;

    private Renderer childRenderer;

    private bool active = false;
    void Start()
    {
        childRenderer = transform.GetChild(0).gameObject.GetComponent<Renderer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && kitchenObject != null && active)
            kitchenObject.SetParent(secondCounter);
    }
    public void SetActive(bool status)
    {
        active = status;
        if (status && childRenderer.materials.Length == materialsNonActive.Length)
            childRenderer.materials = materialsActive;
        else if (!status && childRenderer.materials.Length == materialsActive.Length)
            childRenderer.materials = materialsNonActive;
    }
    public void Action(Player player)
    {
        if (kitchenObject != null && !player.HasKitchenObject())
            kitchenObject.SetParent(player);
        else if (kitchenObject == null && player.HasKitchenObject())
            player.GetKitchenObject().SetParent(this);
        else
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint.transform);
            kitchenObjectTransform.localPosition = Vector3.zero;
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetParent(this);
        }
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    public Transform GetFollowTransform()
    {
        return counterTopPoint.transform;
    }
    public string GetParentType()
    {
        return "counter";
    }
}
