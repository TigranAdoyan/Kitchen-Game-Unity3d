using System;
using UnityEngine;

public class TrashCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    [SerializeField] protected KitchenObjectSO kitchenObjectSO;

    [SerializeField] private string objectName;
    public override void Action(Player player)
    {
        Debug.Log(1);
        if (player.HasKitchenObject())
        {
            Debug.Log(2);
            player.GetKitchenObject().Destroy();
            player.ClearKitchenObject();
        }
    }
}
