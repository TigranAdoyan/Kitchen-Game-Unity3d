using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resipeNameText;
    public void SetResipeSO(ResipeSO resipeSO) {
        resipeNameText.text = resipeSO.resipeName;
    }
}
