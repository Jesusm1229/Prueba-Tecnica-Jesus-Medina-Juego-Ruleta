using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
	public class ScoreNotFoundException: NotFoundException
	{
		public ScoreNotFoundException(Guid employeeId)
			: base($"Score with id: {employeeId} doesn't exist in the database.")
		{
		}

	}

}
