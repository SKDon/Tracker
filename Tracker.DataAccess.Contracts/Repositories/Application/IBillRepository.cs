using Tracker.DataAccess.Contracts.Contracts.Application;

namespace Tracker.DataAccess.Contracts.Repositories.Application
{
	public interface IBillRepository
	{
		void AddOrReplace(long applicationId, BillData data);
		BillData Get(long applicationId);
	}
}