using System.Linq;
using UnityEngine;
using WordUp.Service.Word;
using WordUp.Shared.StaticShared;
using WordUp.UI.ConfirmedMessageBox;
using Zenject;

namespace WordUp.Views.ExtensionSettingsView
{
    public class ExtensionSettingsViewController : SceneControllerBase
    {
        [Inject] private IWordService _wordService;
        [Inject] private Canvas _canvas;
        [Inject] private ConfirmedMessageBox _confirmedMessageBox;

        public void ClearAllWords()
        {
            string message = "Are you sure you want to remove all words?";

            UIHelper.ShowConfirmedMessageBox(
                _canvas, 
                _confirmedMessageBox, 
                message,
                onApplyAction: () => _wordService.DeleteAll());
        }

        public void LoadInitWords()
        {
            string message = _wordService.GetModels().Any()
                ? "Do you really want to download all the words? You already have a list of words, duplicates are possible." 
                : "Do you really want to download all the words?";

            UIHelper.ShowConfirmedMessageBox(
                _canvas, 
                _confirmedMessageBox, 
                message,
                onApplyAction: LoadWords);
        }

        private void LoadWords()
        {
            WordsLoader loader = new WordsLoader(_wordService);

            loader.LoadWords();   
        }
    }
}