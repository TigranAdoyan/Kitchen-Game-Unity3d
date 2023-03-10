using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipes;

    public override void Action(Player player)
    {
        if (player.HasKitchenObject() && kitchenObject == null)
        {
            player.GetKitchenObject().SetParent(this);
            StartCoroutine(StartCooking());
        } else if (!player.HasKitchenObject() && kitchenObject != null)
        {
            kitchenObject.SetParent(player);
        }
    }
    IEnumerator StartCooking()
    {
        while (true)
        {
            FryingRecipeSO recipe = GetFryingRecipeOutput(kitchenObject);
            if (recipe != null)
            {
                yield return new WaitForSeconds(recipe.fryingTimerMax);
                kitchenObject.Destroy();
                ClearKitchenObject();
                Transform kitchenObjectTransform = Instantiate(recipe.output.prefab, counterTopPoint.transform);
                kitchenObjectTransform.localPosition = Vector3.zero;
                kitchenObjectTransform.GetComponent<KitchenObject>().SetParent(this);
            } else
            {
                break;
            }
        }
    }
    private FryingRecipeSO GetFryingRecipeOutput(KitchenObject kitchenObject)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipes)
            if (kitchenObject.GetKitchenObjectSO() == fryingRecipeSO.input)
                return fryingRecipeSO;
        return null;
    }
}
