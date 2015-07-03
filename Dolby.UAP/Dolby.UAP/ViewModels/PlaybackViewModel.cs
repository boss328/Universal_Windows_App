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

            _movieChangedCommand = new DelegateCommand<int>(MovieChangedCommandExecute);
            _movieStartedCommand = new DelegateCommand<MediaElement>(MovieStartedCommandExecute);
            _movieEndedCommand = new DelegateCommand(MovieEndedCommandExecute);
            _updateWindowSizeCommand = new DelegateCommand<SizeChangedEventArgs>(UpdateWindowSizeCommandExecute);
            DolbyFormatEnabled = true;
            ListViewOpacity = 0;
        }

        public override void LoadState(LoadStateEventArgs e)
        {
            base.LoadState(e);
            GetMoviesInfo();
        }

        private async void GetMoviesInfo()
        {
            List<Movie> moviesList = await _dataProvider.GetDolbyMoviesInfo();
            if (moviesList.Count > 0)
            {
                MoviesList = new ObservableCollection<Movie>(moviesList);
                SetAudioFormat();
                SelectedMovie = MoviesList[0];
                MarkSelectedMovie(0);
            }
        }

        private void MarkSelectedMovie(int index)
        {
            for (int i = 0; i < MoviesList.Count; i++)
            {
                MoviesList[i].IsSelected = false;
            }

            MoviesList[index].IsSelected = true;

            if (!SelectedMovie.HasBothVideoSources)
            {
                DolbyFormatEnabled = !string.IsNullOrEmpty(SelectedMovie.DolbyVideoSource);
                SetAudioFormat();
            }
        }

        private void SetAudioFormat()
        {
            if (MoviesList != null)
            {
                _savedPosition = Position;
                for (int i = 0; i < MoviesList.Count; i++)
                {
                    MoviesList[i].SwitchVideoSource(DolbyFormatEnabled);
                }
            }
        }

        private void UpdateProgressPercentage()
        {
            ProgressPercentage = (int)((Position.TotalMilliseconds / Duration.TotalMilliseconds) * 100);
        }

        private void SetMediaElementSource()
        {
            Position = TimeSpan.Zero;
            if (MediaElement != null)
                MediaElement.Source = new Uri(SelectedMovie.VideoSource);
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
                _listViewOpacity = value;
            }
        }

        private bool _dolbyFormatEnabled;
        public bool DolbyFormatEnabled
        {
            get
            {
                return _dolbyFormatEnabled;
            }
            set
            {
                SetProperty(ref _dolbyFormatEnabled, value);
                SetAudioFormat();
                SetMediaElementSource();
            }
        }

        private ObservableCollection<Movie> _moviesList;
        public ObservableCollection<Movie> MoviesList
        {
            get
            {
                return _moviesList;
            }
            set
            {
                SetProperty(ref _moviesList, value);
            }
        }

        private Movie _selectedMovie;
        public Movie SelectedMovie
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

        private double _windowWidth;
        public double WindowWidth
        {
            get
            {
                return _windowWidth;
            }
            set
            {
                SetProperty(ref _windowWidth, value);
            }
        }
        #endregion

        #region Commands
        private DelegateCommand<int> _movieChangedCommand;
        public DelegateCommand<int> MovieChangedCommand
        {
            get { return _movieChangedCommand; }
        }

        private void MovieChangedCommandExecute(int index)
        {
            if (ListViewOpacity > 0.8 && !MoviesList[index].IsSelected)
            {
                SelectedMovie = MoviesList[index];
                MarkSelectedMovie(index);
            }
        }

        private DelegateCommand<MediaElement> _movieStartedCommand;
        public DelegateCommand<MediaElement> MovieStartedCommand
        {
            get { return _movieStartedCommand; }
        }

        private void MovieStartedCommandExecute(MediaElement mediaplayer)
        {
            Duration = mediaplayer.NaturalDuration.TimeSpan;
            if (_savedPosition != null)
            {
                Position = (TimeSpan)_savedPosition;
                _savedPosition = null;
            }
        }

        private DelegateCommand _movieEndedCommand;
        public DelegateCommand MovieEndedCommand
        {
            get { return _movieEndedCommand; }
        }

        private void MovieEndedCommandExecute()
        {
            int index = (SelectedMovie.Index + 1) % MoviesList.Count;
            SelectedMovie = MoviesList[index];
            MarkSelectedMovie(index);
        }


        private DelegateCommand<SizeChangedEventArgs> _updateWindowSizeCommand;
        public DelegateCommand<SizeChangedEventArgs> UpdateWindowSizeCommand
        {
            get { return _updateWindowSizeCommand; }
        }

        private void UpdateWindowSizeCommandExecute(SizeChangedEventArgs args)
        {
            if (args != null && !args.NewSize.IsEmpty)
            {
                WindowWidth = args.NewSize.Width;
            }
        }
        #endregion
    }
}