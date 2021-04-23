using System;

namespace NoTell_Services.ViewModel
{
    public class AddReservationVM
    {
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
    }
}