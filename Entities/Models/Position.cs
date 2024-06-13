using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Position
	{
		[Column("PositionId")]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Roullete number is a required field.")]
		[Range(0, 36, ErrorMessage = "Roullete Layaout Number is between 0 - 36")]
		public int Number { get; set; }

		//One to Many relationship with Color - A color can have many positions
		[ForeignKey(nameof(Color))]
		public Guid? ColorId { get; set; }
		public Color? Color { get; set; }

		//One to Many relationship with Roulette - A roulette can have many positions
		[ForeignKey(nameof(Roulette))]
		public Guid RouletteId { get; set; }
		public Roulette? Roulette { get; set; }
	}
}
