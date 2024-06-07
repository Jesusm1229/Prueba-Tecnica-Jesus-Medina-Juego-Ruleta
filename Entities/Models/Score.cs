using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Score
	{
		[Column("ScoreId")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Points is a required field.")]
		public int Points { get; set; } = 0;

		[ForeignKey(nameof(Player))]
		public int PlayerId { get; set; }
		public Player Player { get; set; }
	}
}
