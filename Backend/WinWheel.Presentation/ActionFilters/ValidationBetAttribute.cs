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
				var errors = ValidateBet(betForCalculationDto);

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
		}

		private List<string> ValidateBet(BetForCalculationDto bet)
		{
			var errors = new List<string>();

			ValidateBetAmountRelativeToScore(bet, errors);
			ValidateBetAmountForCategory(bet, errors);
			ValidateColor(bet, errors);
			ValidateCategory(bet, errors);
			ValidateNumberForStraightCategory(bet, errors);
			ValidateColorForColorCategory(bet, errors);
			ValidateSpinWheelColor(bet, errors);
			ValidateSpinWheelNumber(bet, errors);

			return errors;
		}

		private void ValidateBetAmountRelativeToScore(BetForCalculationDto bet, List<string> errors)
		{
			if (bet.BetAmount > bet.Score)
				errors.Add("Bet amount must be lower than your score.");
		}

		private void ValidateBetAmountForCategory(BetForCalculationDto bet, List<string> errors)
		{
			if (bet.Category == "Straight" && bet.BetAmount > int.MaxValue / 3)
				errors.Add($"Bet amount must be lower than {int.MaxValue / 3} for Straight category.");

			if ((bet.Category == "Even" || bet.Category == "Odd") && bet.BetAmount > int.MaxValue / 2)
				errors.Add($"Bet amount must be lower than {int.MaxValue / 2} for {bet.Category} category.");

			if (bet.Category == "Color" && bet.Score + ((long)bet.BetAmount / 2) > int.MaxValue - 1)
				errors.Add("Max limit Bet Amount for this category bet has been reached.");
		}

		private void ValidateColor(BetForCalculationDto bet, List<string> errors)
		{
			if (bet.Color != "Red" && bet.Color != "Black")
				errors.Add("Color must be Red or Black.");
		}

		private void ValidateCategory(BetForCalculationDto bet, List<string> errors)
		{
			var validCategories = new List<string> { "Straight", "Even", "Odd", "Color" };
			if (!validCategories.Contains(bet.Category))
				errors.Add("Category must be Straight, Even, Odd, or Color.");
		}

		private void ValidateNumberForStraightCategory(BetForCalculationDto bet, List<string> errors)
		{
			if (bet.Category == "Straight" && bet.Number == null)
				errors.Add("Number is required for Straight category.");
		}

		private void ValidateColorForColorCategory(BetForCalculationDto bet, List<string> errors)
		{
			if (bet.Category == "Color" && bet.Color == null)
				errors.Add("Color is required for Color category.");
		}

		private void ValidateSpinWheelColor(BetForCalculationDto bet, List<string> errors)
		{
			if (bet.SpinWheelColor != "Red" && bet.SpinWheelColor != "Black")
				errors.Add("SpinWheel Color must be Red or Black.");
		}

		private void ValidateSpinWheelNumber(BetForCalculationDto bet, List<string> errors)
		{
			if (bet.SpinWheelNumber < 0 || bet.SpinWheelNumber > 36)
				errors.Add("SpinWheel Number must be between 0 and 36.");
		}

		public void OnActionExecuted(ActionExecutedContext context) { }
	
	}
	
}
