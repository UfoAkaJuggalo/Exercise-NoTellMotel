using System;

namespace NoTell_DAL.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
    }
}