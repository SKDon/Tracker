using System.Collections.Generic;
using Tracker.DataAccess.Contracts.Contracts.State;

namespace Tracker.DataAccess.Contracts.Repositories
{
	public interface IStateRepository
	{
		long Add(StateEditData data);
		void Delete(long id);
		IReadOnlyDictionary<long, StateData> Get(string language, params long[] ids);
		void Update(long id, StateEditData data);
	}
}