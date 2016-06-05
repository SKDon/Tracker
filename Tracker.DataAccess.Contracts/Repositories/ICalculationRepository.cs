using Tracker.DataAccess.Contracts.Contracts.Calculation;

namespace Tracker.DataAccess.Contracts.Repositories
{
	public interface ICalculationRepository
	{
		void Add(CalculationData data, long applicationId);
		void RemoveByApplication(long applicationId);
		CalculationData GetByApplication(long applicationId);
		CalculationData[] GetByClient(long clientId);
	}
}