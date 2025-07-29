using _Project.Scripts.Kitchen;
using _Project.Scripts.Object;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private KitchenScriptableObject cutKitchenObjectSo;
        
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
            if (HasKitchenObject())
            {
                // There is a KitchenObject here
                GetKitchenObject().DestroySelf();
                
                KitchenObject.SpawnKitchenObject(cutKitchenObjectSo, this);
            }
        }
    }
}