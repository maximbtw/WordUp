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

        private Language _sourceLanguage;
        private Language _targetLanguage;

        public UnityEvent languageTextChanged;
        
        public Language SourceLanguage
        {
            get => _sourceLanguage;
            set
            {
                _sourceLanguage = value;
                UpdateVisibleLanguage();
            }
        }
        
        public Language TargetLanguage
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
            this.SourceLanguage = Language.English;
            
            sourceLanguageButton.onClick.AddListener(ChangeLanguage);
            targetLanguageButton.onClick.AddListener(ChangeLanguage);
        }

        private void ChangeLanguage()
        {
            if (this.SourceLanguage == Language.English)
            {
                this.SourceLanguage = Language.Russian;
                this.TargetLanguage = Language.English;
            }
            else
            {
                this.SourceLanguage = Language.English;
                this.TargetLanguage = Language.Russian;
            }
            
            languageTextChanged?.Invoke();
        }

        private void UpdateVisibleLanguage()
        {
            if (this.SourceLanguage == Language.English)
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