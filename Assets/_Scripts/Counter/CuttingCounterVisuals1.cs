using UnityEngine;

public class CuttingCounterVisuals : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;

    private const string CUT_TRIGGER = "Cut";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnCuttingKitchenObject;
    }
    private void ContainerCounter_OnCuttingKitchenObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT_TRIGGER);
    }
}
