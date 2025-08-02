using System;
using System.ComponentModel;
using _Project.Scripts.CustomEventArgs;
using _Project.Scripts.Helper;
using _Project.Scripts.Kitchen;
using _Project.Scripts.Object;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class CuttingCounter : BaseCounter, IHasProgress
    {
        public static event EventHandler OnAnyCut;
        
        public new static void ResetStaticData()
        {
            OnAnyCut = null;
        }
        
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler OnCut;
        
        [Header("Settings")]
        [SerializeField] private CuttingRecipeScriptableObject[] cuttingRecipes;

        private int cuttingProgress;
        
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
                        cuttingProgress = 0;
                        
                        CuttingRecipeScriptableObject cuttingRecipe = GetCuttingRecipeFromInput(GetKitchenObject().KitchenScriptableObject);
                        
                        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                        {
                            ProgressNormalized = (float) cuttingProgress / cuttingRecipe.cuttingProgressMax,
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
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
                    {
                        // Player is holding a Plate
                        if (plate.TryAddIngredient(GetKitchenObject().KitchenScriptableObject))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                }
                else
                {
                    // Player not carrying anything
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }

        public override void InteractAlternate(PlayerController player)
        {
            if (HasKitchenObject() && HasRecipeFromInput(GetKitchenObject().KitchenScriptableObject))
            {
                // There is a KitchenObject here and it can be cut
                cuttingProgress++;
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);
                
                CuttingRecipeScriptableObject cuttingRecipe = GetCuttingRecipeFromInput(GetKitchenObject().KitchenScriptableObject);
                
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    ProgressNormalized = (float) cuttingProgress / cuttingRecipe.cuttingProgressMax,
                });
                
                if (cuttingProgress >= cuttingRecipe.cuttingProgressMax)
                {
                    KitchenScriptableObject outputKitchenObject = GetOutputFromInput(GetKitchenObject().KitchenScriptableObject);
                
                    GetKitchenObject().DestroySelf();
                
                    KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
                }
            }
        }

        private KitchenScriptableObject GetOutputFromInput(KitchenScriptableObject input)
        {
            CuttingRecipeScriptableObject cuttingRecipe = GetCuttingRecipeFromInput(input);
            return cuttingRecipe != null ? cuttingRecipe.output : null;
        }

        private bool HasRecipeFromInput(KitchenScriptableObject input)
        {
            CuttingRecipeScriptableObject cuttingRecipe = GetCuttingRecipeFromInput(input);
            return cuttingRecipe != null;
        }

        private CuttingRecipeScriptableObject GetCuttingRecipeFromInput(KitchenScriptableObject input)
        {
            foreach (var recipe in cuttingRecipes)
            {
                if (recipe.input == input)
                    return recipe;
            }

            return null;
        }
    }
}