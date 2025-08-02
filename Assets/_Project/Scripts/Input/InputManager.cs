using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        
        public event EventHandler OnInteractAction; 
        public event EventHandler OnInteractAlternateAction; 
        public event EventHandler OnPauseAction; 
        
        private PlayerInputActions playerInputActions;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one " + GetType().Name + " in the scene!");
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
            
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();

            playerInputActions.Player.Interact.performed += InteractPerformed;
            playerInputActions.Player.InteractAlternate.performed += InteractAlternatePerformed;
            
            playerInputActions.Player.Pause.performed += PausePerformed;
        }

        private void OnDestroy()
        {
            playerInputActions.Player.Interact.performed -= InteractPerformed;
            playerInputActions.Player.InteractAlternate.performed -= InteractAlternatePerformed;
            
            playerInputActions.Player.Pause.performed -= PausePerformed;
            
            playerInputActions.Dispose();
        }

        private void PausePerformed(InputAction.CallbackContext obj)
        {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
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