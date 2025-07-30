using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlatesCounter platesCounter;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private GameObject plateVisualPrefab;
        [Space(10)]
        [SerializeField] private float plateOffsetY = 0.03f;
        
        private List<GameObject> plateVisuals = new List<GameObject>();

        private void Start()
        {
            platesCounter.OnPlateSpawned += PlatesCounterOnOnPlateSpawned;
            platesCounter.OnPlateRemoved += PlatesCounterOnOnPlateRemoved;
        }

        private void PlatesCounterOnOnPlateSpawned(object sender, EventArgs e)
        {
            Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint).transform;
            
            plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisuals.Count, 0);
            
            plateVisuals.Add(plateVisualTransform.gameObject);
        }
        
        private void PlatesCounterOnOnPlateRemoved(object sender, EventArgs e)
        {
            GameObject plateVisual = plateVisuals[plateVisuals.Count - 1];
            plateVisuals.Remove(plateVisual);
            
            Destroy(plateVisual);
        }
    }
}