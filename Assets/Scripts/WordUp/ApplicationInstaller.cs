using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using WordUp.Api;
using WordUp.Api.DeepTranslate.Client;
using WordUp.Api.DeepTranslate.Connector;
using Zenject;

namespace WordUp
{
    public class ApplicationInstaller : MonoInstaller
    {
        private Configuration _configuration;

        public override void InstallBindings()
        {
            Container.Bind<IDeepTranslateRestClient>().To<DeepTranslateRestClient>().FromNew().AsSingle().NonLazy();
            Container.Bind<IDeepTranslateConnector>().To<DeepTranslateConnector>().FromNew().AsSingle().NonLazy();
            Container.Bind<IExternalSystemHandler>().To<ExternalSystemHandler>().FromNew().AsSingle().NonLazy();
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