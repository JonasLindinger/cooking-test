using _Project.Scripts.CustomEventArgs;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter clearCounter;
        [SerializeField] private MeshRenderer visualGameObject;
        [SerializeField] private Material selectedMaterial;
        [SerializeField] private Material unselectedMaterial;
        
        private void Start()
        {
            PlayerController.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
            Hide();
        }

        private void OnSelectedCounterChanged(object sender, OnSelectedCounterChangedEventArgs e)
        {
            if (e.SelectedCounter == clearCounter)
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
            visualGameObject.material = selectedMaterial;
        }

        private void Hide()
        {
            visualGameObject.material = unselectedMaterial;
        }
    }
}