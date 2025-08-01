using System;
using _Project.Scripts.Kitchen;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class UIGameOver : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text recipesDeliveredText;
        
        private void Start()
        {
            KitchenGameManager.Instance.OnStateChanged += KitchenGameManagerOnStateChanged;

            Hide();
        }

        private void KitchenGameManagerOnStateChanged(object sender, EventArgs e)
        {
            if (KitchenGameManager.Instance.IsGameOver)
            {
                Show();
                
                recipesDeliveredText.text = DeliveryManager.Instance.SuccessfulRecipesAmount.ToString();
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