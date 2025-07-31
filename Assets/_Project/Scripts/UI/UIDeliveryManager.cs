using System;
using _Project.Scripts.Kitchen;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Helper
{
    public class UIDeliveryManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform container;
        [SerializeField] private GameObject recipeTemplate;

        private void Awake()
        {
            recipeTemplate.SetActive(false);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSpawned += DeliveryManagerOnOnRecipeSpawned;
            DeliveryManager.Instance.OnRecipeCompleted += DeliveryManagerOnOnRecipeCompleted;

            UpdateVisual();
        }

        private void DeliveryManagerOnOnRecipeCompleted(object sender, EventArgs e)
        {
            UpdateVisual();
        }

        private void DeliveryManagerOnOnRecipeSpawned(object sender, EventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform child in container)
            {
                if (child.gameObject == recipeTemplate) continue;
                Destroy(child.gameObject);
            }

            foreach (var recipe in DeliveryManager.Instance.WaitingRecipes)
            {
                Transform recipeTransform = Instantiate(recipeTemplate, container).transform;
                recipeTransform.gameObject.SetActive(true);
                recipeTransform.GetComponent<UIDeliveryManagerSingle>().SetUp(recipe);
            }
        }
    }
}