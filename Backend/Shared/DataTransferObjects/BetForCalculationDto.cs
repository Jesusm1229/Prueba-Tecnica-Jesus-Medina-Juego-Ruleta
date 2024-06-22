using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
	public record BetForCalculationDto(
		
		
		[Required(ErrorMessage = "Points is a required field.")]
		[Range(0, int.MaxValue, ErrorMessage = "Score must be greater than 0.")]
		int Score,

		//Collection of Bets. A player can place multiple bets
		//[Required(ErrorMessage = "Required to place at least one bet")]
		//ICollection<BetDto> Bets);

		[Required(ErrorMessage = "Category is a required field - Straight, Even, Odd, Color.")]
		string Category,

		[Required(ErrorMessage = "BetAmount is a required field.")]
		[Range(0, int.MaxValue, ErrorMessage = "BetAmount must be greater than 0.")]
		int BetAmount,

		[Range(0, 36, ErrorMessage = "Number must be between {1} and {2}.")]
		int? Number,

		[Required(ErrorMessage = "Color is a required field.")]
		string Color,
		
		[Required(ErrorMessage = "SpinWheel Number is a required field.")]
		[Range(0, 36, ErrorMessage = "SpinWheel Number must be between {1} and {2}.")]
		int SpinWheelNumber,


		[Required(ErrorMessage = "SpinWheel Color is a required field.")]
		string SpinWheelColor

		);



}
