using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WordUp.Shared.StaticShared;

namespace WordUp.Views.DictionaryEditView.TranslateControl
{
    public class TranslateControlLanguageController : MonoBehaviour
    {
        private const string RuLanguageNameTitle = "Русский";
        private const string EnLanguageNameTitle = "English";
        
        [SerializeField] private Button sourceLanguageButton;
        [SerializeField] private Button targetLanguageButton;

        private LanguageHelpers.Language _sourceLanguage;
        private LanguageHelpers.Language _targetLanguage;

        public UnityEvent languageTextChanged;
        
        public LanguageHelpers.Language SourceLanguage
        {
            get => _sourceLanguage;
            set
            {
                _sourceLanguage = value;
                UpdateVisibleLanguage();
            }
        }
        
        public LanguageHelpers.Language TargetLanguage
        {
            get => _targetLanguage;
            set
            {
                _targetLanguage = value;
                UpdateVisibleLanguage();
            }
        }

        private void Start()
        {
            this.SourceLanguage = LanguageHelpers.Language.English;
            
            sourceLanguageButton.onClick.AddListener(ChangeLanguage);
            targetLanguageButton.onClick.AddListener(ChangeLanguage);
        }

        private void ChangeLanguage()
        {
            if (this.SourceLanguage == LanguageHelpers.Language.English)
            {
                this.SourceLanguage = LanguageHelpers.Language.Russian;
                this.TargetLanguage = LanguageHelpers.Language.English;
            }
            else
            {
                this.SourceLanguage = LanguageHelpers.Language.English;
                this.TargetLanguage = LanguageHelpers.Language.Russian;
            }
            
            languageTextChanged?.Invoke();
        }

        private void UpdateVisibleLanguage()
        {
            if (this.SourceLanguage == LanguageHelpers.Language.English)
            {
                sourceLanguageButton.GetComponentInChildren<TextMeshProUGUI>().text = EnLanguageNameTitle;
                targetLanguageButton.GetComponentInChildren<TextMeshProUGUI>().text = RuLanguageNameTitle;
            }
            else
            {
                sourceLanguageButton.GetComponentInChildren<TextMeshProUGUI>().text = RuLanguageNameTitle;
                targetLanguageButton.GetComponentInChildren<TextMeshProUGUI>().text = EnLanguageNameTitle;
            }
        }
    }
}