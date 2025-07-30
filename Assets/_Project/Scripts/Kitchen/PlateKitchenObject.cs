using System.Collections.Generic;
using _Project.Scripts.Object;
using UnityEngine;

namespace _Project.Scripts.Kitchen
{
    public class PlateKitchenObject : KitchenObject
    {
        [Header("Settings")]
        [SerializeField] private List<KitchenScriptableObject> validKitchenObjects;
        
        private List<KitchenScriptableObject> kitchenObjects = new List<KitchenScriptableObject>();
        
        public bool TryAddIngredient(KitchenScriptableObject kitchenObject)
        {
            if (!validKitchenObjects.Contains(kitchenObject))
            {
                // Not a valid ingredient
                return false;
            }
            
            if (kitchenObjects.Contains(kitchenObject))
            {
                // Already has this type
                return false;
            }
            else
            {
                // Is a new ingredient
                kitchenObjects.Add(kitchenObject);
                return true;
            }
        }
    }
}