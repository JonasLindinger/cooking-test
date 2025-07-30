using System;
using _Project.Scripts.CustomEventArgs;
using _Project.Scripts.Kitchen;
using _Project.Scripts.Object;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class StoveCounter : BaseCounter
    {
        public event EventHandler<OnStoveCounterStateChangedEventArgs> OnStateChanged;
        
        [Header("Settings")]
        [SerializeField] private FryingRecipeScriptableObject[] fryingRecipes;
        [SerializeField] private BurningRecipeScriptableObject[] burningRecipes;

        private StoveCounterState state;
        
        private float fryingTimer;
        private FryingRecipeScriptableObject currentFryingRecipe;
        
        private float burningTimer;
        private BurningRecipeScriptableObject currentBurningRecipe;

        private void Start()
        {
            state = StoveCounterState.Idle;
        }

        private void Update()
        {
            if (HasKitchenObject())
            {
                switch (state)
                {
                    case StoveCounterState.Idle:
                        break;
                    case StoveCounterState.Frying:
                        fryingTimer += Time.deltaTime;
                
                        if (fryingTimer > currentFryingRecipe.fryingTimerMax)
                        {
                            // Fried
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(currentFryingRecipe.output, this);
                            
                            state = StoveCounterState.Fried;
                            burningTimer = 0;
                            currentBurningRecipe = GetBurningRecipeFromInput(GetKitchenObject().KitchenScriptableObject);
                            
                            OnStateChanged?.Invoke(this, new OnStoveCounterStateChangedEventArgs
                            {
                                State = state
                            });
                        }
                        break;
                    case StoveCounterState.Fried:
                        burningTimer += Time.deltaTime;
                
                        if (burningTimer > currentBurningRecipe.burningTimerMax)
                        {
                            // Fried
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(currentBurningRecipe.output, this);
                            
                            state = StoveCounterState.Burned;
                            
                            OnStateChanged?.Invoke(this, new OnStoveCounterStateChangedEventArgs
                            {
                                State = state
                            });
                        }
                        break;
                    case StoveCounterState.Burned:
                        break;
                }
            }
        }

        public override void Interact(PlayerController player)
        {
            if (!HasKitchenObject())
            {
                // There is no KitchenObject here
                if (player.HasKitchenObject())
                {
                    // Player is carrying something
                    if (HasRecipeFromInput(player.GetKitchenObject().KitchenScriptableObject))
                    {
                        // Player carrying something that can be cut.
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        
                        currentFryingRecipe = GetFryingRecipeFromInput(GetKitchenObject().KitchenScriptableObject);

                        state = StoveCounterState.Frying;
                        fryingTimer = 0;
                        
                        OnStateChanged?.Invoke(this, new OnStoveCounterStateChangedEventArgs
                        {
                            State = state
                        });
                    }
                }
                else
                {
                    // Player not carrying anything
                }
            }
            else
            {
                // There is a KitchenObject here
                if (player.HasKitchenObject())
                {
                    // Player is carrying something
                }
                else
                {
                    // Player not carrying anything
                    GetKitchenObject().SetKitchenObjectParent(player);

                    state = StoveCounterState.Idle;
                    
                    OnStateChanged?.Invoke(this, new OnStoveCounterStateChangedEventArgs
                    {
                        State = state
                    });
                }
            }
        }
        
        private KitchenScriptableObject GetOutputFromInput(KitchenScriptableObject input)
        {
            FryingRecipeScriptableObject fryingRecipe = GetFryingRecipeFromInput(input);
            return fryingRecipe != null ? fryingRecipe.output : null;
        }

        private bool HasRecipeFromInput(KitchenScriptableObject input)
        {
            FryingRecipeScriptableObject fryingRecipe = GetFryingRecipeFromInput(input);
            return fryingRecipe != null;
        }

        private FryingRecipeScriptableObject GetFryingRecipeFromInput(KitchenScriptableObject input)
        {
            foreach (var recipe in fryingRecipes)
            {
                if (recipe.input == input)
                    return recipe;
            }

            return null;
        }
        
        private BurningRecipeScriptableObject GetBurningRecipeFromInput(KitchenScriptableObject input)
        {
            foreach (var recipe in burningRecipes)
            {
                if (recipe.input == input)
                    return recipe;
            }

            return null;
        }
    }
}