using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resipeNameText;

    [SerializeField] private Transform iconContainer;

    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);   
    }
    public void SetResipeSO(ResipeSO resipeSO) {
        resipeNameText.text = resipeSO.resipeName;

        foreach (Transform child in iconContainer)
            if (child != iconTemplate) Destroy(child.gameObject);

        foreach (KitchenObjectSO kitchenObjectSO in resipeSO.kitchenObjectsSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
