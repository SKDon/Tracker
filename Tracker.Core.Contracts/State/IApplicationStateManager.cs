namespace Tracker.Core.Contracts.State
{
	public interface IApplicationStateManager
	{
		void SetState(long applicationId, long stateId);
	}
}
