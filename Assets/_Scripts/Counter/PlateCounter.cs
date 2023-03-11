using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter, IKitchenObjectParent, ICounterProgressUI, ICounter
{
    public event EventHandler<ICounterProgressUI.OnProgressEventArgs> OnProgressEvent;

    [SerializeField] protected KitchenObjectSO kitchenObjectSO;

    [SerializeField] protected string objectName;

    [SerializeField] protected int maxPlatesCount;

    private Stack<PlateKitchenObject> plates = new Stack<PlateKitchenObject>();

    private float spawnPlateTime = 4f;

    private void Start()
    {
        StartCoroutine(StartGeneratingPlates());
    }
    public override void Action(Player player)
    {
        if (!player.HasKitchenObject() && plates.Count > 0)
            plates.Pop().SetParent(player);
    }
    private IEnumerator StartGeneratingPlates()
    {
        while (true)
        {
            if (plates.Count < maxPlatesCount)
            {
                OnProgressEvent?.Invoke(this, new ICounterProgressUI.OnProgressEventArgs { auto = true, status = true, timeout = spawnPlateTime });
                yield return new WaitForSeconds(spawnPlateTime);
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint.transform);
                kitchenObjectTransform.localPosition = Vector3.zero;
                PlateKitchenObject kitchenObject = kitchenObjectTransform.GetComponent<PlateKitchenObject>();
                kitchenObject.SetParent(this);
                plates.Push(kitchenObject);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
