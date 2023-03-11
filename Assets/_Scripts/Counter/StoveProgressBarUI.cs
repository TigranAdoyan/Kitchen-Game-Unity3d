using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoveProgressBarUI : MonoBehaviour
{
    [SerializeField] private ICounterProgressUI counter;

    [SerializeField] public Image progressBar;

    private Coroutine processCoroutine; 
    private void Start()
    {
        counter = transform.parent.gameObject.GetComponent<ICounterProgressUI>();
        counter.OnProgressEvent += Counter_OnProgressEvent;
        progressBar.fillAmount = 0;
    }
    private void Counter_OnProgressEvent(object sender, ICounterProgressUI.OnProgressEventArgs e)
    {
        if (e.status)
            processCoroutine = StartCoroutine(StartProgress(e.timeout));
        else
            StopCoroutine(processCoroutine);

        gameObject.SetActive(e.status);
    }
    IEnumerator StartProgress(float timeout)
    {
        float reamainedTime = timeout;
        float frameTime = timeout / 30;
        float frameProcess = 1f / 30;
        progressBar.fillAmount = 1f;
        while (reamainedTime > 0)
        {
            yield return new WaitForSeconds(frameTime);
            reamainedTime -= frameTime;
            progressBar.fillAmount -= frameProcess;
        }
    }
}
