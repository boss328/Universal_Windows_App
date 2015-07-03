namespace Dolby.UAP.ViewModels
{
    using Dolby.UAP.Base;
    using Dolby.UAP.Models;
    using Dolby.UAP.Services.Interfaces;
    using Dolby.UAP.Views;

    public class MainViewModel : BaseViewModel
    {
        #region Atributes
        private INavigationService _navigationService;
        #endregion

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.Navigate<PlaybackPage>();

            _aboutNavigationCommand = new DelegateCommand(AboutNavigationCommandExecute);
            _snippetsNavigationCommand = new DelegateCommand(SnippetsNavigationCommandExecute);
            _playbackNavigationCommand = new DelegateCommand(PlaybackNavigationCommandExecute);

            IsLeftPanelExpanded = false;
            PageDisplayed = PageType.PlaybackPage;
        }

        #region Properties
        private bool _isLeftPanelExpanded;
        public bool IsLeftPanelExpanded
        {
            get
            {
                return _isLeftPanelExpanded;
            }
            set
            {
                SetProperty(ref _isLeftPanelExpanded, value);
            }
        }

        private PageType _pageDisplayed;
        public PageType PageDisplayed
        {
            get
            {
                return _pageDisplayed;
            }
            set
            {
                SetProperty(ref _pageDisplayed, value);
            }
        }
        #endregion

        #region Commands
        private DelegateCommand _aboutNavigationCommand;
        public DelegateCommand AboutNavigationCommand
        {
            get { return _aboutNavigationCommand; }
        }

        private void AboutNavigationCommandExecute()
        {
            if (PageDisplayed != PageType.AboutPage)
            {
                PageDisplayed = PageType.AboutPage;
                _navigationService.Navigate<AboutPage>();
            }
            IsLeftPanelExpanded = false;
        }

        private DelegateCommand _snippetsNavigationCommand;
        public DelegateCommand SnippetsNavigationCommand
        {
            get { return _snippetsNavigationCommand; }
        }

        private void SnippetsNavigationCommandExecute()
        {
            if (PageDisplayed != PageType.SnippetsPage)
            {
                PageDisplayed = PageType.SnippetsPage;
                _navigationService.Navigate<SnippetsPage>();
            }
            IsLeftPanelExpanded = false;
        }

        private DelegateCommand _playbackNavigationCommand;
        public DelegateCommand PlaybackNavigationCommand
        {
            get { return _playbackNavigationCommand; }
        }

        private void PlaybackNavigationCommandExecute()
        {
            if (PageDisplayed != PageType.PlaybackPage)
            {
                PageDisplayed = PageType.PlaybackPage;
                _navigationService.Navigate<PlaybackPage>();
            }
            IsLeftPanelExpanded = false;
        }
        #endregion
    }
}