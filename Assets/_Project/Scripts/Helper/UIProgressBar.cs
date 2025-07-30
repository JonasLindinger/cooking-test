using _Project.Scripts.Counters;
using _Project.Scripts.CustomEventArgs;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Helper
{
    public class UIProgressBar : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject hasProgressGameObject;
        [SerializeField] private Image progressBar;

        private IHasProgress hasProgress; 
        
        private void Start()
        {
            hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
            if (hasProgress == null)
            {
                Debug.LogError("HasProgress component not found");
                return;
            }
            
            hasProgress.OnProgressChanged += OnProgressChanged;

            progressBar.fillAmount = 0;
            Hide();
        }

        private void OnProgressChanged(object sender, OnProgressChangedEventArgs e)
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