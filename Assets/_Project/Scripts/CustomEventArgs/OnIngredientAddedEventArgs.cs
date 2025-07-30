using System;
using _Project.Scripts.Object;

namespace _Project.Scripts.CustomEventArgs
{
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenScriptableObject KitchenObject;
    }
}