using UnityEngine;
using UnityEngine.UI;
using WordUp.UI.PopupMenu;

namespace WordUp.Views.DictionaryListView
{
    public class DictionaryListViewPopupMenu : PopupMenu
    {
        [SerializeField] private Button buttonDeleteWord;
        [SerializeField] private Button buttonEditWord;
        [SerializeField] private Button buttonChangeLearnedMarkWord;
        [SerializeField] private Button buttonChangeHardMarkWord;

        public Button ButtonDeleteWord => buttonDeleteWord;

        public Button ButtonEditWord => buttonEditWord;

        public Button ButtonChangeLearnedMarkWord => buttonChangeLearnedMarkWord;

        public Button ButtonChangeHardMarkWord => buttonChangeHardMarkWord;
    }
}