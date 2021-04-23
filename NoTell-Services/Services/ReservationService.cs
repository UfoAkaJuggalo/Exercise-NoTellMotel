using System;
using System.Collections.Generic;
using System.Linq;
using NoTell_DAL.RepositoriesInterfaces;
using NoTell_Services.Interfaces;
using NoTell_Services.ViewModel;

namespace NoTell_Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IGuestRepository _guestRepository; 

        public ReservationService(IRoomRepository roomRepository, IReservationRepository reservationRepository, IGuestRepository guestRepository)
        {
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
            _guestRepository = guestRepository;
        }

        public int AddReservation(DateTime reservationFrom, DateTime reservationTo, int guestId, int roomId) =>
            _reservationRepository.AddReservation(reservationFrom, reservationTo, guestId, roomId);

        public int AddReservationGuest(DateTime reservationFrom, DateTime reservationTo, int roomId, string name,
                                       string lastName, string phone)
        {
            int guestId;
            var guest = _guestRepository.getGuestByNamePhone(name, lastName, phone);

            guestId = guest is null ? _guestRepository.AddGuest(name, lastName, phone) : guest.GuestId;

            return _reservationRepository.AddReservation(reservationFrom, reservationTo, guestId, roomId);

        }

        public void DeleteReservation(int reservationId) => _reservationRepository.DeleteReservation(reservationId);

        public ReservationVM GetReservationById(int reservationId)
        {
            var result = _reservationRepository.GetReservationById(reservationId);

            return result is not null
                ? new ReservationVM
                {
                    ReservationId = result.ReservationId,
                    GuestId = result.GuestId,
                    RoomId = result.RoomId,
                    ReservationFrom = result.ReservationFrom,
                    ReservationTo = result.ReservationTo
                }
                : null;
        }

        public IEnumerable<ReservationVM> GetReservationList() =>
            _reservationRepository.GetReservationList().Select(x => new ReservationVM
            {
                ReservationId = x.ReservationId,
                GuestId = x.GuestId,
                RoomId = x.RoomId,
                ReservationFrom = x.ReservationFrom,
                ReservationTo = x.ReservationTo
            });

        public bool IsRoomAvalibileForDateRange(int roomId, DateTime dateFrom, DateTime dateTo) =>
            _reservationRepository.IsRoomAvalibileForDateRange(roomId, dateFrom, dateTo);

        public IEnumerable<RoomVM> GetRoomListAvaliableForDataRange(DateTime dateFrom, DateTime dateTo) =>
            _roomRepository.GetAllRooms()
                           .Where(x => IsRoomAvalibileForDateRange(x.RoomId, dateFrom, dateTo))
                           .Select(x => new RoomVM
                           {
                               RoomId = x.RoomId,
                               RoomNumber = x.RoomNumber,
                               NumberOfBedrooms = x.NumberOfBedrooms
                           });

        public IEnumerable<RoomVM> GetRoomListAvaliableForDataRangeWithBedrooms(DateTime dateFrom, DateTime dateTo, int bedrooms) =>
            _roomRepository.GetRoomsByBedrooms(bedrooms)
                           .Where(x => IsRoomAvalibileForDateRange(x.RoomId, dateFrom, dateTo))
                           .Select(x => new RoomVM
                           {
                               RoomId = x.RoomId,
                               RoomNumber = x.RoomNumber,
                               NumberOfBedrooms = x.NumberOfBedrooms
                           });
    }
}