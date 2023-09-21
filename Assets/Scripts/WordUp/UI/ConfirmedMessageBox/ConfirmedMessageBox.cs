using TMPro;
using UnityEngine;
using UnityEngine.Events;
using WordUp.Shared.StaticShared;

namespace WordUp.UI.ConfirmedMessageBox
{
    public class ConfirmedMessageBox : DestroyedComponent
    {
        [SerializeField] private TextMeshProUGUI textMessage;
        
        public UnityEvent onApplyAction;
        
        public void Construct(string message)
        {
            textMessage.text = message;
        }
        
        public void OnApplyClick()
        {
            onApplyAction.Invoke();
            
            Destroy();
        }
    }
}