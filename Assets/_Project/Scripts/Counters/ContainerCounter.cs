using System;
using _Project.Scripts.Kitchen;
using _Project.Scripts.Object;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class ContainerCounter : BaseCounter
    {
        public event EventHandler OnPlayerGrabbedObject;
        
        [SerializeField] private KitchenScriptableObject kitchenObjectData;

        public override void Interact(PlayerController player)
        {
            if (!player.HasKitchenObject())
            {
                // Player is not carrying anything
                Transform kitchenObjectTransform = Instantiate(kitchenObjectData.prefab).transform;
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}