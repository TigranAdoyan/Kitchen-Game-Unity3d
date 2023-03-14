using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;  

    [SerializeField] private Transform recipeTemplate;

    private void Start()
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
            Transform recipeTextTransform = resipeTransform.Find("RecipeText");
            TextMeshPro recipeText = recipeTextTransform.GetComponent<TextMeshPro>();
            recipeText.SetText("awdawd"); 
            // @Todo Will work on resipes for delivery UI
        }
        //Transform resipeTransform = Instantiate(recipeTemplate, container);
        //resipeTransform.gameObject.SetActive(true);

        //TextMeshPro textMeshPro = resipeTransform.Find("RecipeText").GetComponent<TextMeshPro>();
        //textMeshPro.SetText(resipeSO.name);

    }
   
}
