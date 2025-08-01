using System;
using UnityEngine;

namespace _Project.Scripts.Kitchen
{
    public class KitchenGameManager : MonoBehaviour
    {
        public static KitchenGameManager Instance { get; private set; }
        
        // Events
        public event EventHandler OnStateChanged;
        
        // Getters
        public float CountdownToStartTimerTimer => countdownToStartTimer;
        
        public bool IsGamePlaying => state == KitchenGameState.GamePlaying;
        public bool IsCountdownToStartActive => state == KitchenGameState.CountdownToStart;
        public bool IsGameOver => state == KitchenGameState.GameOver;
        public float PlayingTimerNormalized => 1 - (gamePlayingTimer / gamePlayingTimerMax);
        
        private KitchenGameState state = KitchenGameState.WaitingToStart;
        
        private float waitingToStartTimer = 1f;
        private float countdownToStartTimer = 3f;
        private float gamePlayingTimer;
        private float gamePlayingTimerMax = 10f;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one " + GetType().Name + " in the scene!");
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
        }

        private void Update()
        {
            switch (state)
            {
                case KitchenGameState.WaitingToStart:
                    waitingToStartTimer -= Time.deltaTime;
                    if (waitingToStartTimer <= 0f)
                    {
                        state = KitchenGameState.CountdownToStart;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case KitchenGameState.CountdownToStart:
                    countdownToStartTimer -= Time.deltaTime;
                    if (countdownToStartTimer <= 0f)
                    {
                        state = KitchenGameState.GamePlaying;

                        gamePlayingTimer = gamePlayingTimerMax;
                        
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case KitchenGameState.GamePlaying:
                    gamePlayingTimer -= Time.deltaTime;
                    if (gamePlayingTimer <= 0f)
                    {
                        state = KitchenGameState.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case KitchenGameState.GameOver:
                    break;
            }
        }
    }
}