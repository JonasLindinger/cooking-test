using _Project.Scripts.Kitchen;
using _Project.Scripts.Object;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class ContainerCounter : BaseCounter
    {
        [SerializeField] private KitchenScriptableObject kitchenObjectData;

        public override void Interact(PlayerController player)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectData.prefab).transform;
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(player);
        }
    }
}