using System;
using System.Collections;
using UnityEngine;

public class StoveCounter : BaseCounter, IKitchenObjectParent, ICounterProgressUI, ICounter
{
    public event EventHandler<ICounterProgressUI.OnProgressEventArgs> OnProgressEvent;

    [SerializeField] private FryingRecipeSO[] fryingRecipes;

    private Coroutine cookingCoroutine;
    public override void Action(Player player)
    {
        if (player.HasKitchenObject() && player.GetKitchenObject() is PlateKitchenObject && kitchenObject != null)
        {
            PlateKitchenObject playerKitchen = player.GetKitchenObject() as PlateKitchenObject;
            bool added = playerKitchen.TryAddFood(kitchenObject.GetKitchenObjectSO());
            if (added)
            {
                kitchenObject.Destroy();
                ClearKitchenObject();
            }
        } else if (player.HasKitchenObject() && GetFryingRecipeOutput(player.GetKitchenObject()) != null && kitchenObject == null)
        {
            player.GetKitchenObject().SetParent(this);
            cookingCoroutine = StartCoroutine(StartCooking());
        } else if (!player.HasKitchenObject() && kitchenObject != null)
        {
            kitchenObject.SetParent(player);
            if (cookingCoroutine != null)
            {
                StopCoroutine(cookingCoroutine);
                OnProgressEvent?.Invoke(this, new ICounterProgressUI.OnProgressEventArgs { auto = true, status = false });
            }
        }
    }
    IEnumerator StartCooking()
    {
        while (true)
        {
            FryingRecipeSO recipe = GetFryingRecipeOutput(kitchenObject);
            if (recipe != null)
            {
                OnProgressEvent?.Invoke(this, new ICounterProgressUI.OnProgressEventArgs { auto = true, status = true, timeout = recipe.fryingTimerMax });
                yield return new WaitForSeconds(recipe.fryingTimerMax);
                if (kitchenObject != null)
                {
                    kitchenObject.Destroy();
                    ClearKitchenObject();
                    Transform kitchenObjectTransform = Instantiate(recipe.output.prefab, counterTopPoint.transform);
                    kitchenObjectTransform.localPosition = Vector3.zero;
                    kitchenObjectTransform.GetComponent<KitchenObject>().SetParent(this);
                }
            } else
                break;
        }
        OnProgressEvent?.Invoke(this, new ICounterProgressUI.OnProgressEventArgs { auto = true, status = false });
    }
    private FryingRecipeSO GetFryingRecipeOutput(KitchenObject kitchenObject)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipes)
            if (kitchenObject.GetKitchenObjectSO() == fryingRecipeSO.input)
                return fryingRecipeSO;
        return null;
    }
}
