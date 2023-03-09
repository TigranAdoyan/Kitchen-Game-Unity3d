using System.Collections;
using UnityEditor;
using UnityEngine;

public class CuttingCounterVisuals : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private const string CUT_TRIGGER = "Cut";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cuttingCounter.OnCuttingFood += ContainerCounter_OnCuttingFood;
    }
    private void ContainerCounter_OnCuttingFood(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT_TRIGGER);
    }
}
