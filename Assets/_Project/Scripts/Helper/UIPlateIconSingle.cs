using _Project.Scripts.Object;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Helper
{
    public class UIPlateIconSingle : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image image;
        
        public void SetKitchenScriptableObject(KitchenScriptableObject kitchenObject)
        {
            image.sprite = kitchenObject.sprite;   
        }
    }
}