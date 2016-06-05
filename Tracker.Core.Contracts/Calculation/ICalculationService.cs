using Tracker.DataAccess.Contracts.Contracts.Calculation;

namespace Tracker.Core.Contracts.Calculation
{
	public interface ICalculationService
	{
		CalculationData Calculate(long applicationId);
		void CancelCalculatation(long applicationId);
	}
}