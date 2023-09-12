using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WordUp.UI.PopupMenu
{
    public class PopupMenuItem : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Start()
        {
            button ??= GetComponent<Button>();
        }

        public void BindProperties(UnityAction action)
        {
            button.onClick.AddListener(action);
        }
    }
}