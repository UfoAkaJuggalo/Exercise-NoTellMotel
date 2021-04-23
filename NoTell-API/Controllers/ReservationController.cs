using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoTell_Services.Interfaces;
using NoTell_Services.ViewModel;

namespace NoTell_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpDelete]
        [Route("remove")]
        [ProducesResponseType(StatusCodes.Status205ResetContent)]
        public IActionResult RemoveReservation(int id)
        {
            _reservationService.DeleteReservation(id);
            
            return StatusCode((StatusCodes.Status205ResetContent));
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationVM))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult GetReservation(int id)
        {
            var result = _reservationService.GetReservationById(id);

            return result is not null
                ? Ok(new ReservationVM
                {
                    ReservationId = result.ReservationId,
                    GuestId = result.GuestId,
                    RoomId = result.RoomId,
                    ReservationFrom = result.ReservationFrom,
                    ReservationTo = result.ReservationTo
                })
                : StatusCode(StatusCodes.Status500InternalServerError, "Reservation not found");
        }

        [HttpGet]
        [Route("isAvailable")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult IsAvalible(IsRoomAvaliableVM request)
        {
            return Ok(_reservationService.IsRoomAvalibileForDateRange(request.RoomId, request.ReservationFrom,
                                                                      request.ReservationTo));
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult AddReservation(AddReservationVM reservation)
        {
            var result = _reservationService.AddReservation(reservation.ReservationFrom, reservation.ReservationTo,
                                                            reservation.GuestId, reservation.RoomId);
            
            return result > 0
                ? StatusCode(StatusCodes.Status201Created, result)
                : StatusCode(StatusCodes.Status500InternalServerError, "Unable to add a new reservation");
        }

        [HttpPost]
        [Route("addResGuest")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult AddReservationGuest(AddReservationGuestVM reservation)
        {
            var result = _reservationService.AddReservationGuest(reservation.ReservationFrom, reservation.ReservationTo,
                                                                 reservation.RoomId, reservation.Name,
                                                                 reservation.LastName, reservation.Phone);

            return result > 0
                ? StatusCode(StatusCodes.Status201Created, result)
                : StatusCode(StatusCodes.Status500InternalServerError, "Unable to add a new reservation");
        }

        [HttpGet]
        [Route("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReservationVM>))]
        public IEnumerable<ReservationVM> GetReservations() => _reservationService.GetReservationList();

        [HttpPost]
        [Route("availableRooms")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomVM>))]
        public IEnumerable<RoomVM> AvailableRooms(AvailableRoomsForDataRangeVM model) =>
            _reservationService.GetRoomListAvaliableForDataRange(model.ReservationFrom, model.ReservationTo);

        [HttpPost]
        [Route("abailableRoomsBedrooms")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomVM>))]
        public IEnumerable<RoomVM> AvalibleRoomsBedrooms(AvailableRoomsWithBedroomsForDataRangeVM model) =>
            _reservationService.GetRoomListAvaliableForDataRangeWithBedrooms(
                model.ReservationFrom, model.ReservationTo, model.Bedrooms);
    }
}