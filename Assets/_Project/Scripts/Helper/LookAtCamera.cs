using UnityEngine;

namespace _Project.Scripts.Helper
{
    public class LookAtCamera : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}