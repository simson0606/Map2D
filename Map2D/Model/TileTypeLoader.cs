using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;

namespace Map2D.Model
{
    public class TileTypeLoader
    {
        private readonly string _tilesFolder = $"{AppDomain.CurrentDomain.BaseDirectory}\\Tiles";
        private readonly string _objectsFolder = $"{AppDomain.CurrentDomain.BaseDirectory}\\Objects";
        private readonly string _descriptionFile = "\\description.ini";
        public List<Tile> LoadTiles()
        {
            var directories = Directory.GetDirectories(_tilesFolder);
            return directories.Select(FromDirectory).ToList();
        }

        private Tile FromDirectory(string path)
        {
            var contents = File.ReadAllLines(path + _descriptionFile);
            return new Tile
            {
                Name = Regex.Match(contents[0], "(?<=name=).*").Value,
                Character = Regex.Match(contents[1], "(?<=character=).*").Value,
                Image =  new BitmapImage(new Uri($"{path}\\{Regex.Match(contents[2], "(?<=file=).*").Value}"))
            };
        }

        internal List<Tile> LoadObjects()
        {
            var directories = Directory.GetDirectories(_objectsFolder);
            return directories.Select(FromDirectory).ToList();
        }
    }
}