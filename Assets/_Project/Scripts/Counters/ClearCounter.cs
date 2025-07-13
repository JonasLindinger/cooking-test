using _Project.Scripts.Kitchen;
using _Project.Scripts.Object;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class ClearCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private KitchenScriptableObject kitchenObjectData;
        [SerializeField] private Transform counterTopPoint;
        
        private KitchenObject kitchenObject;
        
        public void Interact(PlayerController player)
        {
            if (kitchenObject == null)
            {
                Transform kitchenObjectTransform = Instantiate(kitchenObjectData.prefab, counterTopPoint).transform;
                kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
            }
            else
            {
                // Give the object to the player
                kitchenObject.SetClearCounter(player);
            }
        }

        #region IKitchenObjectParent

        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject newKitchenObject)
        {
            kitchenObject = newKitchenObject;
        }

        public KitchenObject GetKitchenObject()
        {
            return kitchenObject;
        }

        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return kitchenObject != null;
        }
        
        #endregion
    }
}