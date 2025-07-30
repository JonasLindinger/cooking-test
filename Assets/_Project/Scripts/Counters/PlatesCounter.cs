using System;
using _Project.Scripts.Kitchen;
using _Project.Scripts.Object;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class PlatesCounter : BaseCounter
    {
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;
        
        [Header("Settings")]
        [SerializeField] private float spawnPlateTimerMax = 4;
        [SerializeField] private int platesSpawnedAmountMax = 4;
        [Space(10)]
        [Header("References")]
        [SerializeField] private KitchenScriptableObject plateKitchenObject;
        
        private float spawnPlateTimer;

        private int platesSpawnedAmount;

        private void Update()
        {
            spawnPlateTimer += Time.deltaTime;
            
            if (spawnPlateTimer > spawnPlateTimerMax)
            {
                spawnPlateTimer = 0;

                if (platesSpawnedAmount < platesSpawnedAmountMax)
                {
                    platesSpawnedAmount++;
                    
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override void Interact(PlayerController player)
        {
            if (!player.HasKitchenObject())
            {
                // Player is empty handed
                if (platesSpawnedAmount > 0)
                {
                    // There's at least one plate here
                    platesSpawnedAmount--;
                    
                    KitchenObject.SpawnKitchenObject(plateKitchenObject, player);
                    
                    OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}