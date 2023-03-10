using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoveProgressBarUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    [SerializeField] public Image progressBar;

    private Coroutine processCoroutine; 
    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnProgressChange;
        progressBar.fillAmount = 0;
    }
    private void StoveCounter_OnProgressChange(object sender, StoveCounter.OnStateChangedArgs e)
    {
        Debug.Log($"Received process: {e.status}");
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
            Debug.Log($"frameProcess: {frameProcess}");
            progressBar.fillAmount -= frameProcess;
        }
    }
}
