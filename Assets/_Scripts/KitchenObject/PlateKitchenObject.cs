using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public void AddFood(KitchenObjectSO kitchenObjectSO)
    {
        kitchenObjectSOList.Add(kitchenObjectSO);
    }
}
