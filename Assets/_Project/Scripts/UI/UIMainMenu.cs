using System;
using _Project.Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            playButton.onClick.AddListener(() =>
            {
                // Click
                Loader.Load(Scene.Game);
            });
            
            quitButton.onClick.AddListener(() =>
            {
                // Quit
                Application.Quit();
            });
        }

        private void Start()
        {
            Time.timeScale = 1f;
        }
    }
}