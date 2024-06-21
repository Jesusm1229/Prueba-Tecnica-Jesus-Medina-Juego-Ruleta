using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
	public abstract record ScoreForManipulationDto
	{
		//With this range, no 0 or negative points can be added.
		[Range(1, int.MaxValue, ErrorMessage = "Points cannot be less than 1.")]
		[Required(ErrorMessage = "Points is a required field.")]
		public int Points { get; init; }
	}
	
}
