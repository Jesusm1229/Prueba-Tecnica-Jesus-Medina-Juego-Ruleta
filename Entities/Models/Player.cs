using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{   //Principal entity
	[Index(nameof(UserName), IsUnique = true)]
	public class Player: IdentityUser<Guid> 
	{//We modify the Id property from Identity to be a Guid type and use it in the Player entity
		[Column("PlayerId")]
		public override Guid Id { get; set; }
		
		[Required(ErrorMessage = "Player's name is a required field.")]
		[MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
		public override string? UserName { get; set; }	
	
		public  Score? Score { get; set; }

		public string? RefreshToken { get; set; } //Token to refresh the access token
		public DateTime RefreshTokenExpiryTime { get; set; }

	}
}
