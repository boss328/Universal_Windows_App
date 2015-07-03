namespace Dolby.UAP.Models
{
    using Dolby.UAP.Base;

    public class Movie : BindableBase
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailFileAddress { get; set; }
        public string DolbyVideoSource { get; set; }
        public string OtherVideoSource { get; set; }
        public bool HasBothVideoSources
        {
            get
            {
                return !string.IsNullOrEmpty(DolbyVideoSource) && !string.IsNullOrEmpty(OtherVideoSource);
            }
        }
        private string _videoSource;
        public string VideoSource
        {
            get
            {
                return _videoSource;
            }
            set
            {
                SetProperty(ref _videoSource, value);
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }

        public void SwitchVideoSource(bool dolbyFormatEnabled)
        {
            if (HasBothVideoSources)
            {
                VideoSource = dolbyFormatEnabled ? DolbyVideoSource : OtherVideoSource;
            }
            else
            {
                VideoSource = !string.IsNullOrEmpty(DolbyVideoSource) ? DolbyVideoSource : OtherVideoSource;
            }
        }
    }
}