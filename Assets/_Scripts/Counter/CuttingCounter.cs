using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CuttingCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    public event EventHandler OnCuttingFood;

    [SerializeField] private Dictionary<string, KitchenObjectSO> foodToFoodSliceDict = new Dictionary<string, KitchenObjectSO>();

    [SerializeField] private KitchenObjectSO tomatoSliceSO;

    [SerializeField] private KitchenObjectSO cheeseSliceSO;

    [SerializeField] private KitchenObjectSO cabbageSliceSO;

    [SerializeField] private Image progressBar;

    private int cutsCount;
    private void Start()
    {
        foodToFoodSliceDict.Add("Tomato", tomatoSliceSO);
        foodToFoodSliceDict.Add("Cheese", cheeseSliceSO);
        foodToFoodSliceDict.Add("Cabbage", cabbageSliceSO);
    }
    public override void Action(Player player)
    {
        KitchenObject playerKitchenObject = player.GetKitchenObject();
        if (kitchenObject == null && playerKitchenObject != null && foodToFoodSliceDict.ContainsKey(playerKitchenObject.objectName))
        {
            playerKitchenObject.SetParent(this);
            cutsCount = 0;
        } else if (kitchenObject != null && playerKitchenObject == null)
        {
            if (cutsCount++ <= 5)
            {
                progressBar.gameObject = 
                OnCuttingFood?.Invoke(this, EventArgs.Empty);
            } else
            {
                string currObjectName = kitchenObject.objectName;
                kitchenObject.Destroy();
                ClearKitchenObject();
                Transform kitchenObjectTransform = Instantiate(foodToFoodSliceDict[currObjectName].prefab, counterTopPoint.transform);
                kitchenObjectTransform.localPosition = Vector3.zero;
                kitchenObjectTransform.GetComponent<KitchenObject>().SetParent(player);
            }
        }
    }
}
