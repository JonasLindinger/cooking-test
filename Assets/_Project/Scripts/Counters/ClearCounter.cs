using _Project.Scripts.Kitchen;
using _Project.Scripts.Object;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class ClearCounter : MonoBehaviour
    {
        // Getters
        public Transform KitchenObjectFollowTransform => counterTopPoint;
        public KitchenObject KitchenObject => kitchenObject;
        public bool HasKitchenObject => kitchenObject != null;
        
        [SerializeField] private KitchenScriptableObject tomatoPrefab;
        [SerializeField] private Transform counterTopPoint;
        
        private KitchenObject kitchenObject;
        
        public void Interact()
        {
            if (kitchenObject == null)
            {
                Transform kitchenObjectTransform = Instantiate(tomatoPrefab.prefab, counterTopPoint).transform;
                kitchenObjectTransform.localPosition = Vector3.zero;
                
                kitchenObject = kitchenObjectTransform.gameObject.GetComponent<KitchenObject>();
                kitchenObject.SetClearCounter(this);
            }
        }
        
        public void SetKitchenObject(KitchenObject newKitchenObject)
        {
            kitchenObject = newKitchenObject;
        }
        
        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }
    }
}