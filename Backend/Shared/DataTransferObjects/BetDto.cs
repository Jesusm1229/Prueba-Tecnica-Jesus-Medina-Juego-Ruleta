using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
	public record BetDto
	(
		[Required(ErrorMessage = "Category is a required field - Straight, Even, Odd, Red or Black.")]
		string Category,

		[Required(ErrorMessage = "BetAmount is a required field.")]
		[Range(0, int.MaxValue, ErrorMessage = "BetAmount must be greater than 0.")]
		int BetAmount,

		[Range(0, 36, ErrorMessage = "Number must be between {1} and {2}.")]
		int? Number,

		[Required(ErrorMessage = "Color is a required field.")]
		string Color
		
	
	);
	
}
