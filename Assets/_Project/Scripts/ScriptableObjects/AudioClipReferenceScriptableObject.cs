using UnityEngine;

namespace _Project.Scripts.Object
{
    [CreateAssetMenu()]
    public class AudioClipReferenceScriptableObject : ScriptableObject
    {
        public AudioClip[] chop;
        public AudioClip[] deliveryFail;
        public AudioClip[] deliverySuccess;
        public AudioClip[] footstep;
        public AudioClip[] objectDrop;
        public AudioClip[] objectPickUp;
        public AudioClip stoveSizzle;
        public AudioClip[] trash;
        public AudioClip[] warning;
    }
}