using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deliveredRecipesCountText;
    private void Start()
    {
        gameObject.SetActive(false);
        KitchenGameManager.Instance.OnStateChange += Instance_OnStateChange;
    }
    private void Instance_OnStateChange(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            gameObject.SetActive(true);
            deliveredRecipesCountText.text = DeliveryManager.Instance.deliveredRecipesCount.ToString();
        } else if (KitchenGameManager.Instance.IsGamePlaying())
        {
            gameObject.SetActive(false);
        }
    }
}
