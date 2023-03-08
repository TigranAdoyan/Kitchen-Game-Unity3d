using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    public override void Action(Player player)
    {
        if (kitchenObject != null && !player.HasKitchenObject())
            kitchenObject.SetParent(player);
        else if (kitchenObject == null && player.HasKitchenObject())
            player.GetKitchenObject().SetParent(this);
    }
}
