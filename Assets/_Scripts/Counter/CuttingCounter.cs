using System;
using System.Collections.Generic;
using UnityEngine;


public class CuttingCounter : BaseCounter, IKitchenObjectParent, ICounterProgressUI, ICounter
{
    public static event EventHandler OnAnyCuttingEvent;

    public event EventHandler<ICounterProgressUI.OnProgressEventArgs> OnProgressEvent;

    public event EventHandler<EventArgs> OnCuttingEvent;

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
            if (player.GetKitchenObject() is PlateKitchenObject)
            {
                PlateKitchenObject playerKitchen = player.GetKitchenObject() as PlateKitchenObject;
                bool added = playerKitchen.TryAddFood(kitchenObject.GetKitchenObjectSO());
                if (added)
                {
                    kitchenObject.Destroy();
                    ClearKitchenObject();
                }
            }
            else
            {
                kitchenObject.SetParent(player);
            }
            finished = false;
        }
        else if (kitchenObject == null && playerKitchenObject != null && foodToFoodSliceDict.ContainsKey(playerKitchenObject.objectName))
        {
            playerKitchenObject.SetParent(this);
            progressCurrent = 0;
        }
        else if (kitchenObject != null && playerKitchenObject == null)
        {
            if (progressCurrent++ < progressMax)
            {
                OnProgressEvent?.Invoke(this, new ICounterProgressUI.OnProgressEventArgs
                {
                    auto = false,
                    value = (float)progressCurrent / progressMax
                });
                OnCuttingEvent?.Invoke(this, EventArgs.Empty);
                OnAnyCuttingEvent?.Invoke(this, EventArgs.Empty);
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
