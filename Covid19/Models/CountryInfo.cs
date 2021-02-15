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

                if (Provinces is null) return default;

                var average_x = Provinces.Average(p => p.Location.X);
                var average_y = Provinces.Average(p => p.Location.Y);

                return (Point)(location = new Point(average_x, average_y));
            }
            set => location = value;
        }

        public IEnumerable<PlaceInfo> Provinces { get; set; }

        private IEnumerable<ConfirmedCount> counts;

        // Количество зараженных по стране
        public override IEnumerable<ConfirmedCount> Counts 
        { 
            get
            {
                if (counts != null) return counts;

                var points_count = Provinces.FirstOrDefault()?.Counts?.Count() ?? 0;
                if (points_count == 0) return Enumerable.Empty<ConfirmedCount>();
                var province_points = Provinces.Select(p => p.Counts.ToArray()).ToArray();
                var points = new ConfirmedCount[points_count];

                foreach (var province in province_points)
                    for (var i = 0; i < points_count; i++)
                    {
                        if (points[i].Date == default)
                            points[i] = province[i];
                        else
                            points[i].Count += province[i].Count;
                    }

                return counts = points;
            }
            set => Counts = value; 
        }

    }
}
