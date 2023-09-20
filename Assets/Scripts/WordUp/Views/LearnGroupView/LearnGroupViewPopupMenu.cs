using UnityEngine;
using UnityEngine.UI;
using WordUp.UI.PopupMenu;

namespace WordUp.Views.LearnGroupView
{
    public class LearnGroupViewPopupMenu : PopupMenu
    {
        [SerializeField] private Button buttonChangeLearnedMarkWord;
        [SerializeField] private Button buttonChangeHardMarkWord;
        
        public Button ButtonChangeLearnedMarkWord => buttonChangeLearnedMarkWord;

        public Button ButtonChangeHardMarkWord => buttonChangeHardMarkWord;
    }
}