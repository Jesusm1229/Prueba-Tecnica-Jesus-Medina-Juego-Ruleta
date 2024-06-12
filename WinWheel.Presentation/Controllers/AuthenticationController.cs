﻿using Entities.Models;
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
	[Route("api/authentication")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IServiceManager _service;
		public AuthenticationController(IServiceManager service) => _service = service;


		[HttpPost]		
		public async Task<IActionResult> RegisterUser(PlayerForCreationDto playerForCreation)
		{
			var result = await _service.AuthenticationService.RegisterUser(playerForCreation);

			if (playerForCreation == null)
				return BadRequest("PlayerForCreationDto object is null");

			if (!ModelState.IsValid)
				return UnprocessableEntity(ModelState);

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
	}



}
