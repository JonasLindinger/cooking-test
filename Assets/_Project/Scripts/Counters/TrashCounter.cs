using System;
using Project.Player;

namespace _Project.Scripts.Counters
{
    public class TrashCounter : BaseCounter
    {
        // Events
        public static event EventHandler OnAnyObjectTrashed;
        
        public new static void ResetStaticData()
        {
            OnAnyObjectTrashed = null;
        }
        
        public override void Interact(PlayerController player)
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestroySelf();
                
                OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
            }
        }   
    }
}