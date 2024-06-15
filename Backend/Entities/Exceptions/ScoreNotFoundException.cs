using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
	public class ScoreNotFoundException: NotFoundException
	{
		public ScoreNotFoundException(Guid scoreId)
			: base($"Score with id: {scoreId} doesn't exist in the database.")
		{
		}

	}

}
