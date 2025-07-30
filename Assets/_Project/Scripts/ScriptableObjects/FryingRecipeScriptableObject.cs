using UnityEngine;

namespace _Project.Scripts.Object
{
    [CreateAssetMenu()]
    public class FryingRecipeScriptableObject : ScriptableObject
    {
        public KitchenScriptableObject input;
        public KitchenScriptableObject output;
        public int fryingTimerMax;
    }
}