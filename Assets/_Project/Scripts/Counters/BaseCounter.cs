using System;
using _Project.Scripts.Kitchen;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        // Events
        public static event EventHandler OnAnyObjectPlacedHere;
        
        public static void ResetStaticData()
        {
            OnAnyObjectPlacedHere = null;
        }
        
        // Getters
        public Transform CounterTopPoint => counterTopPoint;
        
        [SerializeField] private Transform counterTopPoint;
        
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

            if (kitchenObject != null)
            {
                OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
            }
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