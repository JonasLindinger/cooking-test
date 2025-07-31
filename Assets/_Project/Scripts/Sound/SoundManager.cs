using System;
using _Project.Scripts.Counters;
using _Project.Scripts.Kitchen;
using _Project.Scripts.Object;
using Project.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        
        [Header("Settings")]
        [SerializeField] private AudioClipReferenceScriptableObject audioClipReference;

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

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnOnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerOnRecipeFailed;
            CuttingCounter.OnAnyCut += CuttingCounterOnAnyCut;
            PlayerController.Instance.OnPickedUpSomething += PlayerOnPickedUpSomething;
            BaseCounter.OnAnyObjectPlacedHere += BaseCounterOnAnyObjectPlacedHere;
            TrashCounter.OnAnyObjectTrashed += TrashCounterOnAnyObjectTrashed;
        }

        private void TrashCounterOnAnyObjectTrashed(object sender, EventArgs e)
        {
            if (sender is not TrashCounter) return;
            TrashCounter trashCounter = sender as TrashCounter;
            PlaySound(audioClipReference.chop, trashCounter.transform.position);
        }

        private void BaseCounterOnAnyObjectPlacedHere(object sender, EventArgs e)
        {
            if (sender is not BaseCounter) return;
            BaseCounter baseCounter = sender as BaseCounter;
            PlaySound(audioClipReference.chop, baseCounter.transform.position);
        }

        private void PlayerOnPickedUpSomething(object sender, EventArgs e)
        {
            PlaySound(audioClipReference.objectPickUp, PlayerController.Instance.transform.position);
        }

        private void CuttingCounterOnAnyCut(object sender, EventArgs e)
        {
            if (sender is not CuttingCounter) return;
            CuttingCounter cuttingCounter = sender as CuttingCounter;
            PlaySound(audioClipReference.chop, cuttingCounter.transform.position);
        }

        private void DeliveryManagerOnRecipeFailed(object sender, EventArgs e)
        {
            DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipReference.deliveryFail, deliveryCounter.transform.position);
        }

        private void DeliveryManagerOnOnRecipeSuccess(object sender, EventArgs e)
        {
            DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipReference.deliverySuccess, deliveryCounter.transform.position);
        }

        private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
        {
            if (audioClips == null) return;
            if (audioClips.Length == 0) return;
            PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volume);
        }
        
        private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            if (audioClip == null) return;
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
        
        public void PlayFootstepSound(Vector3 position, float volume = 1f)
        {
            PlaySound(audioClipReference.footstep, position, volume);
        }
    }
}