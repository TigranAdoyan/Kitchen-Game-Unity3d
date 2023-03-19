using System;
using UnityEngine;

public class TrashCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    public static event EventHandler OnAnyObjectTrash;

    [SerializeField] protected KitchenObjectSO kitchenObjectSO;

    [SerializeField] private string objectName;
    public override void Action(Player player)
    {
        if (player.HasKitchenObject())
        {
            OnAnyObjectTrash?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().Destroy();
            player.ClearKitchenObject();
        }
    }
}
