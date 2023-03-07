using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] Rigidbody rigidBody;

    private IKitchenObjectParent parent;
    public void SetParent(IKitchenObjectParent newParent)
    {
        if (parent != null)
            parent.ClearKitchenObject();
        this.parent = newParent;

        parent.SetKitchenObject(this);
        transform.parent = parent.GetFollowTransform();
        transform.localPosition = Vector3.zero;

        string parentType = parent.GetParentType();
        if (parentType == "player" && rigidBody != null)
            Destroy(rigidBody);
        else if (parentType == "counter" && rigidBody == null)
            rigidBody = this.AddComponent<Rigidbody>();
    }
    public IKitchenObjectParent GetParent()
    {
        return this.parent;
    }
}
