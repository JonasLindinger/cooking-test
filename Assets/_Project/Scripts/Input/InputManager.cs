using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        public event EventHandler OnInteractAction; 
        public event EventHandler OnInteractAlternateAction; 
        
        private PlayerInputActions playerInputActions;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();

            playerInputActions.Player.Interact.performed += InteractPerformed;
            playerInputActions.Player.InteractAlternate.performed += InteractAlternatePerformed;
        }

        private void InteractPerformed(InputAction.CallbackContext context)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }
        
        private void InteractAlternatePerformed(InputAction.CallbackContext context)
        {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }
        
        public Vector2 GetNormalizedMovementVector()
        {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;
            
            return inputVector;
        }
    }
}