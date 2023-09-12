using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using WordUp.Api;
using WordUp.Api.Contracts.Translate;
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
        
        private bool _translateLock = true;

        public LanguageHelpers.Language SourceLanguage
        {
            get => languageController.SourceLanguage;
            set => languageController.SourceLanguage = value;
        }
        
        public LanguageHelpers.Language TargetLanguage
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

            languageController.languageTextChanged.AddListener(() => SourceLanguageTextFieldValueChanged(sourceLanguageTextField.text));
            sourceLanguageTextField.onValueChanged.AddListener(SourceLanguageTextFieldValueChanged);
        }

        private async void SourceLanguageTextFieldValueChanged(string newText)
        {
            _translateLock = false;
            if (!this.AutoTranslate)
            {
                _translateLock = true;
                
                return;
            }
            
            if (string.IsNullOrEmpty(newText))
            {
                SetTranslatedText(string.Empty);
                _translateLock = true;
                
                return;
            }
            
            var languageNewText = LanguageHelpers.GetLanguageByString(newText);

            if (languageNewText != languageController.SourceLanguage)
            {
                SetTranslatedText(newText);
                _translateLock = true;
                
                return;
            }

            SetTranslatedText(await Translate(newText));

            void SetTranslatedText(string translatedText)
            {
                if (!_translateLock)
                {
                    targetLanguageTextField.text = translatedText;
                }
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

        private void AutoTranslateChanged()
        {
            SourceLanguageTextFieldValueChanged(sourceLanguageTextField.text);

            targetLanguageTextField.placeholder.GetComponent<TextMeshProUGUI>().text =
                this.AutoTranslate ? string.Empty : "Enter text...";
            targetLanguageTextField.readOnly = this.AutoTranslate;
        }
    }
}