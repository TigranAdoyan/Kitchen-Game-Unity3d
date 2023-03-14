using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnAddFoodEventArgs> OnAddFoodEvent;
    public class OnAddFoodEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validFoods;

    public List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddFood(KitchenObjectSO kitchenObjectSO)
    {
        if (validFoods.Contains(kitchenObjectSO) && !kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            OnAddFoodEvent?.Invoke(this, new OnAddFoodEventArgs { kitchenObjectSO = kitchenObjectSO });
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
        return false;
    }
}
