using _Project.Scripts.Counters;
using _Project.Scripts.Input;
using UnityEngine;

namespace Project.Player
{
    public class PlayerController : MonoBehaviour
    {
        // Getters
        public bool IsWalking => isWalking;
        
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 7f;
        
        [SerializeField] private InputManager inputManager;
        
        [SerializeField] private LayerMask counterLayerMask;

        private bool isWalking;
        private Vector3 lastInteractDirection;
        
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
                    clearCounter.Interact();
                }
            }
        }
    }   
}