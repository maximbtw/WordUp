using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using WordUp.Api;
using WordUp.Api.DeepTranslate.Client;
using WordUp.Api.DeepTranslate.Connector;
using WordUp.Service.Settings;
using WordUp.Service.Word;
using Zenject;

namespace WordUp.Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IDeepTranslateRestClient>().To<DeepTranslateRestClient>().FromNew().AsSingle().NonLazy();
            Container.Bind<IDeepTranslateConnector>().To<DeepTranslateConnector>().FromNew().AsSingle().NonLazy();
            Container.Bind<IExternalSystemHandler>().To<ExternalSystemHandler>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<IWordService>().To<WordService>().FromNew().AsSingle().NonLazy();
            Container.Bind<ISettingsService>().To<SettingsService>().FromNew().AsSingle().NonLazy();
        }

        public override async void Start()
        {
            if (Utilities.CheckForInternetConnection())
            {
                await InitializeRemoteConfigAsync();
            }
            
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
        }

        private async Task InitializeRemoteConfigAsync()
        {
            await UnityServices.InitializeAsync();

           // if (!AuthenticationService.Instance.IsSignedIn)
            //{
                //await AuthenticationService.Instance.SignInAnonymouslyAsync();
           // }
        }
    }

    public struct appAttributes
    {
    }

    public struct userAttributes
    {
    }
}