using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WordUp.Service.Contracts;
using WordUp.Shared.StaticShared;
using Zenject;

namespace WordUp.UI.ValidationMessageBox
{
    public class ValidationMessageBox : DestroyedComponent
    {
        [SerializeField] private Transform content;
        [SerializeField] private Button buttonApply;
        [Space]
        [SerializeField] private ValidationMessageBoxItem prefabValidationMessageBoxItem;
        [SerializeField] private Color errorValidationItemColor;
        [SerializeField] private Color warningValidationItemColor;

        private UnityAction _onApplyAction;

        public void Construct(
            ICollection<Issue> validationIssues, 
            UnityAction onApplyAction, 
            bool showError = true, 
            bool showWarning = true)
        {
            _onApplyAction = onApplyAction;
            
            if (CollectionHelpers.IsNullOrEmpty(validationIssues))
            {
                throw new Exception("Validation Issues must me provided");
            }

            if (!showError && !showWarning)
            {
                throw new Exception("Any issue must be showed");
            }

            CreateItems(validationIssues, showError, showWarning);

            bool showApplyButton = validationIssues.All(x => x.Type == IssueType.Warning) && showWarning;

            buttonApply.gameObject.SetActive(showApplyButton);
        }

        public void OnApplyClick()
        {
            _onApplyAction();
            
            Destroy();
        }

        private void CreateItems(ICollection<Issue> validationIssues, bool showError = true, bool showWarning = true)
        {
            foreach (Issue issue in validationIssues.OrderBy(x=>x.Type))
            {
                prefabValidationMessageBoxItem.GetComponentInChildren<Image>().IsActive();
                
                if (issue.Type == IssueType.Error && showError)
                {
                    ValidationMessageBoxItem item =
                        UIHelper.CreateInstantiate(prefabValidationMessageBoxItem, content);
                    
                    item.BindProperties(errorValidationItemColor, issue.Text);
                }
                else if (issue.Type == IssueType.Warning && showWarning)
                {
                    ValidationMessageBoxItem item =
                        UIHelper.CreateInstantiate(prefabValidationMessageBoxItem, content);
                    
                    item.BindProperties(warningValidationItemColor, issue.Text);
                }
            }
        }

        [Inject]
        private void Init()
        {
            ValidationMessageBoxItem[] items = content.GetComponentsInChildren<ValidationMessageBoxItem>();

            foreach (ValidationMessageBoxItem item in items)
            {
                Destroy(item.gameObject);
            }
        }
    }
}