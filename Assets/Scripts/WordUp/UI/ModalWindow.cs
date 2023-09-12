using UnityEngine;
using UnityEngine.UI;

namespace WordUp.UI
{
    public class ModalWindow
    {
        public static GameObject Construct(Canvas canvas, DestroyedComponent children,
            bool readOnly, Color modalWindowColor)
        {
            var modalWindow = new GameObject($"ModalWindow_{children.name}");

            var canvasRectTransform = canvas.GetComponent<RectTransform>();

            modalWindow.transform.SetParent(canvasRectTransform.transform);
            modalWindow.transform.localPosition = Vector3.zero;

            var rectTransform = modalWindow.AddComponent<RectTransform>();

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, canvasRectTransform.rect.width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, canvasRectTransform.rect.height);

            var button = modalWindow.AddComponent<Button>();

            if (!readOnly)
            {
                button.onClick.AddListener(() => Object.Destroy(modalWindow));
            }

            modalWindow.AddComponent<Image>().color = modalWindowColor;

            children.transform.SetParent(modalWindow.transform);

            children.onDestroyObject.AddListener(() => Object.Destroy(modalWindow));

            return modalWindow;
        }
    }
}