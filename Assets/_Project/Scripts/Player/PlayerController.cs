using _Project.Scripts.Input;
using UnityEngine;

namespace Project.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 7f;
        
        [SerializeField] private InputManager inputManager;

        private void Update()
        {
            Vector2 inputVector = inputManager.GetNormalizedMovementVector();
            
            Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            
            float rotationSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        }
    }   
}