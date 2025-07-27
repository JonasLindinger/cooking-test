using _Project.Scripts.CustomEventArgs;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter counter;
        [SerializeField] private GameObject normalVisual;
        [SerializeField] private GameObject selectedVisual;
        
        private void Start()
        {
            PlayerController.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
            Hide();
        }

        private void OnSelectedCounterChanged(object sender, OnSelectedCounterChangedEventArgs e)
        {
            if (e.SelectedCounter == counter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            normalVisual.SetActive(false);
            selectedVisual.SetActive(true);
        }

        private void Hide()
        {
            normalVisual.SetActive(true);
            selectedVisual.SetActive(false);
        }
    }
}