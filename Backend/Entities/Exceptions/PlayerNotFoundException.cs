using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
	public class PlayerNotFoundException: NotFoundException
	{
		
		public PlayerNotFoundException(Guid playerId)
			: base($"Player with id {playerId} not found")
		{ }

		public PlayerNotFoundException(string username)
			: base($"Player with username {username} not found")
		{ }
	}
	
}
