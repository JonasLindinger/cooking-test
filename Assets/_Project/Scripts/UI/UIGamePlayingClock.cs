using _Project.Scripts.Kitchen;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class UIGamePlayingClock : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image timerImage;

        private void Update()
        {
            timerImage.fillAmount = KitchenGameManager.Instance.PlayingTimerNormalized;
        }
    }
}