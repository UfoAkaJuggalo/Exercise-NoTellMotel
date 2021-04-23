using System;

namespace NoTell_Services.ViewModel
{
    public class AvailableRoomsWithBedroomsForDataRangeVM
    {
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
        public int Bedrooms { get; set; }
    }
}