using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IAuthenticationService
	{
		Task<IdentityResult> RegisterUser(PlayerForCreationDto playerForCreation);

		Task<bool> ValidateUser(PlayerForAuthenticationDto playerForAuthentication);		

		Task<TokenDto> CreateToken(bool populateExp);

		Task<TokenDto> RefreshToken(TokenDto tokenDto);

		//log out
		Task<bool> LogOut(TokenDto tokenDto);

	}
}
