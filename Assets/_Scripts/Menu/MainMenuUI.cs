using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;

    [SerializeField] private Button quitButton;

    [SerializeField] private string GAME_SCENE = "Game";

    private void Awake()
    {
        playButton.onClick.AddListener(OnStart);
        quitButton.onClick.AddListener(OnQuit);
    }
    private void OnStart()
    {
        StartCoroutine(Loader.LoadSceneAsync(Loader.Scene.Game));
    }
    private void OnQuit()
    {
        Application.Quit();
    }
}
