using UnityEngine;
using UnityEngine.Events;

namespace WordUp
{
    public class DestroyedComponent : MonoBehaviour
    {
        public UnityEvent onDestroyObject;
        public void Destroy()
        {
            onDestroyObject?.Invoke();
            
            Destroy(this.gameObject);
        }
    }
}