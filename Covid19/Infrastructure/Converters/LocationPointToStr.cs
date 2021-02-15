using System;
using System.Globalization;
using System.Windows;

namespace Covid19.Infrastructure.Converters
{
    //[ValueConversion(typeof(Point), typeof(string))] - если дизайнер среды не понимает, что будет возвращено, то можно указать такой атрибут
    internal class LocationPointToStr : Converter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Point point)) return null;
            return $"Lat:{point.X};Lon:{point.Y}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string str)) return null;
            var components = str.Split(';');
            var lat_str = components[0].Split(':')[1];
            var lon_str = components[1].Split(':')[1];
            var lat = double.Parse(lat_str);
            var lon = double.Parse(lon_str);

            return new Point(lat, lon);
        }
    }
}
