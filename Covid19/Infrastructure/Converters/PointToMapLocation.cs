using MapControl;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Covid19.Infrastructure.Converters
{
    internal class PointToMapLocation : Converter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Point point)) return null;
            return new Location(point.X, point.Y);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Location location)) return null;
            return new Point(location.Latitude, location.Longitude);
        }
    }
}
