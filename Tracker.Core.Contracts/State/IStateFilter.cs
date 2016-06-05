using Tracker.DataAccess.Contracts.Contracts.Application;

namespace Tracker.Core.Contracts.State
{
	public interface IStateFilter
	{
		long[] GetStateAvailabilityToSet();
		long[] GetStateVisibility();
		long[] FilterByBusinessLogic(ApplicationEditData applicationData, long[] stateAvailability);
	}
}