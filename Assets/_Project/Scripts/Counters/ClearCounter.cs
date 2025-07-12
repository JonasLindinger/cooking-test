using _Project.Scripts.Object;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private KitchenObject tomatoPrefab;
        [SerializeField] private Transform counterTopPoint;
        
        public void Interact()
        {
            Transform kitchenObjectTransform = Instantiate(tomatoPrefab.prefab, counterTopPoint).transform;
            kitchenObjectTransform.localPosition = Vector3.zero;
        }
    }
}