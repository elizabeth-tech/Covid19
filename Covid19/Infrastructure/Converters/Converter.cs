﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Covid19.Infrastructure.Converters
{
    internal abstract class Converter : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            throw new NotSupportedException("Обратное преобразование не поддерживается");
    }
}
