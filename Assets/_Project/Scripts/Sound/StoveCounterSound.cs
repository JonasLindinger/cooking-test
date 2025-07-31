using System;
using _Project.Scripts.Counters;
using _Project.Scripts.CustomEventArgs;
using UnityEngine;

namespace _Project.Scripts.Sound
{
    public class StoveCounterSound : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private StoveCounter stoveCounter;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounterOnStateChanged;
        }

        private void StoveCounterOnStateChanged(object sender, OnStoveCounterStateChangedEventArgs e)
        {
            bool playSound = e.State == StoveCounterState.Frying || e.State == StoveCounterState.Fried;

            if (playSound)
                audioSource.Play();
            else
                audioSource.Pause();
        }
    }
}