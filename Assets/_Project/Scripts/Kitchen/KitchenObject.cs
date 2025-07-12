using UnityEngine;

namespace _Project.Scripts.Kitchen
{
    public class KitchenObject : MonoBehaviour
    {
        public Object.KitchenObject KitchenObjectData => kitchenObject;
        
        [SerializeField] private Object.KitchenObject kitchenObject;
    }
}