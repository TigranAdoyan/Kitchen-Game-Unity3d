using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour
{
    [SerializeField] protected GameObject activeGameObject;

    [SerializeField] protected GameObject nonActiveGameObject;

    [SerializeField] protected GameObject counterTopPoint;

    protected KitchenObject kitchenObject;

    protected bool active = false;
    public virtual void Action(Player player) {}
    public void SetActive(bool status)
    {
        active = status;
        if (active)
        {
            foreach (Renderer renderer in nonActiveGameObject.GetComponentsInChildren<Renderer>())
                renderer.enabled = false;
            foreach (Renderer renderer in activeGameObject.GetComponentsInChildren<Renderer>())
                renderer.enabled = true;
            if (activeGameObject.GetComponent<Animator>() != null)
                activeGameObject.GetComponent<Animator>().enabled = true;
        } else
        {
            foreach (Renderer renderer in activeGameObject.GetComponentsInChildren<Renderer>())
                renderer.enabled = false;
            foreach (Renderer renderer in nonActiveGameObject.GetComponentsInChildren<Renderer>())
                renderer.enabled = true;
            if (activeGameObject.GetComponent<Animator>() != null)
                activeGameObject.GetComponent<Animator>().enabled = false;
        }
        Debug.Log($"Setting active: {active}");
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    public Transform GetFollowTransform()
    {
        return counterTopPoint.transform;
    }
    public string GetParentType()
    {
        return "counter";
    }
}
