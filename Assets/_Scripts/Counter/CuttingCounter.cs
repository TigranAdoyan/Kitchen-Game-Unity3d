using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    public override void Action(Player player)
    {
        if (player.GetKitchenObject() != null)
            Debug.Log($"Player wanna to cut {player.GetKitchenObject().objectName}");
        else
            Debug.Log($"Player dont have anything to cut");
    }
}
