using UnityEngine;
using UnityEngine.EventSystems;

namespace WordUp.UI.CheckBox
{
    public class CheckBoxSliderAnimator : UIBehaviour
    {
        [SerializeField] private Transform targetObject;
        [SerializeField] private Vector2 normalPosition;
        [SerializeField] private Vector2 selectedPosition;

        [SerializeField] [Range(0.005f, 0.05f)]
        private float speed;

        private bool _selected;

        protected override void Awake()
        {
            if (targetObject == null)
            {
                targetObject = gameObject.transform;
            }
        }

        private void Update()
        {
            Vector2 currentPosition = targetObject.localPosition;
            var rightPosition = _selected ? selectedPosition : normalPosition;

            if (!currentPosition.Equals(rightPosition))
            {
                targetObject.localPosition = Vector2.Lerp(currentPosition, rightPosition, speed);
            }
        }

        public void PlayAnimation(bool selected)
        {
            _selected = selected;
            
            targetObject.localPosition = selected ? selectedPosition : normalPosition;
        }
    }
}