using _Project.Scripts.Kitchen;
using Project.Player;

namespace _Project.Scripts.Counters
{
    public class DeliveryCounter : BaseCounter
    {
        public override void Interact(PlayerController player)
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
                {
                    player.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
}