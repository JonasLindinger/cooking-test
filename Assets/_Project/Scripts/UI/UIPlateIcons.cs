using _Project.Scripts.CustomEventArgs;
using _Project.Scripts.Kitchen;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class UIPlateIcons : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private GameObject iconTemplate;

        private void Awake()
        {
            iconTemplate.gameObject.SetActive(false);
        }

        private void Start()
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
        }

        private void PlateKitchenObjectOnOnIngredientAdded(object sender, OnIngredientAddedEventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject == iconTemplate) continue;
                Destroy(child.gameObject);    
            }   
            
            foreach (var kitchenObject in plateKitchenObject.KitchenObjects)
            {
                Transform iconTransform = Instantiate(iconTemplate, transform).transform;
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<UIPlateIconSingle>().SetKitchenScriptableObject(kitchenObject);
            }
        }
    }
}