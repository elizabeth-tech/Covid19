using System;
using System.Globalization;

namespace Covid19.Infrastructure.Converters
{
    class Ratio : Converter
    {
        //[ConstructorArgument("Coefficient")]
        public double Coefficient { get; set; } = 1;

        public Ratio() { }

        public Ratio(double Coefficient) => this.Coefficient = Coefficient;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            var x = System.Convert.ToDouble(value, culture);
            return x * Coefficient;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            var x = System.Convert.ToDouble(value, culture);
            return x / Coefficient;
        }
    }
}
