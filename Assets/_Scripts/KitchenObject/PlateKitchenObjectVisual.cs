using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObjectVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    [SerializeField] private List<KitchenObjectSO_Object> plateKitchenObjectSOGameObject;

    [Serializable]
    public struct KitchenObjectSO_Object
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    private void Start()
    {
        plateKitchenObject.OnAddFoodEvent += PlateKitchenObject_OnAddFoodEvent;
    }

    private void PlateKitchenObject_OnAddFoodEvent(object sender, PlateKitchenObject.OnAddFoodEventArgs e)
    {
        foreach (KitchenObjectSO_Object item in plateKitchenObjectSOGameObject)
           if (item.kitchenObjectSO == e.kitchenObjectSO)
                item.gameObject.SetActive(true);
    }
}
