using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
	internal sealed class WheelService: IWheelService
	{
		private readonly ILoggerManager _logger;

		public WheelService(ILoggerManager logger) => _logger = logger;

		private string[] Colors = ["Red", "Black"];
		public async Task<WheelPositionDto> SpinWheel()
		{
			//random number, random color
			Random random = new Random();
			int number = random.Next(0, 37);
			string color = Colors[random.Next(0, 2)];

			return new WheelPositionDto(number, color);

		}
	}
}
