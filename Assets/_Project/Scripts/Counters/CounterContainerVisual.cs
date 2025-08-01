﻿using System;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    [RequireComponent(typeof(Animator))]
    public class CounterContainerVisual : MonoBehaviour
    {
        private const string OPEN_CLOSE = "OpenClose";
        
        [SerializeField] private ContainerCounter containerCounter;
        
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            containerCounter.OnPlayerGrabbedObject += OnPlayerGrabbedObject;
        }

        private void OnPlayerGrabbedObject(object sender, EventArgs e)
        {
            animator.SetTrigger(OPEN_CLOSE);
        }
    }
}