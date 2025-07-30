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
        }

        private void CuttingCounterOnOnProgressChanged(object sender, OnProgressChangedEventArgs e)
        {
            progressBar.fillAmount = e.ProgressNormalized;
        }
    }
}