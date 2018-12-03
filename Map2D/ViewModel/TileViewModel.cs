using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using Map2D.Model;

namespace Map2D.ViewModel
{
    public class TileViewModel : ViewModelBase
    {
        private Tile _tile;

        public string Name
        {
            get => _tile.Name;
            set
            {
                _tile.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string Character
        {
            get => _tile.Character;
            set
            {
                _tile.Character = value;
                RaisePropertyChanged(() => Character);
            }
        }

        public BitmapImage Image
        {
            get => _tile.Image;
            set
            {
                _tile.Image = value;
                RaisePropertyChanged(() => Image);
            }
        }

        public TileViewModel(Tile tile)
        {
            _tile = tile;
        }

        public Tile ToModel()
        {
            return _tile;
        }
    }
}