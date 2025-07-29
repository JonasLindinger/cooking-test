using _Project.Scripts.Object;
using UnityEngine;

namespace _Project.Scripts.Kitchen
{
    public class KitchenObject : MonoBehaviour
    {
	    // Getter
	    public IKitchenObjectParent KitchenObjectParent => kitchenObjectParent;
	    
        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;

		private IKitchenObjectParent kitchenObjectParent;

		public void SetKitchenObjectParent(IKitchenObjectParent newKitchenObjectParent)
		{
			if (kitchenObjectParent != null)
				kitchenObjectParent.ClearKitchenObject();
			
			kitchenObjectParent = newKitchenObjectParent;

			if (kitchenObjectParent.HasKitchenObject())
				Debug.LogError("KitchenObjectParent already has a KitchenObject!");
			
			kitchenObjectParent.SetKitchenObject(this);
			
			transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
			transform.localPosition = Vector3.zero;
		}

		public void DestroySelf()
		{
			kitchenObjectParent.ClearKitchenObject();
			
			Destroy(gameObject);
		}

		public static void SpawnKitchenObject(KitchenScriptableObject kitchenScriptableObject, IKitchenObjectParent parent)
		{
			Transform kitchenObjectTransform = Instantiate(kitchenScriptableObject.prefab).transform;
			kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(parent);
		}
    }
}