namespace Dolby.UAP.Services
{
    using Dolby.UAP.Services.Interfaces;
    using System;
    using System.Linq;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public class NavigationService : INavigationService
    {
        private Frame frame;

        public event NavigatingCancelEventHandler Navigating;

        public NavigationService()
        {
            frame = App.RootFrame;
            frame.Navigating += NavigationService_Navigating;
        }

        public void UpdateNavigationFrame(Frame newFrame)
        {
            frame.Navigating -= NavigationService_Navigating;
            frame = newFrame;
            frame.Navigating += NavigationService_Navigating;
        }

        private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            var eventHandler = this.Navigating;
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }

        public void GoBack()
        {
            if (frame.CanGoBack)
                frame.GoBack();
        }

        public void GoForward()
        {
            if (frame.CanGoForward)
                frame.GoForward();
        }
        public bool Navigate<T>()
        {
            return Navigate<T>(null);
        }

        public bool Navigate<T>(object parameter = null)
        {
            var type = typeof(T);

            return Navigate(type, parameter);
        }

        public bool Navigate(Type source, object parameter = null)
        {
            return frame.Navigate(source, parameter);
        }

        public void ResetHistory()
        {
            frame.BackStack.Clear();
        }

        public void RemoveLastHistory()
        {
            var last = frame.BackStack.LastOrDefault();
            if (last != null)
            {
                frame.BackStack.Remove(last);
            }
        }

    }
}