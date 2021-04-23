using System;
using System.Collections.Generic;
using System.Linq;
using NoTell_DAL.Entities;
using NoTell_DAL.RepositoriesInterfaces;

namespace NoTell_DAL.InMemoryRepositories
{
    public class ReservationInMemoryRepository : IReservationRepository
    {
        private readonly List<Reservation> _reservations;
        private int _idCounter;

        public ReservationInMemoryRepository()
        {
            _reservations = new List<Reservation>();
            _idCounter = 0;
        }

        public int AddReservation(DateTime reservationFrom, DateTime reservationTo, int guestId, int roomId)
        {
            _reservations.Add(new Reservation
            {
                ReservationFrom = reservationFrom,
                ReservationTo = reservationTo,
                GuestId = guestId,
                RoomId = roomId,
                ReservationId = ++_idCounter
            });

            return _idCounter;
        }

        public void DeleteReservation(int reservationId) => _reservations.Remove(GetReservationById(reservationId));

        public void UpdateReservation(int reservationId, DateTime reservationFrom, DateTime reservationTo, int guestId, int roomId)
        {
            foreach (var reservation in _reservations.Where(w => w.ReservationId == reservationId))
            {
                reservation.ReservationFrom = reservationFrom;
                reservation.ReservationTo = reservationTo;
                reservation.GuestId = guestId;
                reservation.RoomId = roomId;
            }
        }

        public Reservation GetReservationById(int reservationId) => _reservations.FirstOrDefault(f => f.ReservationId == reservationId);

        public IEnumerable<Reservation> GetReservationList() => _reservations;
        
        public bool IsRoomAvalibileForDateRange(int roomId, DateTime dateFrom, DateTime dateTo)
        {
            return !_reservations
                         .Where(w => w.RoomId == roomId && (w.ReservationFrom < dateTo && dateFrom < w.ReservationTo))
                         .Any();
        }
    }
}