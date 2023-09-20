using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WordUp.Views.LearnMenuView
{
    public class LearnMenuGroupItemVisualization : UIBehaviour
    {
        [SerializeField] private Color defaultTextColor;
        [SerializeField] private Color completedTextColor;
        [SerializeField] private Sprite defaultBackgroundSprite;
        [SerializeField] private Sprite completedBackgroundSprite;
        [SerializeField] private Sprite defaultIconSprite;
        [SerializeField] private Sprite completedIconSprite;
        [Space]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI numberTextMP;
        [SerializeField] private TextMeshProUGUI statTextMP;
        [SerializeField] private Outline outline;

        private LearnMenuGroupItem _item;

        protected override void Awake()
        {
            BindDataUpdate();
        }

        private void BindingControls(LearnMenuGroupItemData data)
        {
            if (data.learnedCount == data.count)
            {
                backgroundImage.sprite = completedBackgroundSprite;
                iconImage.sprite = completedIconSprite;
                numberTextMP.color = completedTextColor;
                statTextMP.color = completedTextColor;
                statTextMP.fontStyle = FontStyles.Bold;
                outline.effectColor = completedTextColor;
            }
            else
            {
                backgroundImage.sprite = defaultBackgroundSprite;
                iconImage.sprite = defaultIconSprite;
                numberTextMP.color = defaultTextColor;
                statTextMP.color = defaultTextColor;
                statTextMP.fontStyle = FontStyles.Normal;
                outline.effectColor = defaultTextColor;
            }

            numberTextMP.text = data.number.ToString();
            statTextMP.text = $"{data.learnedCount}/{data.count}";
        }

        private void BindDataUpdate()
        {
            if (_item == null)
            {
                _item ??= GetComponent<LearnMenuGroupItem>();

                _item.onDataUpdated.AddListener(BindingControls);
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            BindDataUpdate();
        }
#endif
    }
}