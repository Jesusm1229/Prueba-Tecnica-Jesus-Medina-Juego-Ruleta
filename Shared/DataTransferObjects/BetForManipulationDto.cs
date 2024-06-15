using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
	public abstract record BetForManipulationDto
	{
		[Required(ErrorMessage = "Category is a required field.")]
		public string? Category { get; init; }

		[Required(ErrorMessage = "Points is a required field.")]
		[Range(0, int.MaxValue, ErrorMessage = "Score must be greater than 0.")]
		public int Score { get; init; }
		[Required(ErrorMessage = "BetAmount is a required field.")]
		[Range(0, int.MaxValue, ErrorMessage = "BetAmount must be greater than 0.")]
		public int BetAmount { get; init; }
		public int? Number { get; init; }
		[Required(ErrorMessage = "Color is a required field. - Red, Black")]
		public string Color { get; init; }
	}
}
