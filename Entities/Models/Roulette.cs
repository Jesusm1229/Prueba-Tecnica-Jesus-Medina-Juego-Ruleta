using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Roulette
	{
		[Column("RouletteId")]
		public Guid Id { get; set; }

		//Collection of positions. Will have 0-36 numbers with their respective colors
		public ICollection<Position>? Positions { get; set; }
	}
}
