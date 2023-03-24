using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deliveredRecipesCountText;

    [SerializeField] private Button newGameButton;
    private void Start()
    {
        gameObject.SetActive(false);
        KitchenGameManager.Instance.OnStateChange += Instance_OnStateChange;

        newGameButton.onClick.AddListener(OnNewGame);
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
    private void OnNewGame()
    {
        StartCoroutine(Loader.LoadSceneAsync(Loader.Scene.Game));
    }
}
