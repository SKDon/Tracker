using Tracker.DataAccess.Contracts.Contracts;

namespace Tracker.DataAccess.Contracts.Repositories
{
	public interface ITransitRepository
	{
		long Add(TransitData transit);
		void Update(TransitData transit);
		TransitData[] Get(params long[] ids);
		TransitData GetByApplication(long applicationId);
		TransitData GetByClient(long clientId);
		void Delete(long transitId);
	}
}