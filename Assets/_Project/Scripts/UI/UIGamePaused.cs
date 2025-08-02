using System;
using _Project.Scripts.Kitchen;
using _Project.Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class UIGamePaused : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;
        
        private void Start()
        {
            resumeButton.onClick.AddListener(() =>
            {
                KitchenGameManager.Instance.TogglePauseGame();
            });
            
            mainMenuButton.onClick.AddListener(() =>
            {
                Loader.Load(Scene.MainMenu);
            });
            
            KitchenGameManager.Instance.OnGamePaused += KitchenGameManagerOnGamePaused;
            KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManagerOnGameUnpaused;
            
            Hide();
        }

        private void KitchenGameManagerOnGamePaused(object sender, EventArgs e)
        {
            Show();
        }
        
        private void KitchenGameManagerOnGameUnpaused(object sender, EventArgs e)
        {
            Hide();
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