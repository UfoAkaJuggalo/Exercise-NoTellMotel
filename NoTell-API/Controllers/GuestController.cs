﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoTell_API.ViewModel;
using NoTell_Services.Interfaces;

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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult Add(AddGuestVM model)
        {
            var result = _guestService.AddGuest(model.Name, model.LastName, model.Phone);

            if (result > 0)
                return StatusCode(StatusCodes.Status201Created, result);
            
            return StatusCode(StatusCodes.Status500InternalServerError, "Unable to add a new guest");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuestVM))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult Get(int id)
        {
            var result = _guestService.GetGuestById(id);

            if (result is not null)
                return Ok(result);

            return StatusCode(StatusCodes.Status500InternalServerError, "Guest not found."); 
        }
    }
}