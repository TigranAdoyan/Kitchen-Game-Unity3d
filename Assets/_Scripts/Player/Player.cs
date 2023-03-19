using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickUp;

    public event EventHandler OnDrop;

    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private LayerMask counterLayerMask;

    [SerializeField] private LayerMask kitchenObjectLayerMask;

    [SerializeField] private Transform pickUpPoint;

    private Rigidbody rigidBody;

    private KitchenObject kitchenObject;

    private ICounter currCounter;

    private Vector3 lastInteractDirection;

    public float height = 2f;

    public float intersectionDistance = .65f;

    public float moveSpeed = 5f;

    public float rotateSpeed = 10f;

    public float jumpForce = 50f;

    private bool isWalking = false;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerInput.OnAction += GameInput_OnAction;
    }
    void Update()
    {
        Vector2 inputVector = playerInput.MovementVector(true);
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        HandleMovement(moveDir);
        HandleInteraction(moveDir);
    }
    private void GameInput_OnAction(object sender, EventArgs args)
    {
        if (currCounter != null)
            currCounter.Action(this);
    }
    private void HandleMovement(Vector3 moveDir)
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        if (playerInput.JumpingStatus())
            rigidBody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
    }
    private void HandleInteraction(Vector3 moveDir)
    {
        if (moveDir != Vector3.zero)
            lastInteractDirection = moveDir;

        if (
            Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, intersectionDistance, counterLayerMask) &&
            raycastHit.transform.TryGetComponent(out ICounter counter))
        {
            if (currCounter != null)
                currCounter.SetActive(false);
            currCounter = counter;
            currCounter.SetActive(true);
        } else if (currCounter != null)
        {
            currCounter.SetActive(false);
            currCounter = null;
        }
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        OnPickUp?.Invoke(this, EventArgs.Empty);
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        OnDrop?.Invoke(this, EventArgs.Empty);
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    public Transform GetFollowTransform()
    {
        return pickUpPoint;
    }
    public string GetParentType()
    {
        return "player";
    }
}
