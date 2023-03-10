using System;
using System.Collections.Generic;
using UnityEngine;


public class CuttingCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    public event EventHandler<OnCuttingFoodEventArgs> OnCuttingFood;
    public class OnCuttingFoodEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] private Dictionary<string, KitchenObjectSO> foodToFoodSliceDict = new Dictionary<string, KitchenObjectSO>();

    [SerializeField] private KitchenObjectSO tomatoSliceSO;

    [SerializeField] private KitchenObjectSO cheeseSliceSO;

    [SerializeField] private KitchenObjectSO cabbageSliceSO;

    [SerializeField] private int progressMax = 5;

    private int progressCurrent = 0;

    private bool finished = false;
    private void Start()
    {
        foodToFoodSliceDict.Add("Tomato", tomatoSliceSO);
        foodToFoodSliceDict.Add("Cheese", cheeseSliceSO);
        foodToFoodSliceDict.Add("Cabbage", cabbageSliceSO);
    }
    public override void Action(Player player)
    {
        KitchenObject playerKitchenObject = player.GetKitchenObject();
        if (finished)
        {
            kitchenObject.SetParent(player);
            finished = false;
        } else if (kitchenObject == null && playerKitchenObject != null && foodToFoodSliceDict.ContainsKey(playerKitchenObject.objectName))
        {
            playerKitchenObject.SetParent(this);
            progressCurrent = 0;
        } else if (kitchenObject != null && playerKitchenObject == null)
        {
            if (progressCurrent++ < progressMax)
            {
                OnCuttingFood.Invoke(this, new OnCuttingFoodEventArgs
                {
                    progressNormalized = (float)progressCurrent / progressMax
                });
            } 

            if (progressCurrent == progressMax)
            {
                string currObjectName = kitchenObject.objectName;
                kitchenObject.Destroy();
                ClearKitchenObject();
                Transform kitchenObjectTransform = Instantiate(foodToFoodSliceDict[currObjectName].prefab, counterTopPoint.transform);
                kitchenObjectTransform.localPosition = Vector3.zero;
                kitchenObjectTransform.GetComponent<KitchenObject>().SetParent(this);
                finished = true;
            }
        }
    }
}
