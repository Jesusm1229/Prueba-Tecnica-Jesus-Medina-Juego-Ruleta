using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	[Index(nameof(Name), IsUnique = true)]
	public class Player
	{
		[Column("PlayerId")]
		public Guid Id { get; set; }
		
		[Required(ErrorMessage = "Player's name is a required field.")]
		[MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
		public string? Name { get; set; }
	
		//Principal entity
		public  Score? Score { get; set; }
	}
}
