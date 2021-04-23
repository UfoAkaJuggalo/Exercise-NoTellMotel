using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoTell_Services.Interfaces;
using NoTell_Services.ViewModel;

namespace NoTell_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult Add(AddGuestVM model)
        {
            var result = _guestService.AddGuest(model.Name, model.LastName, model.Phone);

            return result > 0 ? StatusCode(StatusCodes.Status201Created, result) : StatusCode(StatusCodes.Status500InternalServerError, "Unable to add a new guest");
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuestVM))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult Get(int id)
        {
            var result = _guestService.GetGuestById(id);

            return result is not null ? Ok(result) : StatusCode(StatusCodes.Status500InternalServerError, "Guest not found.");
        }
    }
}