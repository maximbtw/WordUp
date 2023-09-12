using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WordUp.UI.CheckBox
{
    [ExecuteAlways]
    [SelectionBase]
    public class CheckBox : UIBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public enum State
        {
            Normal,
            Selected,
            HighlightedNormal,
            HighlightedSelected,
            Disabled
        }

        [SerializeField] private CheckBoxColorBlock colorBlock = CheckBoxColorBlock.DefaultColorBlock;
        [SerializeField] private Graphic targetGraphic;
        [SerializeField] private bool selected;
        
        private bool _isPointerInside;

        public CheckBoxColorBlock ColorBlock
        {
            set
            {
                colorBlock = value;
                UpdateState();
            }
        }

        public Graphic TargetGraphic
        {
            set
            {
                targetGraphic = value == null ? gameObject.GetComponent<Graphic>() : value;
                UpdateState();
            }
        }

        public bool Seleted
        {
            get => selected;
            set
            {
                selected = value;
                UpdateState();
            }
        }

        public State CurrentState
        {
            get
            {
                if (!enabled)
                {
                    return State.Disabled;
                }

                if (selected)
                {
                    return _isPointerInside ? State.HighlightedSelected : State.Selected;
                }

                return _isPointerInside ? State.HighlightedNormal : State.Normal;
            }
        }

        protected override void Awake()
        {
            if (targetGraphic == null)
            {
                targetGraphic = GetComponent<Graphic>();
            }
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

        public void OnPointerDown(PointerEventData eventData)
        {
            selected = !selected;

            OnClick?.Invoke(selected);

            UpdateState();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerInside = true;

            UpdateState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPointerInside = false;

            UpdateState();
        }

        public UnityEvent<bool> OnClick;

        private void UpdateState()
        {
            var color = Color.clear;

            switch (CurrentState)
            {
                case State.Normal:
                    color = colorBlock.normalColor;
                    break;
                case State.Selected:
                    color = colorBlock.selectedColor;
                    break;
                case State.HighlightedNormal:
                    color = colorBlock.highlightedNormalColor;
                    break;
                case State.HighlightedSelected:
                    color = colorBlock.highlightedSelectedColor;
                    break;
                case State.Disabled:
                    color = colorBlock.disabledColor;
                    break;
            }

            UpdateGraphic(color);
        }

        private void UpdateGraphic(Color color)
        {
            targetGraphic.CrossFadeColor(color, 0f, true, true);
        }
    }
}