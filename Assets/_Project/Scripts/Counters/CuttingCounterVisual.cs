using System;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    [RequireComponent(typeof(Animator))]
    public class CuttingCounterVisual : MonoBehaviour
    {
        private const string CUT = "Cut";
        
        [SerializeField] private CuttingCounter cuttingCounter;
        
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            cuttingCounter.OnCut += OnCut;
        }

        private void OnCut(object sender, EventArgs e)
        {
            animator.SetTrigger(CUT);
        }
    }
}