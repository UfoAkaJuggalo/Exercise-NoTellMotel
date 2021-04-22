using System;
using System.Collections.Generic;
using NoTell_DAL.Entities;

namespace NoTell_DAL.RepositoriesInterfaces
{
    public interface IReservationRepository
    {
        int AddReservation(DateTime reservationFrom, DateTime reservationTo, int guestId, int roomId);
        void DeleteReservation(int reservationId);
        void UpdateReservation(int reservationId, DateTime reservationFrom, DateTime reservationTo, int guestId,
                               int roomId);
        Reservation GetReservationById(int reservationId);
        IEnumerable<Reservation> GetReservationList();
    }
}