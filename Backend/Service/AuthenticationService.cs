using AutoMapper;
using Contracts;
using Entities.Exceptions;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	internal sealed class AuthenticationService : IAuthenticationService
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

		public async Task<IdentityResult> RegisterUser(PlayerForCreationDto	playerForCreation)
		{
			var player = _mapper.Map<Player>(playerForCreation);

			var result = await _userManager.CreateAsync(player
			//,playerForCreation.
			);

			return result;
		}

		private Player? _player;

		public async Task<bool> ValidateUser(PlayerForAuthenticationDto	playerForAuthentication)
		{
			_player = await _userManager.FindByNameAsync(playerForAuthentication.UserName);

			if (_player == null)
			{
				_logger.LogInfo($"User with username: {playerForAuthentication.UserName} does not exist in the database.");
				return false;
			}

			return true;
		}

		public async Task<TokenDto> CreateToken(bool populateExp)
		{
			var signingCredentials = GetSigningCredentials();
			var claims = await GetClaims();
			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

			var refreshToken = GenerateRefreshToken();
			_player.RefreshToken = refreshToken;

			if (populateExp)
				_player.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

			await _userManager.UpdateAsync(_player);

			var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			return new TokenDto(accessToken, refreshToken);
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

		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);

			}

		}

		private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"))),
				ValidateLifetime = true,
				ValidIssuer = jwtSettings["validIssuer"],
				ValidAudience = jwtSettings["validAudience"]
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken securityToken;

			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out  securityToken);
			
			var jwtSecurityToken = securityToken as JwtSecurityToken;

			if (jwtSecurityToken == null ||
				!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
				StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Invalid token");
			}

			return principal;
		}

		public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
		{
			var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

			var user = await _userManager.FindByNameAsync(principal.Identity.Name);

			if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
				user.RefreshTokenExpiryTime <= DateTime.Now)
				throw new RefreshTokenBadRequest();

			_player = user;

			return await CreateToken(populateExp: false);
		}

		public async Task<bool> LogOut()
		{
			_player.RefreshToken = null;
			_player.RefreshTokenExpiryTime = DateTime.Now;

			await _userManager.UpdateAsync(_player);

			return true;
		}


	}
}
