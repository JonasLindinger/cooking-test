using _Project.Scripts.Counters;
using _Project.Scripts.Object;
using UnityEngine;

namespace _Project.Scripts.Kitchen
{
    public class KitchenObject : MonoBehaviour
    {
		// Getter
        public KitchenScriptableObject KitchenScriptableObjectData => kitchenScriptableObject;
        public IKitchenObjectParent KitchenObjectParent => kitchenObjectParent;
        
        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;

		private IKitchenObjectParent kitchenObjectParent;

		public void SetClearCounter(IKitchenObjectParent newKitchenObjectParent)
		{
			if (kitchenObjectParent != null)
				kitchenObjectParent.ClearKitchenObject();
			
			kitchenObjectParent = newKitchenObjectParent;

			if (kitchenObjectParent.HasKitchenObject())
				Debug.LogError("Counter already has a KitchenObject!");
			
			kitchenObjectParent.SetKitchenObject(this);
			
			transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
			transform.localPosition = Vector3.zero;
		}
    }
}