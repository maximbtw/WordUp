using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WordUp.Views.SettingsView.Control
{
    public class SettingsViewButtonSound : UIBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image targetImage;
        [SerializeField] private Sprite selectedSprite;
        [SerializeField] private Sprite disabledSprite;
        [SerializeField] private bool selected;

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                UpdateSprite();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            this.Selected = !this.Selected;
        }
        
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            
            this.Selected = selected;
        }
#endif

        private void UpdateSprite()
        {
            targetImage.sprite = selected ? selectedSprite : disabledSprite;
        }
    }
}