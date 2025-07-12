using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private GameObject tomatoPrefab;
        [SerializeField] private Transform counterTopPoint;
        
        public void Interact()
        {
            Transform tomatoTransform = Instantiate(tomatoPrefab, counterTopPoint).transform;
            tomatoTransform.localPosition = Vector3.zero;
        }
    }
}