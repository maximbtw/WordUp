using UnityEngine;
using UnityEngine.SceneManagement;

namespace WordUp.Views
{
    public abstract class SceneControllerBase : MonoBehaviour
    {
        private void Start()
        {
            var staticDataHandler = new SceneStaticDataLoader.DataHandler
            {
                GetData = GetDataFromScene,
                LoadData = LoadDataFromScene
            };
            
            SceneStaticDataLoader.OpenScene(staticDataHandler);

            LateStart();
        }

        protected virtual void LateStart()
        {
        }

        protected virtual object GetDataFromScene()
        {
            return null;
        }

        protected virtual void LoadDataFromScene(object data)
        {
        }
        
        public void LoadSceneAdditive(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
        }
        
        public void UnloadScene(int sceneBuildIndex)
        {
            SceneStaticDataLoader.CloseScene();
            
            SceneManager.UnloadSceneAsync(sceneBuildIndex);
        }
    }
}