namespace Dolby.UAP.Services.Interfaces
{
    using System;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public interface INavigationService
    {
        void UpdateNavigationFrame(Frame newFrame);
        void GoBack();
        void GoForward();
        bool Navigate(Type source, object parameter = null);
        bool Navigate<T>();
        bool Navigate<T>(object parameter = null);
        event NavigatingCancelEventHandler Navigating;
        void RemoveLastHistory();
        void ResetHistory();
    }
}