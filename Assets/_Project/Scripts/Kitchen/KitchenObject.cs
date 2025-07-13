using _Project.Scripts.Counters;
using _Project.Scripts.Object;
using UnityEngine;

namespace _Project.Scripts.Kitchen
{
    public class KitchenObject : MonoBehaviour
    {
		// Getter
        public KitchenScriptableObject KitchenScriptableObjectData => kitchenScriptableObject;
        public ClearCounter ClearCounter => clearCounter;
        
        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;

		private ClearCounter clearCounter;

		public void SetClearCounter(ClearCounter newClearCounter)
		{
			if (clearCounter != null)
				clearCounter.ClearKitchenObject();
			
			clearCounter = newClearCounter;

			if (clearCounter.HasKitchenObject)
				Debug.LogError("Counter already has a KitchenObject!");
			
			clearCounter.SetKitchenObject(this);
			
			transform.parent = clearCounter.KitchenObjectFollowTransform;
			transform.localPosition = Vector3.zero;
		}
    }
}