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
			var wheelSpinned = await SpinWheel();			

			switch (betForCalculationDto.Category)
			{
				case "Straight":
					{
						if (betForCalculationDto.Number == wheelSpinned.number && betForCalculationDto.Color == wheelSpinned.color)
						{
							//If the player wins, he gets 3 times the bet amount
							int score = betForCalculationDto.Score + betForCalculationDto.BetAmount * 3;

							return	new BetForResultDto(
									score,
									wheelSpinned.number,
									wheelSpinned.color,
									true,
									false
									
								);							
						}

						break;
					}
				case "Even":
					{
						//Even number and color
						if (wheelSpinned.number % 2 == 0 && betForCalculationDto.Color == wheelSpinned.color)
						{
							int score = betForCalculationDto.Score + betForCalculationDto.BetAmount;

							return	new BetForResultDto(
									score,
									wheelSpinned.number,
									wheelSpinned.color,
									true,
									false
								);							
						}				

						break;
					}
				case "Odd":
					{
						if (wheelSpinned.number % 2 != 0 && betForCalculationDto.Color == wheelSpinned.color)
						{
							int score = betForCalculationDto.Score + betForCalculationDto.BetAmount;

							return	new BetForResultDto(
									score,
									wheelSpinned.number,
									wheelSpinned.color,
									true,
									false
								);							
						}

						break;
					}
				case "Color":
					{
						//If the color is the same as the color of the wheel, the player wins half of the bet amount
						if (betForCalculationDto.Color == wheelSpinned.color)
						{
							int score = betForCalculationDto.Score + (betForCalculationDto.BetAmount / 2);

							return	new BetForResultDto(
									score,
									wheelSpinned.number,
									wheelSpinned.color,
									true,
									false
								);							
						}						
						break;
					}				
					
			}

			//Player lost
			return new BetForResultDto(
				betForCalculationDto.Score - betForCalculationDto.BetAmount,
				wheelSpinned.number,
				wheelSpinned.color,
				false,
				betForCalculationDto.Score - betForCalculationDto.BetAmount <= 0
				);
		}


		//Out Roulette
		private string[] Colors = [ "Red", "Black" ];

		private async Task<WheelPositionDto> SpinWheel()
		{
			//random number, random color
			Random random = new Random();
			int number =  random.Next(0, 37);
			string color = Colors[random.Next(0, 2)];

			return new WheelPositionDto(number, color);

		}

		
	}
	
}
