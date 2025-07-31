using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Sound
{
    public class PlayerSounds : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float footstepTimerMax = .1f;
        
        // Helper variables
        private float footstepTimer;
        
        // References
        private PlayerController player;

        private void Awake()
        {
            player = GetComponent<PlayerController>();
        }

        private void Update()
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                footstepTimer = footstepTimerMax;

                if (player.IsWalking)
                {
                    // Play footstep sound
                    float volume = 1f;
                    SoundManager.Instance.PlayFootstepSound(player.transform.position, volume);
                }
            }
        }
    }
}