using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryCounter : BaseCounter, IKitchenObjectParent, ICounter
{
    public override void Action(Player player)
    {
        if (player.HasKitchenObject() && player.GetKitchenObject() is PlateKitchenObject) 
        {
            bool delivered = DeliveryManager.Instance.DeliverResipe(player.GetKitchenObject() as PlateKitchenObject);
            if (delivered)
            {
                player.GetKitchenObject().SetParent(this);
                StartCoroutine(StartDelivering());
            }
        }
    }
    private void Update()
    {
        if (kitchenObject != null)
            kitchenObject.transform.position += new Vector3(Time.deltaTime, 0, 0);
    }
    IEnumerator StartDelivering ()
    {
        yield return new WaitForSeconds(1f);
        kitchenObject.Destroy();
        ClearKitchenObject();
    }
}


