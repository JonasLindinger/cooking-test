using UnityEngine;

namespace _Project.Scripts.Object
{
    [CreateAssetMenu()]
    public class BurningRecipeScriptableObject : ScriptableObject
    {
        public KitchenScriptableObject input;
        public KitchenScriptableObject output;
        public float burningTimerMax;
    }
}