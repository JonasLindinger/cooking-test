using _Project.Scripts.Object;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField] private KitchenScriptableObject kitchenObjectData;
        
        public override void Interact(PlayerController player)
        {
            
        }
    }
}