using System;

namespace Covid19.Models
{
    // Количество подтвержденных случаев
    internal struct ConfirmedCount
    {
        public DateTime Date { get; set; }

        public int Count { get; set; }
    }
}
