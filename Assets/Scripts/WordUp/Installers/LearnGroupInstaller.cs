using UnityEngine;
using UnityEngine.Serialization;
using WordUp.UI.ValidationMessageBox;
using WordUp.Views.LearnGroupView;
using Zenject;

namespace WordUp.Installers
{
    public class LearnGroupInstaller : MonoInstaller
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private ValidationMessageBox validationMessageBox;
        [SerializeField] private LearnGroupViewPopupMenu popupMenu;
            
        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(canvas).AsSingle().NonLazy();
            Container.Bind<ValidationMessageBox>().FromInstance(validationMessageBox).AsSingle();
            Container.Bind<LearnGroupViewPopupMenu>().FromInstance(popupMenu).AsSingle();
        }
    }
}