using System;
using _Project.Scripts.Kitchen;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        public Transform counterTopPoint;
        
        private KitchenObject kitchenObject;
        
        public virtual void Interact(PlayerController player)
        {
            throw new NotImplementedException();
        }
        
        public virtual void InteractAlternate(PlayerController player)
        {
            // throw new NotImplementedException();
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