using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Map2D.ViewModel;

namespace Map2D.Model
{
    public class TileLoader
    {
        public void Save(MapViewModel map, string filename)
        {
            string[] tileLines = new string[map.Rows];
            string[] objectLines = new string[map.Rows];
            int index = 0;
            for (int r = 0; r < map.Rows; r++)
            {
                for (int c = 0; c < map.Columns; c++)
                {
                    tileLines[r] += map.Tiles[index].TileViewModel.Character;
                    objectLines[r] += map.Tiles[index].ObjectViewModel.Character;
                    index++;
                }
            }

            var path = Path.GetDirectoryName(filename);
            var file = Path.GetFileNameWithoutExtension(filename);
            var extension = Path.GetExtension(filename);

            File.WriteAllLines(filename, tileLines);
            File.WriteAllLines($"{path}\\{file}{extension}o", objectLines);
        }

        public MapViewModel Load(string filename, ObservableCollection<TileViewModel> tileTypes, ObservableCollection<TileViewModel> tileObjectTypes)
        {
            var path = Path.GetDirectoryName(filename);
            var file = Path.GetFileNameWithoutExtension(filename);
            var extension = Path.GetExtension(filename);
            var objectPath = $"{path}\\{file}{extension}o";

            var tileLines = File.ReadAllLines(filename);
            var objectLines = Enumerable.Range(0, tileLines.Length * tileLines[0].Length).Select(s => tileObjectTypes[0].ToModel().Character).ToArray();

            if (File.Exists(objectPath))
            {
                objectLines = File.ReadAllLines(objectPath);
            }

            var objects = string.Join("", objectLines);

            MapViewModel mapViewModel = new MapViewModel(tileLines.Length, tileLines[0].Length, tileTypes[0].ToModel(), tileObjectTypes[0].ToModel() );
            int index = 0;
            foreach (var line in tileLines)
            {
                foreach (var character in line)
                {
                    var objectCharacter = objects[index];
                    mapViewModel.Tiles[index] = new TileObjectViewModel
                    {
                        TileViewModel = new TileViewModel(tileTypes.First(tt => tt.Character == character.ToString()).ToModel()),
                        ObjectViewModel = new TileViewModel(tileObjectTypes.First(tt => tt.Character == objectCharacter.ToString()).ToModel())
                    };
                    index++;
                }
            }

            return mapViewModel;
        }
    }
}