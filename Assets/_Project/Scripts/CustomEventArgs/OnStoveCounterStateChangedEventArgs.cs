using System;
using _Project.Scripts.Counters;

namespace _Project.Scripts.CustomEventArgs
{
    public class OnStoveCounterStateChangedEventArgs : EventArgs
    {
        public StoveCounterState State;
    }
}