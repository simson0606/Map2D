using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Map2D.ViewModel;

namespace Map2D.ValueConverters
{
    public class MapViewModelToWidthConverter : MarkupExtension, IValueConverter
    {
        private static MapViewModelToWidthConverter _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new MapViewModelToWidthConverter());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MapViewModel model)
            {
                return model.Columns * model.TileSize + 4;
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}