using System;
using _Project.Scripts.Counters;
using _Project.Scripts.CustomEventArgs;
using _Project.Scripts.Input;
using _Project.Scripts.Kitchen;
using UnityEngine;

namespace Project.Player
{
    public class PlayerController : MonoBehaviour, IKitchenObjectParent
    {
        // Statics
        public static PlayerController Instance { get; private set; }
        
        // Events
        public event EventHandler OnPickedUpSomething;
        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        
        // Getters
        public bool IsWalking => isWalking;
        
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private LayerMask counterLayerMask;
        [SerializeField] private Transform kitchenObjectHoldPoint;

        private bool isWalking;
        private Vector3 lastInteractDirection;
        private BaseCounter selectedCounter;
        
        private KitchenObject kitchenObject;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one PlayerController in the scene!");
            }
            Instance = this;
        }

        private void Start()
        {
            inputManager.OnInteractAction += OnInteractAction;
            inputManager.OnInteractAlternateAction += OnInteractAlternateAction;
        }

        private void Update()
        {
            HandleMovement();
            HandleInteractions();
        }

        private void HandleMovement()
        {
            Vector2 inputVector = inputManager.GetNormalizedMovementVector();
            
            Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

            float moveDistance = moveSpeed * Time.deltaTime;
            
            float playerRadius = .5f;
            float playerHeight = 2f;
            bool canMove = !Physics.CapsuleCast(transform.position, 
                transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

            // Split down movement into X and Z, if XZ Movement is blocked
            if (!canMove)
            {
                // Cannot move towards moveDirection
                
                // Attempt only X movement
                Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
                canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, 
                    transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

                if (canMove)
                {
                    // Can move only on the X
                    moveDirection = moveDirectionX;
                }
                else
                {
                    // Cannot move only on the X
                    
                    // Attempt only X movement
                    Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                    canMove = moveDirection.z != 0 && !Physics.CapsuleCast(transform.position, 
                        transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                    if (canMove)
                    {
                        // Can move only on the Z
                        moveDirection = moveDirectionZ;
                    }
                }
            }
            
            if (canMove)
            {
                transform.position += moveDirection * moveDistance;
            }
            
            isWalking = moveDirection != Vector3.zero;
            
            float rotationSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        }
        
        private void HandleInteractions()
        {
            Vector2 inputVector = inputManager.GetNormalizedMovementVector();
            
            Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

            if (moveDirection != Vector3.zero)
                // We move!
                lastInteractDirection = moveDirection;
            
            float interactDistance = 1f;
            if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit hit, interactDistance, counterLayerMask))
            {
                // We are in front of something
                if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    // Has ClearCounter component
                    if (baseCounter != selectedCounter)
                    {
                        SetSelectedCounter(baseCounter);
                    }
                }
                else
                {
                    SetSelectedCounter(null);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        
        private void SetSelectedCounter(BaseCounter newCounter)
        {
            selectedCounter = newCounter;
            
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                SelectedCounter = newCounter,
            });
        }
        
        private void OnInteractAction(object sender, EventArgs e)
        {
            if (selectedCounter != null)
            {
                selectedCounter.Interact(this);
            }
        }
        
        private void OnInteractAlternateAction(object sender, EventArgs e)
        {
            if (selectedCounter != null)
            {
                selectedCounter.InteractAlternate(this);
            }
        }

        #region IKitchenObjectParent

        public Transform GetKitchenObjectFollowTransform()
        {
            return kitchenObjectHoldPoint;
        }

        public void SetKitchenObject(KitchenObject newKitchenObject)
        {
            kitchenObject = newKitchenObject;
            
            if (newKitchenObject != null)
            {
                // Player picked up something
                OnPickedUpSomething?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                
            }
        }

        public KitchenObject GetKitchenObject()
        {
            return kitchenObject;
        }

        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return kitchenObject != null;
        }
        
        #endregion
    }   
}