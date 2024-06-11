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
		[Required(ErrorMessage = "Points is a required field.")]
		public int Points { get; init; }
	}
	
}
