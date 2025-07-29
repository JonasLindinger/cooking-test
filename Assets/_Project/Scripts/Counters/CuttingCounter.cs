using _Project.Scripts.Kitchen;
using _Project.Scripts.Object;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private CuttingRecipeScriptableObject[] cuttingRecipes;
        
        public override void Interact(PlayerController player)
        {
            if (!HasKitchenObject())
            {
                // There is no KitchenObject here
                if (player.HasKitchenObject())
                {
                    // Player is carrying something
                    if (HasRecipeWithInput(player.GetKitchenObject().KitchenScriptableObject))
                    {
                        // Player carrying something that can be cut.
                        player.GetKitchenObject().SetKitchenObjectParent(this);
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
                }
            }
        }

        public override void InteractAlternate(PlayerController player)
        {
            if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().KitchenScriptableObject))
            {
                // There is a KitchenObject here and it can be cut
                KitchenScriptableObject outputKitchenObject = GetOutputFromInput(GetKitchenObject().KitchenScriptableObject);
                
                GetKitchenObject().DestroySelf();
                
                KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
            }
        }

        private KitchenScriptableObject GetOutputFromInput(KitchenScriptableObject input)
        {
            foreach (var recipe in cuttingRecipes)
            {
                if (recipe.input == input)
                    return recipe.output;
            }

            return null;
        }

        private bool HasRecipeWithInput(KitchenScriptableObject input)
        {
            return GetOutputFromInput(input) != null;
        }
    }
}