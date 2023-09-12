using UnityEngine;
using Zenject;
using DictionaryListViewPopupMenu = WordUp.Views.DictionaryListView.DictionaryListViewPopupMenu;

namespace WordUp.Installers
{
    public class DictionaryListInstaller : MonoInstaller
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private DictionaryListViewPopupMenu dictionaryItemPopupMenu;
            
        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(canvas).AsSingle().NonLazy();
            Container.Bind<DictionaryListViewPopupMenu>().FromInstance(dictionaryItemPopupMenu).AsSingle();
        }
    }
}