using System;

namespace NoTell_Services.ViewModel
{
    public class IsRoomAvaliableVM
    {
        public int RoomId { get; set; }
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
    }
}