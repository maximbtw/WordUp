using UnityEngine;
using UnityEngine.SceneManagement;

namespace WordUp.Shared
{
    public class SceneSwitcher : MonoBehaviour
    {
        public void Invoke(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}