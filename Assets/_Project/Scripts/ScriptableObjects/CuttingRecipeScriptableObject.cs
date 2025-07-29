using UnityEngine;

namespace _Project.Scripts.Object
{
    [CreateAssetMenu()]
    public class CuttingRecipeScriptableObject : ScriptableObject
    {
        public KitchenScriptableObject input;
        public KitchenScriptableObject output;
    }
}