using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnEvent;

    public event EventHandler OnResipeComplited;

    public event EventHandler OnResipeSuccess;
    
    public event EventHandler OnResipeFail;

    [SerializeField] private ResipeListSO resipeListSO;

    public List<ResipeSO> waitingResipeSOList;

    private float spawnResipeTimer = 4f;

    private int waitingResipesMaxCount = 4;

    private void Awake()
    {
        waitingResipeSOList = new List<ResipeSO>();
        Instance = this;
        StartCoroutine(StartDeliveryMangementLoop());
    }
    IEnumerator StartDeliveryMangementLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnResipeTimer);
            if (waitingResipeSOList.Count < waitingResipesMaxCount)
            {
                ResipeSO rso = resipeListSO.resipeSOList[Random.Range(0, resipeListSO.resipeSOList.Count)];
                waitingResipeSOList.Add(rso);
                OnEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool DeliverResipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingResipeSOList.Count; i++)
        {
            ResipeSO resipeSO = waitingResipeSOList[i];

            bool isOk = false;
            if (resipeSO.kitchenObjectsSOList.Count == plateKitchenObject.kitchenObjectSOList.Count)
            {
                foreach (KitchenObjectSO resipeKitchenObjectSO in resipeSO.kitchenObjectsSOList)
                {
                    bool isWrong = false;
                    int index = plateKitchenObject.kitchenObjectSOList.IndexOf(resipeKitchenObjectSO);
                    if (index == -1)
                        isWrong = true;

                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.kitchenObjectSOList)
                        if (resipeSO.kitchenObjectsSOList.IndexOf(plateKitchenObjectSO) == -1)
                            isWrong = true;

                    if (!isWrong)
                    {
                        isOk = true;
                        break;
                    }
                }
            }

            if (isOk)
            {
                Debug.Log("Delivered correct resipe");
                waitingResipeSOList.RemoveAt(i);
                OnEvent?.Invoke(this, EventArgs.Empty);
                OnResipeComplited?.Invoke(this, EventArgs.Empty);
                OnResipeSuccess?.Invoke(this, EventArgs.Empty);
                return true;
            }
        }

        Debug.Log("Wrong Delivery");
        OnResipeComplited?.Invoke(this, EventArgs.Empty);
        OnResipeFail?.Invoke(this, EventArgs.Empty);
        return false;
    }        
 }
