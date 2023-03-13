using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static PlateKitchenObjectVisual;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    [SerializeField] private GameObject iconGameObject;

    private void Start()
    {
        plateKitchenObject.OnAddFoodEvent += PlateKitchenObject_OnAddFoodEvent;
    }

    private void PlateKitchenObject_OnAddFoodEvent(object sender, PlateKitchenObject.OnAddFoodEventArgs e)
    {
        Transform iconTransform = Instantiate(iconGameObject.transform, transform);
        iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(e.kitchenObjectSO);
    }
}
