using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWheel.Presentation.ActionFilters;

namespace WinWheel.Presentation.Controllers
{
	[Route("api/authentication")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IServiceManager _service;
		public AuthenticationController(IServiceManager service) => _service = service;

		/// <summary>
		/// Register Player
		/// </summary>
		/// <param name="playerForCreation"></param>
		/// <returns>A newly created player</returns>
		/// <response code="201">Returns the newly created item</response>
		[HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> RegisterUser(PlayerForCreationDto playerForCreation)
		{
			var result = await _service.AuthenticationService.RegisterUser(playerForCreation);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}

			return StatusCode(201);

		}
		/// <summary>
		/// Login Player
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		[HttpPost("login")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> Authenticate([FromBody] PlayerForAuthenticationDto player)
		{
			
			if(!await _service.AuthenticationService.ValidateUser(player))
			{
				return Unauthorized();
			}
			//Token Refresh generation
			var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);

			return Ok(tokenDto);

		}

		//Log out Player
		[HttpPost("logout")]
		public async Task<IActionResult> Logout([FromBody] TokenDto tokenDto)
		{
			if (tokenDto == null)
				return BadRequest("TokenDto object is null");

			if (!ModelState.IsValid)
				return UnprocessableEntity(ModelState);

			var tokenDtoReturn = await _service.AuthenticationService.LogOut(tokenDto);

			return Ok(201);
		}
	}



}
