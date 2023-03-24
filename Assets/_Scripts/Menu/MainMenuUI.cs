using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;

    [SerializeField] private Button quitButton;
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
