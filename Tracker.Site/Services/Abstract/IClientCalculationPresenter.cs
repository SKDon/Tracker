using Tracker.ViewModels.Calculation.Client;

namespace Tracker.Services.Abstract
{
	public interface IClientCalculationPresenter
	{
		ClientCalculationListCollection List(long clientId, int take, long skip);
	}
}
