using System;
using System.Collections.Generic;
using _Project.Scripts.Object;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Kitchen
{
    public class DeliveryManager : MonoBehaviour
    {
        public static DeliveryManager Instance { get; private set; }
        
        // Events
        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;
        public event EventHandler OnRecipeSuccess;
        public event EventHandler OnRecipeFailed;
        
        // Getters
        public List<RecipeScriptableObject> WaitingRecipes => waitingRecipes;
        
        [Header("Settings")]
        [SerializeField] private RecipeListScriptableObject recipeList;
        [SerializeField] private float spawnRecipeTimerMax = 4;
        [SerializeField] private int waitingRecipesMax = 4;
        
        private List<RecipeScriptableObject> waitingRecipes = new List<RecipeScriptableObject>();
        
        private float spawnRecipeTimer;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one " + GetType().Name + " in the scene!");
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
        }

        private void Update()
        {
            spawnRecipeTimer -= Time.deltaTime;
            if (spawnRecipeTimer <= 0)
            {
                spawnRecipeTimer = spawnRecipeTimerMax;

                if (waitingRecipes.Count < waitingRecipesMax)
                {
                    RecipeScriptableObject waitingRecipe = recipeList.recipes[Random.Range(0, recipeList.recipes.Count)];
                    waitingRecipes.Add(waitingRecipe);
                    
                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void DeliverRecipe(PlateKitchenObject plate)
        {
            for (int i = 0; i < waitingRecipes.Count; i++)
            {
                RecipeScriptableObject waitingRecipe = waitingRecipes[i];

                if (waitingRecipe.kitchenObjects.Count == plate.KitchenObjects.Count)
                {
                    // Has the same number of ingredients
                    bool plateContentsMatchesRecipe = true;
                    foreach (var recipeKitchenObject in waitingRecipe.kitchenObjects)
                    {
                        // Cycling through all ingredients in the recipe
                        bool ingredientFound = false;
                        foreach (var plateKitchenObject in plate.KitchenObjects)
                        {
                            // Cycling through all ingredients on the plate
                            if (plateKitchenObject == recipeKitchenObject)
                            {
                                // Ingredient matches!
                                ingredientFound = true;
                                break;
                            }
                        }
                        
                        // Check if ingredient was found
                        if (!ingredientFound)
                        {
                            // This recipe ingredient was not found on the plate
                            plateContentsMatchesRecipe = false;
                        }

                        if (plateContentsMatchesRecipe)
                        {
                            // Plater delivered the correct recipe!
                            
                            // Remove the recipe from the waiting list
                            waitingRecipes.RemoveAt(i);
                            
                            // Fire event
                            OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                            OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                            return;
                        }
                    }
                }
            }
            
            // No matches found!
            // Player did not deliver a correct recipe
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }
    }
}