using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;

    [SerializeField] KitchenObjectSO kitchenObjectSO;

    public string objectName;

    private IKitchenObjectParent parent;
    public void SetParent(IKitchenObjectParent newParent)
    {
        if (parent != null)
            parent.ClearKitchenObject();

        if (newParent == null)
        {
            this.parent = null;
            transform.parent = null;
            if (rigidBody == null)
                rigidBody = this.AddComponent<Rigidbody>();
            return;
        }

        this.parent = newParent;

        parent.SetKitchenObject(this);
        transform.parent = parent.GetFollowTransform();
        transform.localPosition = Vector3.zero;

        string parentType = parent.GetParentType();
        if (parentType == "player" && rigidBody != null)
            Destroy(rigidBody);
        else if (parentType == "counter" && rigidBody == null)
        {
            rigidBody = this.AddComponent<Rigidbody>();
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
