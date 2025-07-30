using System.Collections.Generic;
using _Project.Scripts.CustomEventArgs;
using UnityEngine;

namespace _Project.Scripts.Kitchen
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [Space(5)]
        [SerializeField] private List<KitchenScriptableObjectGameObject> kitchenScriptableObjectGameObjects;
        
        private void Start()
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
            
            // Hide all ingredients at the start
            foreach (var kitchenScriptableObject in kitchenScriptableObjectGameObjects)
            {
                kitchenScriptableObject.gameObject.SetActive(false);
            }
        }

        private void PlateKitchenObjectOnOnIngredientAdded(object sender, OnIngredientAddedEventArgs e)
        {
            foreach (var kitchenScriptableObject in kitchenScriptableObjectGameObjects)
            {
                if (kitchenScriptableObject.kitchenObject == e.KitchenObject)
                {
                    kitchenScriptableObject.gameObject.SetActive(true);
                }
            }
        }
    }
}