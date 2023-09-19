using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using WordUp.Api;
using WordUp.Api.Contracts.Translate;
using WordUp.Shared;
using WordUp.Shared.StaticShared;
using Zenject;

namespace WordUp.Views.DictionaryEditView.TranslateControl
{
   public class TranslateControlController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField sourceLanguageTextField;
        [SerializeField] private TMP_InputField targetLanguageTextField;
        [SerializeField] private TranslateControlLanguageController languageController;
        
        [Inject] private IExternalSystemHandler _externalSystemHandler;
        
        private bool _autoTranslate;

        private string _previousTextOnControl = string.Empty;
        private TimerInvoker _timer;

        public Language SourceLanguage
        {
            get => languageController.SourceLanguage;
            set => languageController.SourceLanguage = value;
        }
        
        public Language TargetLanguage
        {
            get => languageController.TargetLanguage;
            set => languageController.TargetLanguage = value;
        }

        public bool AutoTranslate
        {
            get => _autoTranslate;
            set
            {
                _autoTranslate = value;
                
                AutoTranslateChanged();
            }
        }

        public string SourceText
        {
            get => sourceLanguageTextField.text;
            set => sourceLanguageTextField.text = value;
        }

        public string TargetText
        {
            get => targetLanguageTextField.text;
            set => targetLanguageTextField.text = value;
        }

        private void Start()
        {
            languageController = GetComponent<TranslateControlLanguageController>();

            languageController.languageTextChanged.AddListener(SwitchText);
            sourceLanguageTextField.onValueChanged.AddListener(SourceLanguageTextFieldValueChanged);

            _timer = new TimerInvoker();
            _timer.BindAction(seconds: 1f, TryUpdateTranslate);
        }

        private void SwitchText()
        {
           string sourceText = sourceLanguageTextField.text;
           string targetText = targetLanguageTextField.text;

           sourceLanguageTextField.text = targetText;
           targetLanguageTextField.text = sourceText;

           SourceLanguageTextFieldValueChanged(targetText);
        }

        private void Update()
        {
            _timer.Update();
        }

        private async void TryUpdateTranslate()
        {
            string currentText = sourceLanguageTextField.text;
            var languageCurrentText = LanguageHelpers.GetLanguageByString(currentText);

            bool needUpdateTextByExternalResource =
                this.AutoTranslate && 
                !string.IsNullOrEmpty(currentText) &&
                !_previousTextOnControl.Equals(currentText) && 
                languageCurrentText == languageController.SourceLanguage;

            if (needUpdateTextByExternalResource)
            {
                if (!_previousTextOnControl.Equals(currentText))
                {
                    _previousTextOnControl = currentText;
                }
                
                string newText = await Translate(currentText);

                if (!currentText.Equals(_previousTextOnControl))
                {
                    TryUpdateTranslate();
                }
                
                targetLanguageTextField.text = newText;
            }
        }

        private async Task<string> Translate(string translatedText)
        {
            var request = new TranslateRequest
            {
                Text = translatedText,
                SourceLanguage = LanguageHelpers.GetLanguageCodeByLanguage(languageController.SourceLanguage),
                TargetLanguage = LanguageHelpers.GetLanguageCodeByLanguage(languageController.TargetLanguage)
            };

            var response = await _externalSystemHandler.Translate(request);

            if (response.IsSuccess)
            {
                return response.TranslatedText;
            }
            
            Debug.LogError($"Error while translate: {response.Error}");
            
            return string.Empty;
        }

        void SourceLanguageTextFieldValueChanged(string newText)
        {
            if (!this.AutoTranslate)
            {
                return;
            }
            
            Language languageCurrentText = LanguageHelpers.GetLanguageByString(newText);
            bool repeatText = languageCurrentText != languageController.SourceLanguage || string.IsNullOrEmpty(newText);

            if (repeatText)
            {
                targetLanguageTextField.text = newText;
            }
        }

        private void AutoTranslateChanged()
        {
            SourceLanguageTextFieldValueChanged(sourceLanguageTextField.text);

            targetLanguageTextField.placeholder.GetComponent<TextMeshProUGUI>().text =
                this.AutoTranslate ? string.Empty : "Enter text...";
            targetLanguageTextField.readOnly = this.AutoTranslate;
        }
    }
}