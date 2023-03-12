using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    protected KitchenObject plate;

    protected KitchenObject food;
    public override void Action(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (kitchenObject == null)
            {
                player.GetKitchenObject().SetParent(this);
            } else if (kitchenObject is PlateKitchenObject plateKitchen && player.GetKitchenObject() is not PlateKitchenObject)
            {
                KitchenObject playerKitchen = player.GetKitchenObject();
                bool added = plateKitchen.TryAddFood(playerKitchen.GetKitchenObjectSO());
                if (added)
                {
                    player.ClearKitchenObject();
                    playerKitchen.Destroy();
                }

            } else if (kitchenObject is not PlateKitchenObject && player.GetKitchenObject() is PlateKitchenObject)
            {
                PlateKitchenObject playerKitchen = player.GetKitchenObject() as PlateKitchenObject;
                bool added = playerKitchen.TryAddFood(kitchenObject.GetKitchenObjectSO());
                if (added)
                {
                    kitchenObject.Destroy();
                    ClearKitchenObject();
                }
            }
        } else if (kitchenObject != null)
        {
            kitchenObject.SetParent(player);
        }
    }
}
