namespace Dolby.UAP.Base
{
    public class BaseViewModel : BindableBase
    {
        #region Common Properties
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
                if (value)
                {
                    OnLoading();
                }
                else
                {
                    OnLoaded();
                }
            }
        }
        #endregion

        #region Common Methods
        protected virtual void OnLoading()
        {

        }

        protected virtual void OnLoaded()
        {

        }

        public virtual void LoadState(LoadStateEventArgs e)
        {

        }

        public virtual void SaveState(SaveStateEventArgs e)
        {

        }
        #endregion
    }
}