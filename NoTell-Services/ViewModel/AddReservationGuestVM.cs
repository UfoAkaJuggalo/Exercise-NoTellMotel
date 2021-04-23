using System;

namespace NoTell_Services.ViewModel
{
    public class AddReservationGuestVM
    {
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}