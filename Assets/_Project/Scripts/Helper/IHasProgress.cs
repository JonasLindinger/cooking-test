using System;
using _Project.Scripts.CustomEventArgs;

namespace _Project.Scripts.Helper
{
    public interface IHasProgress
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    }
}