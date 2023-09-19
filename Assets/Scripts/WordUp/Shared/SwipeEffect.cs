using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WordUp.Shared
{
    public class SwipeEffect : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image targetImage;

        public UnityEvent swipeLeft;
        public UnityEvent swipeRight;

        private Vector3 _initialPosition;
        private float _distanceMoved;
        private bool _swipeLeft;

        private void Start()
        {
            targetImage ??= GetComponent<Image>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            var localPosition = transform.localPosition;

            transform.localPosition = new Vector2(localPosition.x + eventData.delta.x, localPosition.y);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _initialPosition = transform.localPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _distanceMoved = Mathf.Abs(transform.localPosition.x - _initialPosition.x);
            if (_distanceMoved < 0.1 * Screen.width)
            {
                transform.localPosition = _initialPosition;
            }
            else
            {
                _swipeLeft = !(transform.localPosition.x > _initialPosition.x);

                StartCoroutine(MovedCard());
            }
        }

        private IEnumerator MovedCard()
        {
            float time = 0;
            while (targetImage.color != new Color(1, 1, 1, 0))
            {
                Vector3 localPosition = transform.localPosition;

                time += Time.deltaTime;

                float toPosition = _swipeLeft ? localPosition.x - Screen.width : localPosition.x + Screen.width;

                transform.localPosition = new Vector3(
                    x: Mathf.SmoothStep(localPosition.x,
                    to:toPosition,
                    t: time),
                    y: localPosition.y,
                    z: 0);

                targetImage.color = new Color(1, 1, 1, Mathf.SmoothStep(1, 0, 4 * time));

                yield return null;
            }

            if (_swipeLeft)
            {
                swipeLeft?.Invoke();
            }
            else
            {
                swipeRight?.Invoke();
            }

            Destroy(gameObject);
        }
    }
}