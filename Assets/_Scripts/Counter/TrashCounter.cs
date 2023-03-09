using System;
using UnityEngine;

public class TrashCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] protected KitchenObjectSO kitchenObjectSO;

    [SerializeField] private string objectName;
    public override void Action(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().Destroy();
            player.ClearKitchenObject();
        }
    }
}
