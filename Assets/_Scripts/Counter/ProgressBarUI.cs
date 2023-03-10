using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;

    [SerializeField] public Image progressBar;

    private void Start()
    {
        cuttingCounter.OnCuttingFood += CuttingCounter_OnProgressChange;
        progressBar.fillAmount = 0;
    }
    private void CuttingCounter_OnProgressChange(object sender, CuttingCounter.OnCuttingFoodEventArgs e)
    {
        progressBar.fillAmount = e.progressNormalized;

        Debug.Log($"progressBar.fillAmount: {progressBar.fillAmount}");
        gameObject.SetActive(!(progressBar.fillAmount == 0f || progressBar.fillAmount == 1f));
    }
}
