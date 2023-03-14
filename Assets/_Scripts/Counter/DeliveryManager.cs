using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnEvent;

    [SerializeField] private ResipeListSO resipeListSO;

    public List<ResipeSO> waitingResipeSOList;

    private float spawnResipeTimer;

    private float spawnResipeTimerMax = 4f;

    private int waitingResipesMaxCount = 4;

    private void Awake()
    {
        waitingResipeSOList = new List<ResipeSO>();
        Instance = this;
    }
    private void Update()
    {
        spawnResipeTimer -= Time.deltaTime; 
        if (spawnResipeTimer <= 0f) {
            spawnResipeTimer = spawnResipeTimerMax;
            if (waitingResipeSOList.Count < waitingResipesMaxCount)
            {
                ResipeSO rso = resipeListSO.resipeSOList[Random.Range(0, resipeListSO.resipeSOList.Count)];
                Debug.Log(rso.name);
                OnEvent?.Invoke(this, EventArgs.Empty);
                waitingResipeSOList.Add(rso);
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
                return true;
            }
        }
        Debug.Log("Wrong Delivery");
        return false;
    }        
 }
