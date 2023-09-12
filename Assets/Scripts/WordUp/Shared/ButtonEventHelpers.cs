using UnityEngine;
using UnityEngine.SceneManagement;

namespace WordUp.Shared
{
    public class ButtonEventHelpers : MonoBehaviour
    {
        public void LoadSSceneAdditive(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
        }
        
        public void DestroyObject(GameObject objectToDestroy)
        {
            Destroy(objectToDestroy);
        }
    }
}