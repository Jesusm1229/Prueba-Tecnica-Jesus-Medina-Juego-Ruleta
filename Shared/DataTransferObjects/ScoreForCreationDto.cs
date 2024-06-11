using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
	public record ScoreForCreationDto(int Points);
	//We dont have to include the PlayerId in the ScoreForCreationDto because the
	//PlayerId is already included in the route
	//[Route("api/players/{playerId}/scores")]
}
