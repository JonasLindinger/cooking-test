using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Object
{
    // [CreateAssetMenu()]
    public class RecipeListScriptableObject : ScriptableObject
    {
        public List<RecipeScriptableObject> recipes = new List<RecipeScriptableObject>();
    }
}