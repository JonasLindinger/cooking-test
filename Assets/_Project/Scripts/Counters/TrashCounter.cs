using Project.Player;

namespace _Project.Scripts.Counters
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(PlayerController player)
        {
            if (player.HasKitchenObject())
                player.GetKitchenObject().DestroySelf();
        }   
    }
}