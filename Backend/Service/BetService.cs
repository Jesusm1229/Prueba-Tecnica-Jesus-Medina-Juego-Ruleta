using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	internal sealed class BetService: IBetService
	{
		private readonly ILoggerManager _logger;

		public BetService(ILoggerManager logger) => _logger = logger;


		public async Task<BetForResultDto> PlaceBet(BetForCalculationDto betForCalculationDto)
		{
			int scoreChange = await CalculateScoreChangeAsync(betForCalculationDto); // Now awaiting the async method
			bool isWin = scoreChange > 0 || (betForCalculationDto.BetAmount == 1 && scoreChange >= 0);
			int finalScore = betForCalculationDto.Score + scoreChange;

			return new BetForResultDto(
				finalScore,
				//betForCalculationDto.SpinWheelNumber,
				//betForCalculationDto.SpinWheelColor,
				isWin,
				finalScore <= 0
			);
		}

		private async Task<int> CalculateScoreChangeAsync(BetForCalculationDto bet)
		{
			// Hypothetical asynchronous operation
			// var result = await someAsyncDatabaseQuery(bet);
			switch (bet.Category)
			{
				case "Straight":
					return bet.Number == bet.SpinWheelNumber && bet.Color == bet.SpinWheelColor ? bet.BetAmount * 3 : -bet.BetAmount;
				case "Even":
					return bet.SpinWheelNumber % 2 == 0 && bet.Color == bet.SpinWheelColor ? bet.BetAmount : -bet.BetAmount;
				case "Odd":
					return bet.SpinWheelNumber % 2 != 0 && bet.Color == bet.SpinWheelColor ? bet.BetAmount : -bet.BetAmount;
				case "Color":
					return bet.Color == bet.SpinWheelColor ? bet.BetAmount / 2 : -bet.BetAmount;
				default:
					_logger.LogError($"Unknown betting category: {bet.Category}");
					return 0;
			}
		}

		//Out Roulette
		//private string[] Colors = ["Red", "Black"];

		//private async Task<WheelPositionDto> SpinWheel()
		//{
		//	//random number, random color
		//	Random random = new Random();
		//	int number =  random.Next(0, 37);
		//	string color = Colors[random.Next(0, 2)];

		//	return new WheelPositionDto(number, color);

		//}


	}

}
