using UnityEngine;

namespace _Project.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInputActions playerInputActions;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
        }

        public Vector2 GetNormalizedMovementVector()
        {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;
            
            return inputVector;
        }
    }
}