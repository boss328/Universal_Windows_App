namespace Dolby.UAP.Views
{
    using Dolby.UAP.Base;
    using Dolby.UAP.ViewModels;
    using System.Diagnostics;
    using Windows.UI.Xaml.Controls;

    public sealed partial class PlaybackPage : BasePage
    {
        private PlaybackViewModel vm;
        private bool _carouselAnimationIsActive;
        public PlaybackPage()
        {
            this.InitializeComponent();
            vm = this.DataContext as PlaybackViewModel;
            vm.MediaElement = MediaPlayer;
            CarouselDisplayingStoryboard.Completed += CarouselDisplayingStoryboard_Completed;
            KeepCarouselStoryboard.Completed += KeepCarouselStoryboard_Completed;

            _carouselAnimationIsActive = true;
            CarouselDisplayingStoryboard.Begin();
        }

        private void KeepCarouselStoryboard_Completed(object sender, object e)
        {
            _carouselAnimationIsActive = false;
        }

        private void PlaybackRootGrid_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (!_carouselAnimationIsActive)
            {
                _carouselAnimationIsActive = true;
                CarouselDisplayingStoryboard.Begin();
            }
        }

        private void CarouselDisplayingStoryboard_Completed(object sender, object e)
        {
            _carouselAnimationIsActive = false;
        }

        private void VideosGrid_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            KeepCarouselStoryboard.Begin();
        }

        private void VideosGrid_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {            
            if ((sender as Grid).Opacity > 0.0)
            {
                KeepOverCarouselStoryboard.Begin();
                KeepCarouselStoryboard.Stop();
                CarouselDisplayingStoryboard.Stop();
                _carouselAnimationIsActive = false;
            }
            else
            {
                if (!_carouselAnimationIsActive)
                {
                    _carouselAnimationIsActive = true;
                    CarouselDisplayingStoryboard.Begin();
                }
            }
        }

        private void VideosGrid_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _carouselAnimationIsActive = true;
            KeepCarouselStoryboard.Begin();
        }

        private void MediaPlayer_MediaEnded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!_carouselAnimationIsActive)
            {
                _carouselAnimationIsActive = true;
                CarouselDisplayingStoryboard.Begin();
            }
            if (vm != null)
            {
                vm.VideoEnded();
            }
        }

        private void MediaPlayer_MediaOpened(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (vm != null)
            {
                vm.VideoStarted(MediaPlayer);
            }            
        }
    }
}