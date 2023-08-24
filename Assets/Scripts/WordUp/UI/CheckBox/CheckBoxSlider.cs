using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WordUp.UI.CheckBox
{
    [ExecuteAlways]
    [SelectionBase]
    public class CheckBoxSlider : UIBehaviour
    {
        [SerializeField] private CheckBoxColorBlock colorBlock = CheckBoxColorBlock.DefaultColorBlock;
        [SerializeField] private Graphic targetGraphic;
        [SerializeField] private bool selected;
        [SerializeField] private string text;
        
        private CheckBoxSliderAnimator _animator;
        private CheckBox _checkBox;
        private TextMeshProUGUI _text;

        public CheckBoxColorBlock ColorBlock
        {
            set
            {
                colorBlock = value;
                SetProperties();
            }
        }

        public Graphic TargetGraphic
        {
            set
            {
                targetGraphic = value == null ? gameObject.GetComponent<Graphic>() : value;
                SetProperties();
            }
        }

        public bool Seleted
        {
            get => selected;
            set
            {
                selected = value;
                SetProperties();
                OnSelectedChanged();
            }
        }

        protected override void Start()
        {
            SetProperties();
            _checkBox.OnClick += OnSelectedChanged;
        }

        protected void Update()
        {
            _text.text = text;
        }
        
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            TargetGraphic = targetGraphic;
            ColorBlock = colorBlock;
            Seleted = selected;
        }
#endif

        private void SetProperties()
        {
            _text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            _checkBox = gameObject.GetComponentInChildren<CheckBox>();
            _animator = gameObject.GetComponent<CheckBoxSliderAnimator>();

            _checkBox.ColorBlock = colorBlock;
            _checkBox.TargetGraphic = targetGraphic;
            _checkBox.Seleted = selected;
        }

        private void OnSelectedChanged()
        {
            Debug.Log("Click");

            _animator.PlayAnimation(selected);
        }
    }
}