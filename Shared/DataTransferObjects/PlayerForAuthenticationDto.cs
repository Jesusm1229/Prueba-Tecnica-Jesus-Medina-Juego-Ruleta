using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
	public record PlayerForAuthenticationDto
	{
		[Required(ErrorMessage = "Username is required")]
		public string? UserName { get; init; }		
	}
}
