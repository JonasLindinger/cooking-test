using UnityEngine;

namespace _Project.Scripts.SceneManagement
{
    public class LoaderCallback : MonoBehaviour
    {
        private bool isFirstUpdate = true;

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;

                Loader.LoaderCallback();
            }
        }
    }
}