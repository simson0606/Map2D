using System.Windows.Media.Imaging;

namespace Map2D.Model
{
    public class Tile
    {
        public string Name { get; set; }
        public string Character { get; set; }
        public BitmapImage Image { get; set; } = null;
    }
}