using UnityEngine;

namespace _Project.Scripts.Object
{
    [CreateAssetMenu()]
    public class KitchenObject : ScriptableObject
    {
        public string objectName;
        public Sprite sprite;
        public GameObject prefab;
    }
}