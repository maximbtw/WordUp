using UnityEngine;
using UnityEngine.SceneManagement;

namespace WordUp.Shared
{
    public class ButtonEventHelpers : MonoBehaviour
    {
        public void DestroyObject(GameObject objectToDestroy)
        {
            Destroy(objectToDestroy);
        }
    }
}