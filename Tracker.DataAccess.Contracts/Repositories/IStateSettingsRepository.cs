using Tracker.DataAccess.Contracts.Contracts.State;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Repositories
{
	public interface IStateSettingsRepository
	{
		StateRole[] GetStateAvailabilities();
		StateRole[] GetStateVisibilities();
		void SetStateAvailabilities(long stateId, RoleType[] roles);
		void SetStateVisibilities(long stateId, RoleType[] roles);
	}
}