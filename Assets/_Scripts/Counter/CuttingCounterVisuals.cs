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
        cuttingCounter.OnCuttingEvent += ContainerCounter_OnCuttingEvent;
    }
    private void ContainerCounter_OnCuttingEvent(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT_TRIGGER);
    }
}
