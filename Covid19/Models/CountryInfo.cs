using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Covid19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        private Point? location;

        public override Point Location
        {
            get
            {
                if (location != null)
                    return (Point)location;

                if (ProvinceCounts is null) return default;

                var average_x = ProvinceCounts.Average(p => p.Location.X);
                var average_y = ProvinceCounts.Average(p => p.Location.Y);

                return (Point)(location = new Point(average_x, average_y));
            }
            set => location = value;
        }

        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
    }
}
