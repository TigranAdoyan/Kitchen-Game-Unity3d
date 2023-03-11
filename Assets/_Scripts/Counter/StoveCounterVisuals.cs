using UnityEngine;

public class StoveCounterVisuals : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnGameObject;

    [SerializeField] private GameObject particlesOnGameObject;

    [SerializeField] private StoveCounter counter;

    private void Start()
    {
        counter.OnProgressEvent += Counter_OnProgressUI;
    }
    private void Counter_OnProgressUI(object sender, ICounterProgressUI.OnProgressEventArgs e)
    {
        stoveOnGameObject.SetActive(e.status);
        particlesOnGameObject.SetActive(e.status);
    }
}
