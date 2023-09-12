using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WordUp.Views.ProfileView.Control
{
    public class ProfileViewLabel : UIBehaviour
    {
        [SerializeField] private TextMeshProUGUI modifiedText;

        public void SetText(string text)
        {
            modifiedText.text = text;
        }
    }
}