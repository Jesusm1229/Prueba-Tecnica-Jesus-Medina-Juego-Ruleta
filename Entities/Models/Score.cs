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
		[ForeignKey("ScoreId")]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Points is a required field.")]
		public int Points { get; set; } = 0;

		//Dependent entity
		public Guid PlayerId { get; set; }
		public Player Player { get; set; }
	}
}
