using UnityEngine;
using WordUp.UI.ValidationMessageBox;
using Zenject;

namespace WordUp.Installers
{
    public class LearnTestMenuInstaller : MonoInstaller
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private ValidationMessageBox validationMessageBox;
            
        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(canvas).AsSingle().NonLazy();
            Container.Bind<ValidationMessageBox>().FromInstance(validationMessageBox).AsSingle();
        }
    }
}