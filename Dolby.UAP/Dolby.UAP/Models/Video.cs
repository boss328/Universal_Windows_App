namespace Dolby.UAP.Models
{
    using Dolby.UAP.Base;

    public class Video : BindableBase
    {
        private int _index;
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                SetProperty(ref _index, value);
            }
        }            

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                SetProperty(ref _description, value);
            }
        }

        private string _thumbnailFileAddress;
        public string ThumbnailFileAddress
        {
            get
            {
                return _thumbnailFileAddress;
            }
            set
            {
                SetProperty(ref _thumbnailFileAddress, value);
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
    }
}