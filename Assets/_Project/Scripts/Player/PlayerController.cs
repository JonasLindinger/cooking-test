using System;
using _Project.Scripts.Counters;
using _Project.Scripts.CustomEventArgs;
using _Project.Scripts.Input;
using UnityEngine;

namespace Project.Player
{
    public class PlayerController : MonoBehaviour
    {
        // Statics
        public static PlayerController Instance { get; private set; }
        
        // Events
        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        
        // Getters
        public bool IsWalking => isWalking;
        
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private LayerMask counterLayerMask;

        private bool isWalking;
        private Vector3 lastInteractDirection;
        private ClearCounter selectedCounter;

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
                canMove = !Physics.CapsuleCast(transform.position, 
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
                    canMove = !Physics.CapsuleCast(transform.position, 
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
                if (hit.transform.TryGetComponent(out ClearCounter clearCounter))
                {
                    // Has ClearCounter component
                    if (clearCounter != selectedCounter)
                    {
                        SetSelectedCounter(clearCounter);
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
        
        private void SetSelectedCounter(ClearCounter newSelectedCounter)
        {
            selectedCounter = newSelectedCounter;
            
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                SelectedCounter = newSelectedCounter,
            });
        }
        
        private void OnInteractAction(object sender, EventArgs e)
        {
            if (selectedCounter != null)
            {
                selectedCounter.Interact();
            }
        }
    }   
}