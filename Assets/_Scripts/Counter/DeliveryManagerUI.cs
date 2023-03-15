using System;
using TMPro;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;  

    [SerializeField] private Transform recipeTemplate;
    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
        
        DeliveryManager.Instance.OnEvent += UpdateVisual;
    }
    private void UpdateVisual(object sendner, EventArgs e)
    {
        foreach (Transform child in container)
            if (child != recipeTemplate) Destroy(child.gameObject);

        foreach (ResipeSO resipeSO in DeliveryManager.Instance.waitingResipeSOList)
        {
            Transform resipeTransform = Instantiate(recipeTemplate, container);
            resipeTransform.gameObject.SetActive(true);
            resipeTransform.GetComponent<DeliveryManagerSingleUI>().SetResipeSO(resipeSO);
        }
    }
   
}
