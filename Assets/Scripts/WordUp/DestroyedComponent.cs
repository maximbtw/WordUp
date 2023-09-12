using UnityEngine;
using UnityEngine.Events;

namespace WordUp
{
    public class DestroyedComponent : MonoBehaviour
    {
        public UnityEvent onDestroyObject;
        
        private void OnDestroy()
        {
            onDestroyObject?.Invoke();
        }
    }
}