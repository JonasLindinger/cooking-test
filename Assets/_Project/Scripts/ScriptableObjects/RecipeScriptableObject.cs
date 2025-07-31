using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Object
{
    [CreateAssetMenu()]
    public class RecipeScriptableObject : ScriptableObject
    {
        public string recipeName;
        public List<KitchenScriptableObject> kitchenObjects = new List<KitchenScriptableObject>();
    }
}