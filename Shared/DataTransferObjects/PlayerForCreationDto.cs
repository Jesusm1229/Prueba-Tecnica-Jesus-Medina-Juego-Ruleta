using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
	public record PlayerForCreationDto(string Name, //Only Name is required to create a player
		ScoreForCreationDto Score//Score is optional to create a player
		); 
	
}
