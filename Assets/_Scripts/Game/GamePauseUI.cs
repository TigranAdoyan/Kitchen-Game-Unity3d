using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deliveredRecipesCountText;

    [SerializeField] private TextMeshProUGUI remainedTimeText;

    [SerializeField] private Button optionsButton;

    [SerializeField] private GameObject optionsGameObject;
    private void Start()
    {
        gameObject.SetActive(false);
        KitchenGameManager.Instance.OnPauseChange += Instance_OnPauseChange;
        optionsButton.onClick.AddListener(OnClickOptions);
    }
    private void Instance_OnPauseChange(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsPaused())
        {
            deliveredRecipesCountText.text = $"Delivered: {DeliveryManager.Instance.deliveredRecipesCount}";
            remainedTimeText.text = $"Remained: {Math.Ceiling(KitchenGameManager.Instance.GetRemainedPlayingTime())} sec";
            gameObject.SetActive(true);
        } else
        {
            gameObject.SetActive(false);
            optionsGameObject.SetActive(false);

        }
    }

    private void OnClickOptions()
    {
        optionsGameObject.SetActive(true);
    }
}
