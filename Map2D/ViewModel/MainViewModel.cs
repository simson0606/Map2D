using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Map2D.Model;
using Microsoft.Win32;

namespace Map2D.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool _tileMode = true;
        private TileLoader _tileLoader = new TileLoader();
        private TileViewModel _selectedTileType;
        private readonly ObservableCollection<TileViewModel> _tileTypes;
        private readonly ObservableCollection<TileViewModel> _objectTypes;


        public TileViewModel SelectedTileType
        {
            get => _selectedTileType;
            set
            {
                _selectedTileType = value;
                RaisePropertyChanged(() => SelectedTileType);
            }
        }

        private TileObjectViewModel _selectedTile;

        public TileObjectViewModel SelectedTile
        {
            get => _selectedTile;
            set
            {
                if (_selectedTile != value)
                {
                    _selectedTile = value;


                    Map.UpdateTile(_selectedTileType, _tileMode?_selectedTile?.TileViewModel:null, _tileMode?null:_selectedTile?.ObjectViewModel);
                    RaisePropertyChanged(() => SelectedTileType);
                }
            }
        }

        public ObservableCollection<TileViewModel> TileTypes => _tileMode ? _tileTypes : _objectTypes;

        public MapViewModel Map { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand CreateCommand { get; set; }

        public ICommand ObjectsCommand { get; set; }
        public ICommand MapCommand { get; set; }

        public MainViewModel(string path)
        {
            var tileTypeLoader = new TileTypeLoader();
            _objectTypes = new ObservableCollection<TileViewModel>(tileTypeLoader.LoadObjects().Select(t => new TileViewModel(t)));
            _tileTypes = new ObservableCollection<TileViewModel>(tileTypeLoader.LoadTiles().Select(t => new TileViewModel(t)));

            EditMap();
            InitializeMap(16, 16);
            SaveCommand = new RelayCommand(Save);
            LoadCommand = new RelayCommand(Load);
            CreateCommand = new RelayCommand<Window>(Create);
            MapCommand = new RelayCommand(EditMap);
            ObjectsCommand = new RelayCommand(EditObjects);

            LoadFile(path);   
        }

        private void EditObjects()
        {
            RaisePropertyChanged(() => TileTypes);
            _tileMode = false;
        }

        private void EditMap()
        {
            var tileTypeLoader = new TileTypeLoader();
            RaisePropertyChanged(() => TileTypes);
            _tileMode = true;
        }

        public void InitializeMap(int rows, int columns)
        {
            Map = new MapViewModel(rows, columns, _tileTypes[0].ToModel(), _objectTypes[0].ToModel());
            RaisePropertyChanged(() => Map);
        }

        private void Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Map2D files (*.m2d)|*.m2d|All files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
               _tileLoader.Save(Map, saveFileDialog.FileName);
            }
        }

        private void Load()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Map2D files (*.m2d)|*.m2d|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                LoadFile(openFileDialog.FileName);
            }
        }


        private void LoadFile(string path = null)
        {
            if (path != null)
            { 
                Map = _tileLoader.Load(path, _tileTypes, _objectTypes);
                RaisePropertyChanged(() => Map);
            }
        }

        private void Create(Window window)
        {
            new CreateWindow()
            {
                Owner = window,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            }.ShowDialog();
        }
    }
}