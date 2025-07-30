using _Project.Scripts.CustomEventArgs;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class StoveCounterVisual : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private StoveCounter stoveCounter;
        [Space(5)]
        [SerializeField] private GameObject stoveOnVisual;
        [SerializeField] private GameObject particles;

        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
        }

        private void StoveCounterOnOnStateChanged(object sender, OnStoveCounterStateChangedEventArgs e)
        {
            bool showVisual = e.State == StoveCounterState.Frying || e.State == StoveCounterState.Fried;
            stoveOnVisual.SetActive(showVisual);
            particles.SetActive(showVisual);
        }
    }
}