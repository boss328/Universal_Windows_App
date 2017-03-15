namespace Dolby.UAP.Views
{
    using Dolby.UAP.Base;
    using Dolby.UAP.Services.Interfaces;

    public sealed partial class MainPage : BasePage
    {
        public MainPage()
        {
            this.InitializeComponent();
            var locator = App.Current.Resources["Locator"] as Locator;
            INavigationService navigator = locator.Resolve<INavigationService>();
            navigator.UpdateNavigationFrame(ScenarioFrame);
            navigator.Navigate<PlaybackPage>();
        }
    }
}