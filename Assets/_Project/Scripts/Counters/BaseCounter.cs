using System;
using Project.Player;
using UnityEngine;

namespace _Project.Scripts.Counters
{
    public class BaseCounter : MonoBehaviour
    {
        public virtual void Interact(PlayerController player)
        {
            throw new NotImplementedException();
        }
    }
}