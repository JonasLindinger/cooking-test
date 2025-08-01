using UnityEngine;
using UnityEngine.SceneManagement;
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
                SceneManager.LoadScene(1);
            });
            
            quitButton.onClick.AddListener(() =>
            {
                // Quit
                Application.Quit();
            });
        }
    }
}