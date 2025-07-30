using _Project.Scripts.Kitchen;
using Project.Player;

namespace _Project.Scripts.Counters
{
    public class ClearCounter : BaseCounter
    {
        public override void Interact(PlayerController player)
        {
            if (!HasKitchenObject())
            {
                // There is no KitchenObject here
                if (player.HasKitchenObject())
                {
                    // Player is carrying something
                    player.GetKitchenObject().SetKitchenObjectParent(this);
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
                    else
                    {
                        // Player is not carrying a Plate but something else
                        if (GetKitchenObject().TryGetPlate(out plate))
                        {
                            // Counter is holding a Plate
                            if (plate.TryAddIngredient(player.GetKitchenObject().KitchenScriptableObject))
                            {
                                player.GetKitchenObject().DestroySelf();
                            }
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
    }
}