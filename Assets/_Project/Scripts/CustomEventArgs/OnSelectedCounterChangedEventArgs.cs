using System;
using _Project.Scripts.Counters;

namespace _Project.Scripts.CustomEventArgs
{
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }
}