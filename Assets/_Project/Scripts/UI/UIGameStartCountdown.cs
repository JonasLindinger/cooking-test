using System;
using _Project.Scripts.Kitchen;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class UIGameStartCountdown : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text countdownText;

        private void Start()
        {
            KitchenGameManager.Instance.OnStateChanged += KitchenGameManagerOnStateChanged;

            Hide();
        }

        private void Update()
        {
            countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.CountdownToStartTimerTimer).ToString();
        }

        private void KitchenGameManagerOnStateChanged(object sender, EventArgs e)
        {
            if (KitchenGameManager.Instance.IsCountdownToStartActive)
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
            gameObject.SetActive(true);
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}