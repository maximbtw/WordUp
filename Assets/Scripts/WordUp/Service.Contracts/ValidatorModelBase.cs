using System.Collections.Generic;
using System.Linq;

namespace WordUp.Service.Contracts
{
    public abstract class ValidatorModelBase<TModel> where TModel : IModelDto
    {
        public bool HasIssues => Issues.Any();
        
        public bool HasErrors => Issues.Any(x => x.Type == IssueType.Error);
        
        public bool HasWarnings => Issues.Any(x => x.Type == IssueType.Warning);
        
        public List<Issue> Issues = new();

        public void Validate(TModel model)
        {
            Issues.Clear();
            
            CustomValidate(model, ref  Issues);
        }
        
        protected abstract void CustomValidate(TModel model, ref List<Issue> issues);

        protected void AddError(ref List<Issue> issues, string text)
        {
            issues.Add(new Issue
            {
                Type  = IssueType.Error,
                Text = text
            });
        }
        
        protected void AddWarning(ref List<Issue> issues, string text)
        {
            issues.Add(new Issue
            {
                Type  = IssueType.Warning,
                Text = text
            });
        }

        private void ValidateBase(TModel model, ref List<Issue> issues)
        {
            //TODO: Validate by attribute
        }
    }
    
    public class Issue
    {
        public string Text { get; set; }
            
        public IssueType Type { get; set; }
    }

    public enum IssueType
    {
        Error,
        Warning
    }
}