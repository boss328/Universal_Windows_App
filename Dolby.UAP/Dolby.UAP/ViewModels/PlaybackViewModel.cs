namespace Dolby.UAP.ViewModels
{
    using Dolby.UAP.Base;
    using Dolby.UAP.Models;
    using Dolby.UAP.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Windows.Media.Streaming.Adaptive;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class PlaybackViewModel : BaseViewModel
    {
        #region Atributes
        private IDataProvider _dataProvider;
        private TimeSpan? _savedPosition;
        #endregion

        public PlaybackViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _savedPosition = null;

            _videoChangedCommand = new DelegateCommand<string>(VideoChangedCommandExecute);            
            ListViewOpacity = 1;
        }

        public override void LoadState(LoadStateEventArgs e)
        {
            base.LoadState(e);
            GetVideosInfo();
        }

        private async void GetVideosInfo()
        {
            List<Video> providedVideoList = await _dataProvider.GetDolbyMoviesInfo();
            if (providedVideoList.Count > 0)
            {
                VideosList = new ObservableCollection<Video>(providedVideoList);                               
                SelectedVideo = VideosList[0];
                MarkSelectedVideo(0);
            }
        }

        private void MarkSelectedVideo(int index)
        {
            for (int i = 0; i < VideosList.Count; i++)
            {
                VideosList[i].IsSelected = false;
            }

            VideosList[index].IsSelected = true;            
        }
       
        private void UpdateProgressPercentage()
        {
            ProgressPercentage = (int)((Position.TotalMilliseconds / Duration.TotalMilliseconds) * 100);
        }

        private void SetMediaElementSource()
        {
            Position = TimeSpan.Zero;
            if (MediaElement != null && SelectedVideo != null)
            {
                MediaElement.Source = new Uri(SelectedVideo.VideoSource);
            }                
        }

        #region Properties
        private MediaElement _mediaElement;
        public MediaElement MediaElement
        {
            get
            {
                return _mediaElement;
            }
            set
            {
                _mediaElement = value;
            }
        }

        private double _listViewOpacity;
        public double ListViewOpacity
        {
            get
            {
                return _listViewOpacity;
            }
            set
            {                
                SetProperty(ref _listViewOpacity, value);
            }
        }
        
        private ObservableCollection<Video> _videosList;
        public ObservableCollection<Video> VideosList
        {
            get
            {
                return _videosList;
            }
            set
            {
                SetProperty(ref _videosList, value);
            }
        }

        private Video _selectedMovie;
        public Video SelectedVideo
        {
            get
            {
                return _selectedMovie;
            }
            set
            {
                SetProperty(ref _selectedMovie, value);
                SetMediaElementSource();
            }
        }

        private TimeSpan _position;
        public TimeSpan Position
        {
            get
            {
                return _position;
            }
            set
            {
                SetProperty(ref _position, value);
                UpdateProgressPercentage();
            }
        }

        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                SetProperty(ref _duration, value);
            }
        }

        private int _progressPercentage;
        public int ProgressPercentage
        {
            get
            {
                return _progressPercentage;
            }
            set
            {
                SetProperty(ref _progressPercentage, value);
            }
        }        
        #endregion

        #region Commands
        private DelegateCommand<string> _videoChangedCommand;
        public DelegateCommand<string> VideoChangedCommand
        {
            get { return _videoChangedCommand; }
        }

        private void VideoChangedCommandExecute(string strIndex)
        {            
            int index = Int32.TryParse(strIndex, out index) ? index : 0;
            if (ListViewOpacity > 0.8 && !VideosList[index].IsSelected)
            {
                SelectedVideo = VideosList[index];
                MarkSelectedVideo(index);
            }
        }       

        public void VideoStarted(MediaElement mediaplayer)
        {
            Duration = mediaplayer.NaturalDuration.TimeSpan;
            if (_savedPosition != null)
            {
                Position = (TimeSpan)_savedPosition;
                _savedPosition = null;
            }
        }        

        public void VideoEnded()
        {
            int index = (SelectedVideo.Index + 1) % VideosList.Count;
            SelectedVideo = VideosList[index];
            MarkSelectedVideo(index);
        }
        #endregion
    }
}