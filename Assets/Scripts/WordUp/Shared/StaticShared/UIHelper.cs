using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WordUp.Service.Contracts;
using WordUp.UI;
using WordUp.UI.ConfirmedMessageBox;
using WordUp.UI.ValidationMessageBox;
using Zenject;
using Object = UnityEngine.Object;

namespace WordUp.Shared.StaticShared
{
    public static class UIHelper
    {
        public static TObject CreateInstantiate<TObject>(TObject tObject, Transform parentTransform)
            where TObject : MonoBehaviour
        {
            var diContainer = new DiContainer();
            TObject copyObject = diContainer
                .InstantiatePrefab(tObject, parentTransform).GetComponent<TObject>();

            return copyObject;
        }

        public static void ShowModalValidationMessageBox(
            Canvas canvas,
            ValidationMessageBox validationMessageBoxPrefab,
            List<Issue> issues, 
            UnityAction onApplyAction)
        {
            ValidationMessageBox validationMessageBox = CreateInstantiate(validationMessageBoxPrefab, canvas.transform);

            ModalWindow.Construct(
                canvas,
                validationMessageBox,
                readOnly: true,
                new Color(0, 0, 0, 0.5f));

            validationMessageBox.Construct(issues);
            validationMessageBox.onApplyAction.AddListener(onApplyAction);
        }

        public static void ShowConfirmedMessageBox(
            Canvas canvas, 
            ConfirmedMessageBox confirmedMessageBoxPrefab, 
            string text,
            UnityAction onApplyAction)
        {
            ConfirmedMessageBox messageBox = CreateInstantiate(confirmedMessageBoxPrefab, canvas.transform);

            ModalWindow.Construct(canvas, messageBox, readOnly: true, new Color(0, 0, 0, 0.5f));

            messageBox.Construct(text);
            messageBox.onApplyAction.AddListener(onApplyAction);
        }

        public static void DestroyAllChildren(Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}