using GalaSoft.MvvmLight;

namespace Map2D.ViewModel
{
    public class TileObjectViewModel : ViewModelBase
    {
        private TileViewModel _tileViewModel;
        private TileViewModel _objectViewModel;

        public TileViewModel TileViewModel
        {
            get => _tileViewModel;
            set
            {
                if (_tileViewModel != value)
                {
                    _tileViewModel = value;
                    RaisePropertyChanged(() => TileViewModel);
                }
            }
        }

        public TileViewModel ObjectViewModel
        {
            get => _objectViewModel;
            set
            {
                if (_objectViewModel != value)
                {
                    _objectViewModel = value;
                    RaisePropertyChanged(() => ObjectViewModel);
                }
            }
        }
    }
}