using System;
using _Project.Scripts.Object;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class UIDeliveryManagerSingle : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text recipeNameText;
        [SerializeField] private Transform iconContainer;
        [SerializeField] private GameObject iconTemplate;

        private RecipeScriptableObject recipe;

        private void Awake()
        {
            iconTemplate.SetActive(false);
        }

        public void SetUp(RecipeScriptableObject recipe)
        {
            this.recipe = recipe;

            recipeNameText.text = recipe.recipeName;

            foreach (Transform child in iconContainer)
            {
                if (child.gameObject == iconTemplate) continue;
                Destroy(child.gameObject);
            }

            foreach (var kitchenObject in recipe.kitchenObjects)
            {
                Transform iconTransform = Instantiate(iconTemplate, iconContainer).transform;
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<Image>().sprite = kitchenObject.sprite;
            }
        }
    }
}