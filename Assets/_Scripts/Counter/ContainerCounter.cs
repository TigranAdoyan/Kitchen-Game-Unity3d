using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] protected KitchenObjectSO kitchenObjectSO;

    [SerializeField] private string objectName;
    public override void Action(Player player)
    {
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        StartCoroutine(ProcessAction(player, .1f));
    }
    private IEnumerator ProcessAction(IKitchenObjectParent kitchenObjectParent, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (!kitchenObjectParent.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint.transform);
            kitchenObjectTransform.localPosition = Vector3.zero;
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.objectName = objectName;
            kitchenObject.SetParent(kitchenObjectParent);
        } else if (kitchenObjectParent.GetKitchenObject().objectName == objectName)
        {
            kitchenObjectParent.GetKitchenObject().Destroy();
            kitchenObjectParent.ClearKitchenObject();
        }
    }
}
