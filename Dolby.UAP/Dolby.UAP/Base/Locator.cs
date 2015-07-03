namespace Dolby.UAP.Base
{
    using Dolby.UAP.Services;
    using Dolby.UAP.Services.Interfaces;
    using Dolby.UAP.ViewModels;
    using Microsoft.Practices.Unity;

    public class Locator
    {
        private IUnityContainer container;

        public Locator()
        {
            container = new UnityContainer();

            //ViewModels
            container.RegisterType<MainViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<AboutViewModel>();
            container.RegisterType<SnippetsViewModel>();
            container.RegisterType<PlaybackViewModel>(new ContainerControlledLifetimeManager());

            //Services
            container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IConfigService, ConfigService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDataProvider, DataProvider>();
        }

        public MainViewModel MainViewModel
        {
            get { return container.Resolve<MainViewModel>(); }
        }

        public AboutViewModel AboutViewModel
        {
            get { return container.Resolve<AboutViewModel>(); }
        }

        public SnippetsViewModel SnippetsViewModel
        {
            get { return container.Resolve<SnippetsViewModel>(); }
        }

        public PlaybackViewModel PlaybackViewModel
        {
            get { return container.Resolve<PlaybackViewModel>(); }
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}