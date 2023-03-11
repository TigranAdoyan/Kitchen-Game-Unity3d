using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    protected KitchenObject plate;

    protected KitchenObject food;
    public override void Action(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (kitchenObject != null && player.GetKitchenObject() is PlateKitchenObject)
            {
                PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                plateKitchenObject.AddFood(player.GetKitchenObject().GetKitchenObjectSO());
                kitchenObject.Destroy();
            } else if (player.GetKitchenObject() is KitchenObject)
            {
                player.GetKitchenObject().SetParent(this);
            }
        }
    }
}
