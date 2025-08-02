using UnityEngine.SceneManagement;

namespace _Project.Scripts.SceneManagement
{
    public static class Loader
    {
        private static Scene targetScene;
        
        public static void Load(Scene targetScene)
        {
            Loader.targetScene = targetScene;
            
            SceneManager.LoadScene(Scene.Loading.ToString());
        }

        public static void LoaderCallback()
        {
            SceneManager.LoadScene(targetScene.ToString());
        }
    }
}