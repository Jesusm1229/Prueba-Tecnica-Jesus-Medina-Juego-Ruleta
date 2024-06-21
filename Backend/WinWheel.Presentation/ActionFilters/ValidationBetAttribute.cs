using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWheel.Presentation.ActionFilters
{
	public class ValidationBetAttribute: IActionFilter
	{ 
		public ValidationBetAttribute()
		{
		}
				

		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (context.ActionArguments.TryGetValue("betForCalculationDto", out var dtoObj) && dtoObj is BetForCalculationDto betForCalculationDto)
			{
				var errors = new List<string>();

				if (betForCalculationDto.BetAmount > betForCalculationDto.Score)
					errors.Add("Bet amount must be lower than your score.");

				//Check the amount bet and category so the score int doesnt overflow
				if (betForCalculationDto.Category == "Straight" && betForCalculationDto.BetAmount > int.MaxValue/3)
					errors.Add("Bet amount must be lower than "+ int.MaxValue/3  + " for Straight category.");

				if ((betForCalculationDto.Category == "Even" || betForCalculationDto.Category == "Odd") && betForCalculationDto.BetAmount > int.MaxValue/2)
					errors.Add("Bet amount must be lower than "+ int.MaxValue/2  + " for " + betForCalculationDto.Category + " category.");

				if(betForCalculationDto.Category == "Color" && betForCalculationDto.Score + ((long)betForCalculationDto.BetAmount / 2) > int.MaxValue - 1)
					errors.Add("Max limit for this category bet has been reached.");

				if (betForCalculationDto.Color != "Red" && betForCalculationDto.Color != "Black")
					errors.Add("Color must be Red or Black.");

				if (betForCalculationDto.Category != "Straight" && betForCalculationDto.Category != "Even" && betForCalculationDto.Category != "Odd" && betForCalculationDto.Category != "Color" )
					errors.Add("Category must be Straight, Even, Odd, or Color.");

				if (betForCalculationDto.Category == "Straight" && betForCalculationDto.Number == null)
					errors.Add("Number is required for Straight category.");

				if (errors.Any())
				{
					context.Result = new BadRequestObjectResult(string.Join(" ", errors));
					return;
				}
			}
			else
			{
				context.Result = new BadRequestObjectResult("Invalid or missing betForCalculationDto object.");
			}
			//else
			//	context.Result = new BadRequestObjectResult("BetAmount is a required field");
		}

		public void OnActionExecuted(ActionExecutedContext context) { }
	
	}
	
}
