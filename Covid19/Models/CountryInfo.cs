using System.Collections.Generic;

namespace Covid19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
    }
}
