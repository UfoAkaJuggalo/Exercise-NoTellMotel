using System;
using System.Collections.Generic;
using NoTell_Services.ViewModel;

namespace NoTell_Services.Interfaces
{
    public interface IReservationService
    {
        int AddReservation(DateTime reservationFrom, DateTime reservationTo, int guestId, int roomId);
        int AddReservationGuest(DateTime reservationFrom, DateTime reservationTo, int roomId, string name, string lastName, string phone);
        void DeleteReservation(int reservationId);
        ReservationVM GetReservationById(int reservationId);
        IEnumerable<ReservationVM> GetReservationList();
        bool IsRoomAvalibileForDateRange(int roomId, DateTime dateFrom, DateTime dateTo);
        IEnumerable<RoomVM> GetRoomListAvaliableForDataRange(DateTime dateFrom, DateTime dateTo);
        IEnumerable<RoomVM> GetRoomListAvaliableForDataRangeWithBedrooms(DateTime dateFrom, DateTime dateTo, int bedrooms);
    }
}