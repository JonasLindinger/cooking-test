using _Project.Scripts.CustomEventArgs;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Counters
{
    public class UIProgressBar : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private CuttingCounter cuttingCounter;
        [SerializeField] private Image progressBar;

        private void Start()
        {
            cuttingCounter.OnProgressChanged += CuttingCounterOnOnProgressChanged;

            progressBar.fillAmount = 0;
            Hide();
        }

        private void CuttingCounterOnOnProgressChanged(object sender, OnProgressChangedEventArgs e)
        {
            progressBar.fillAmount = e.ProgressNormalized;

            if (e.ProgressNormalized == 0 || e.ProgressNormalized == 1)
                Hide();
            else
                Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}