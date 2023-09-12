using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WordUp.Views
{
    public abstract class SceneControllerBase : MonoBehaviour
    {
        protected virtual void Start()
        {
            StaticDataLoader.OpenScene(this);
        }

        public virtual object GetDataFromScene()
        {
            return null;
        }

        public virtual void LoadDataFromScene(object data){
            
        }
        
        public void LoadSceneAdditive(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
        }
        
        public void UnloadScene(int sceneBuildIndex)
        {
            StaticDataLoader.CloseScene();
            
            SceneManager.UnloadSceneAsync(sceneBuildIndex);
        }
    }
    
    internal static class StaticDataLoader
    {
        private static object _data;

        private static readonly Stack<SceneControllerBase> StackActiveScenes = new();

        internal static void CloseScene()
        {
            var closetScene = StackActiveScenes.Pop();
            
            object data = closetScene.GetDataFromScene();
            
            SceneControllerBase lastOpenScene = StackActiveScenes.Peek();
            
            lastOpenScene.LoadDataFromScene(data);
        }

        internal static void OpenScene(SceneControllerBase scene)
        {
            if (!StackActiveScenes.Any())
            {
                StackActiveScenes.Push(scene);
                
                return;
            }
            
            SceneControllerBase lastOpenScene = StackActiveScenes.Peek();
            
            object data = lastOpenScene.GetDataFromScene();
            
            scene.LoadDataFromScene(data);
            
            StackActiveScenes.Push(scene);
        }
    }
}