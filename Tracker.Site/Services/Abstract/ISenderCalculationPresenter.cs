using Tracker.ViewModels.Calculation.Sender;

namespace Tracker.Services.Abstract
{
	public interface ISenderCalculationPresenter
	{
		SenderCalculationListCollection List(long senderId, int take, long skip);
	}
}
