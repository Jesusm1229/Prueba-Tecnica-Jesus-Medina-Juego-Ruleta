﻿using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
	public interface IServiceManager
	{
		IPlayerService PlayerService { get; }
		IScoreService ScoreService { get; }
		IAuthenticationService AuthenticationService { get; }
		IBetService BetService { get; }

		IWheelService WheelService { get; }

	}
}
