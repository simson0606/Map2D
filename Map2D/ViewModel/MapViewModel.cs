using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using Map2D.Model;

namespace Map2D.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        public int Columns { get; set; }
        public int Rows { get; set; }
        public ObservableCollection<TileObjectViewModel> Tiles { get; set; }
        public int TileSize { get; set; } = 32;
        public MapViewModel(int rows, int columns, Tile defaultTile, Tile defaultObjectTile)
        {
            Rows = rows;
            Columns = columns;
            Tiles = new ObservableCollection<TileObjectViewModel>(Enumerable.Range(0, Rows * Columns).Select(t => new TileObjectViewModel{TileViewModel = new TileViewModel(defaultTile), ObjectViewModel = new TileViewModel(defaultObjectTile) }));
        }

        public void UpdateTile(TileViewModel selectedType, TileViewModel selectedTile, TileViewModel selectedObject)
        {
            if (selectedType != null && (selectedTile != null || selectedObject != null))
            {
                var selectedTileObjectViewModel = Tiles.First(to => selectedTile != null ? to.TileViewModel == selectedTile : to.ObjectViewModel == selectedObject);
                int index = Tiles.IndexOf(selectedTileObjectViewModel);
                Tiles.RemoveAt(index);

                if (selectedTile != null)
                {
                    selectedTileObjectViewModel.TileViewModel = new TileViewModel(selectedType.ToModel());
                }
                if (selectedObject != null)
                {
                    selectedTileObjectViewModel.ObjectViewModel = new TileViewModel(selectedType.ToModel());
                }
                Tiles.Insert(index, selectedTileObjectViewModel);
            }
        }
    }
}