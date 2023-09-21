using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace WordUp.UI.ConfirmedMessageBox
{
    public class ConfirmedMessageBox : DestroyedComponent
    {
        [SerializeField] private TextMeshProUGUI textMessage;
        
        private UnityAction _onApplyAction;
        
        public void Construct(string message, UnityAction onApplyAction)
        {
            textMessage.text = message;
            _onApplyAction = onApplyAction;
        }
        
        public void OnApplyClick()
        {
            _onApplyAction();
            
            Destroy();
        }
    }
}