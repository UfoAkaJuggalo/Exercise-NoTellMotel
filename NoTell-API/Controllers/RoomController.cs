using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoTell_Services.Interfaces;
using NoTell_Services.ViewModel;

namespace NoTell_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomVM))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult GetById(int id)
        {
            var result = _roomService.GetRoomById(id);

            return result is not null ? Ok(result) : StatusCode(StatusCodes.Status500InternalServerError, "Room not found");
        }

        [HttpGet]
        [Route("getByBedrooms/{bedroomsNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomVM>))]
        public IEnumerable<RoomVM> GetByBedroomsNumber(int bedroomsNumber) => _roomService.GetRoomsByBedrooms(bedroomsNumber);
    }
}