using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Player
	{
		[Key]
		[Required(ErrorMessage = "Player's name is a required field.")]
		[MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]

		public string? Name { get; set; }

		[ForeignKey(nameof(Score))]
		public int Score { get; set; }
		public Score? Score { get; set; }
	}
}
