using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Color
	{
		[Column("ColorId")]
		public Guid Id { get; set; }

		//Color black or red
		[Required(ErrorMessage = "Color name is a required field.")]
		public string? Name { get; set; }
		
		//One to Many relationship with Position - A color can have many positions
		IEnumerable<Position>? Positions { get; set; }

	}
}
