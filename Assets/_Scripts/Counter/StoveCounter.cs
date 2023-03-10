using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoveCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    public event EventHandler<OnStateChangedArgs> OnStateChanged;
    public class OnStateChangedArgs : EventArgs
    {
        public bool status;
        public float timeout;
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipes;

    private Coroutine cookingCoroutine;
    public override void Action(Player player)
    {
        if (player.HasKitchenObject() && GetFryingRecipeOutput(player.GetKitchenObject()) != null && kitchenObject == null)
        {
            player.GetKitchenObject().SetParent(this);
            cookingCoroutine = StartCoroutine(StartCooking());
        } else if (!player.HasKitchenObject() && kitchenObject != null)
        {
            kitchenObject.SetParent(player);
            if (cookingCoroutine != null)
            {
                StopCoroutine(cookingCoroutine);
                OnStateChanged?.Invoke(this, new OnStateChangedArgs { status = false });
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
                OnStateChanged?.Invoke(this, new OnStateChangedArgs { status = true, timeout = recipe.fryingTimerMax });
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
        OnStateChanged?.Invoke(this, new OnStateChangedArgs { status = false});
    }
    private FryingRecipeSO GetFryingRecipeOutput(KitchenObject kitchenObject)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipes)
            if (kitchenObject.GetKitchenObjectSO() == fryingRecipeSO.input)
                return fryingRecipeSO;
        return null;
    }
}
