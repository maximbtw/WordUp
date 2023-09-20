using TMPro;
using UnityEngine;
using WordUp.Service.Contracts.Settings;
using WordUp.Service.Settings;
using WordUp.Shared.StaticShared;
using WordUp.UI.ValidationMessageBox;
using WordUp.Views.SettingsView.Control;
using Zenject;

namespace WordUp.Views.SettingsView
{
    public class SettingsViewController : SceneControllerBase
    {
        [SerializeField] private SettingsViewButtonSound soundButton;
        [SerializeField] private TMP_InputField wordCountInputField;

        [Inject] private ISettingsService _settingsService;
        [Inject] private ValidationMessageBox _validationMessageBox;
        [Inject] private Canvas _canvas;

        private SettingsDto _saveModel;
        private SettingsDto _modificationModel;

        protected override void LateStart()
        {
            _saveModel = _settingsService.GetModel();
            _modificationModel = _saveModel.DeepClone();
            
            UpdateBindings();
        }

        private void UpdateBindings()
        {
            soundButton.Selected = _modificationModel.SoundActive;
            wordCountInputField.text = _modificationModel.CountWordInGroup.ToString();
        }
        
        public void OnInputFieldWordCountChanged()
        {
            string newText = wordCountInputField.text;
            if (string.IsNullOrEmpty(newText))
            {
                newText = "0";
            }
            
            _modificationModel.CountWordInGroup = int.Parse(newText);
        }

        public void OnSaveClick()
        {
            var validator = new SettingsDtoValidator();
            validator.Validate(_modificationModel);

            if (validator.HasIssues)
            {
                UIHelper.ShowModalValidationMessageBox(_canvas, _validationMessageBox, validator.Issues, () => { });

                return;
            }

            _settingsService.Update(_modificationModel);
            UnloadScene((int)SceneNumber.SettingsView);
        }

        public void ChangeSoundSettings()
        {
            _modificationModel.SoundActive = !_modificationModel.SoundActive;
        }
    }
}