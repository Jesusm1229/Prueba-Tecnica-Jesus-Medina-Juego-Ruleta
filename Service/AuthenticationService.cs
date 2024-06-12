using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

		private Player? _player;

		public async Task<bool> ValidateUser(PlayerForAuthenticationDto
			playerForAuthentication)
		{
			_player = await _userManager.FindByNameAsync(playerForAuthentication.UserName);

			if (_player == null)
			{
				_logger.LogInfo($"User with username: {playerForAuthentication.UserName} does not exist in the database.");
				return false;
			}				

			return true;
		}

		public async Task<string> CreateToken()
		{
			var signingCredentials = GetSigningCredentials();
			var claims = await GetClaims();
			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
			return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
		}

		private SigningCredentials GetSigningCredentials()
		{
			var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
			var secret = new SymmetricSecurityKey(key);
			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}

		private async Task<List<Claim>> GetClaims()
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, _player.UserName)
			};

			//var roles = await _userManager.GetRolesAsync(_player);

			//foreach (var role in roles)
			//{
			//	claims.Add(new Claim(ClaimTypes.Role, role));
			//}

			return claims;
		}

		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
			List<Claim> claims)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var tokenOptions = new JwtSecurityToken
			(
			issuer: jwtSettings["validIssuer"],
			audience: jwtSettings["validAudience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
			signingCredentials: signingCredentials
			);
			return tokenOptions;
		}

	}

}
