using System;
using _Project.Scripts.Kitchen;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class DeliveryCounter : BaseCounter
    {
        public static DeliveryCounter Instance { get; private set; }

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

        public override void Interact(PlayerController player)
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
                {
                    // Only accepts plates
                    
                    DeliveryManager.Instance.DeliverRecipe(plate);
                    player.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
}