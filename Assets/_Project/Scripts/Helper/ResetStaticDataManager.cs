using _Project.Scripts.Counters;
using UnityEngine;

namespace _Project.Scripts.Helper
{
    public class ResetStaticDataManager : MonoBehaviour
    {
        private void Start()
        {
            CuttingCounter.ResetStaticData();
            BaseCounter.ResetStaticData();
            TrashCounter.ResetStaticData();
        }
    }
}