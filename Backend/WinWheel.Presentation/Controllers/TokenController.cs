using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWheel.Presentation.Controllers
{
	[Route("api/token")]
	[ApiController]
	public class TokenController : ControllerBase
	{
		private readonly IServiceManager _service;
		public TokenController(IServiceManager service) => _service = service;

		[HttpPost("refresh")]		
		public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
		{
			if (tokenDto == null)
				return BadRequest("TokenDto object is null");

			if (!ModelState.IsValid)
				return UnprocessableEntity(ModelState);

			var tokenDtoReturn = await _service.AuthenticationService.RefreshToken(tokenDto);			
			

			return Ok(tokenDtoReturn);
		}



	}
}
