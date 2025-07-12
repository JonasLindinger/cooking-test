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

        private bool isWalking;
        
        private void Update()
        {
            Vector2 inputVector = inputManager.GetNormalizedMovementVector();
            
            Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

            float playerSize = .7f;
            bool canMove = !Physics.Raycast(transform.position, moveDirection, playerSize);

            if (canMove)
            {
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
            
            isWalking = moveDirection != Vector3.zero;
            
            float rotationSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        }
    }   
}