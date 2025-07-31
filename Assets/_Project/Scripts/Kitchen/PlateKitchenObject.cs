using System;
using System.Collections.Generic;
using _Project.Scripts.CustomEventArgs;
using _Project.Scripts.Object;
using UnityEngine;

namespace _Project.Scripts.Kitchen
{
    public class PlateKitchenObject : KitchenObject
    {
        public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
     
        // Getters
        public List<KitchenScriptableObject> KitchenObjects => kitchenObjects;
        
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
                
                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                {
                    KitchenObject = kitchenObject,
                });
                return true;
            }
        }
    }
}