using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start()
    {
        gameObject.SetActive(false);
        KitchenGameManager.Instance.OnStateChange += Instance_OnStateChange;
    }
    private void Update()
    {
        countDownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountDownStateTimer()).ToString();   
    }
    private void Instance_OnStateChange(object sender, System.EventArgs e)
    {
        gameObject.SetActive(KitchenGameManager.Instance.IsCountDownToStart());
    }
}
