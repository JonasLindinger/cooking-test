using UnityEngine;

namespace _Project.Scripts.Object
{
    [CreateAssetMenu()]
    public class KitchenScriptableObject : ScriptableObject
    {
        public string objectName;
        public Sprite sprite;
        public GameObject prefab;
    }
}