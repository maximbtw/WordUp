using UnityEngine;
using WordUp.UI.ConfirmedMessageBox;
using Zenject;

namespace WordUp.Installers
{
    public class ExtensionSettingsInstaller : MonoInstaller
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private ConfirmedMessageBox messageBox;
        
        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(canvas).AsSingle().NonLazy();
            Container.Bind<ConfirmedMessageBox>().FromInstance(messageBox).AsSingle();
        }
    }
}