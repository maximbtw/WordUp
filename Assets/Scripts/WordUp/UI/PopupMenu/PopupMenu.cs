using UnityEngine.Events;
using Zenject;

namespace WordUp.UI.PopupMenu
{
    public class PopupMenu : DestroyedComponent
    {
        public void AddActionListenerToAllButtons(UnityAction action)
        {
            PopupMenuItem[] items = GetComponentsInChildren<PopupMenuItem>();

            foreach (PopupMenuItem popupMenuItem in items)
            {
                popupMenuItem.BindProperties(action);
            }
        }
        
        [Inject]
        private void Init()
        {
            AddActionListenerToAllButtons(() => Destroy(this.gameObject));
        }
    }
}