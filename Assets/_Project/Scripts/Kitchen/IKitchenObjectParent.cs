using UnityEngine;

namespace _Project.Scripts.Kitchen
{
    public interface IKitchenObjectParent
    {
        public Transform GetKitchenObjectFollowTransform();
        public void SetKitchenObject(KitchenObject newKitchenObject);
        public KitchenObject GetKitchenObject();
        public void ClearKitchenObject();
        public bool HasKitchenObject();
    }
}