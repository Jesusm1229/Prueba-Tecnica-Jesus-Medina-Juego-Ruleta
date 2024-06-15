using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
	public interface IBetService
	{
		//Bet For Calculation 
		Task<BetForResultDto> PlaceBet(BetForCalculationDto betForCalculationDto);
		
	}
}
