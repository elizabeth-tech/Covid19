﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Covid19.Infrastructure.Converters
{
    internal class LocationPointToStr : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Point point)) return null;
            return $"Lat:{point.X};Lon:{point.Y}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
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
