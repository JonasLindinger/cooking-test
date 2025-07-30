using UnityEngine;

namespace _Project.Scripts.Helper
{
    public class LookAtCamera : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private LookAtCameraMode mode;
        
        private void LateUpdate()
        {
            switch (mode)
            {
                case LookAtCameraMode.LookAt:
                    transform.LookAt(Camera.main.transform);
                    break;
                case LookAtCameraMode.LookAtInverted:
                    Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;
                case LookAtCameraMode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
                case LookAtCameraMode.CameraForwardInverted:
                    transform.forward = -Camera.main.transform.forward;
                    break;
            }
        }
    }
}