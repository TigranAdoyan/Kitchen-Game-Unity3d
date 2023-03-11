using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static ICounterProgressUI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private ICounterProgressUI counter;

    [SerializeField] public Image progressBar;

    private Coroutine progressCoroutine;

    private void Start()
    {
        counter = transform.parent.gameObject.GetComponent<ICounterProgressUI>();
        counter.OnProgressEvent += Counter_OnProgressEvent;
        progressBar.fillAmount = 0;
    }
    private void Counter_OnProgressEvent(object sender, OnProgressEventArgs e)
    {
        if (e.auto)
        {
            gameObject.SetActive(e.status);
            if (e.status) 
                progressCoroutine = StartCoroutine(StartProgress(e.timeout));
            else
                StopCoroutine(progressCoroutine);
        }
        else
        {
            progressBar.fillAmount = e.value;
            gameObject.SetActive(!(progressBar.fillAmount == 0f || progressBar.fillAmount == 1f));
        }
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
