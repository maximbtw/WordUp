using System.Collections.Generic;

namespace WordUp.Service.Contracts.Settings
{
    public class SettingsDtoValidator : ValidatorModelBase<SettingsDto>
    {
        protected override void CustomValidate(SettingsDto model, ref List<Issue> issues)
        {
            if (model.CountWordInGroup <= 0)
            {
                AddError(ref issues, "The number of words in the group must be greater than zero");
            }
        }
    }
}