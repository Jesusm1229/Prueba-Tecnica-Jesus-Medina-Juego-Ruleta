using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	internal sealed class AuthenticationService: IAuthenticationService
	{
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly UserManager<Player> _userManager;
		private readonly IConfiguration _configuration;
		public AuthenticationService(ILoggerManager logger, IMapper mapper,
		UserManager<Player> userManager, IConfiguration configuration)
		{
			_logger = logger;
			_mapper = mapper;
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<IdentityResult> RegisterUser(PlayerForCreationDto
			playerForCreation)
		{
			var player = _mapper.Map<Player>(playerForCreation);

			var result = await _userManager.CreateAsync(player
				//,playerForCreation.
			);			

			return result;
		}
		
	}

}
