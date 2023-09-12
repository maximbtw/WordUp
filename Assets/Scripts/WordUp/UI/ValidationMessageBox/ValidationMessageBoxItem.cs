using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace WordUp.UI.ValidationMessageBox
{
    public class ValidationMessageBoxItem : MonoBehaviour
    {
        [SerializeField] private Image coloredImage;
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        public void BindProperties(Color colorImage, string messageText)
        {
            coloredImage.color = colorImage;
            textMeshProUGUI.text = messageText;
        }
        
        [Inject]
        private void Init()
        {
            coloredImage ??= GetComponentInChildren<Image>();
            textMeshProUGUI ??= GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}