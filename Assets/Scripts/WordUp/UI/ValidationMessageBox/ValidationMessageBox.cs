using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WordUp.Service.Contracts;
using WordUp.Shared.StaticShared;
using Zenject;

namespace WordUp.UI.ValidationMessageBox
{
    public class ValidationMessageBox : DestroyedComponent
    {
        [SerializeField] private ValidationMessageBoxItem prefabValidationMessageBoxItem;
        [SerializeField] private Color errorValidationItemColor;
        [SerializeField] private Color warningValidationItemColor;

        public void Construct(
            ICollection<Issue> validationIssues,
            bool showError = true,
            bool showWarning = true)
        {
            if (CollectionHelpers.IsNullOrEmpty(validationIssues))
            {
                throw new Exception("Validation Issues must me provided");
            }

            if (!showError && !showWarning)
            {
                throw new Exception("Any issue must be showed");
            }

            foreach (Issue issue in validationIssues.OrderBy(x=>x.Type))
            {
                prefabValidationMessageBoxItem.GetComponentInChildren<Image>().IsActive();
                
                if (issue.Type == IssueType.Error && showError)
                {
                    ValidationMessageBoxItem item =
                        UIHelper.CreateInstantiate(prefabValidationMessageBoxItem, transform);
                    
                    item.BindProperties(errorValidationItemColor, issue.Text);
                }
                else if (issue.Type == IssueType.Warning && showWarning)
                {
                    ValidationMessageBoxItem item =
                        UIHelper.CreateInstantiate(prefabValidationMessageBoxItem, transform);
                    
                    item.BindProperties(warningValidationItemColor, issue.Text);
                }
            }
        }

        [Inject]
        private void Init()
        {
            ValidationMessageBoxItem[] items = GetComponentsInChildren<ValidationMessageBoxItem>();

            foreach (ValidationMessageBoxItem item in items)
            {
                Destroy(item.gameObject);
            }
        }
    }
}